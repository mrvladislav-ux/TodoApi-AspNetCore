using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(AppDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }
    
    public User? Register(RegisterDto dto)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Email == dto.Email); 

        if(existingUser != null)
        {
            return null;
        }

        var user = new User
        {
            Email = dto.Email
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public string? Login(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

        if(user == null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            dto.Password
        );

        if(result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("THIS_IS_A_VERY_LONG_SECRET_KEY_FOR_JWT_AUTH_1234567890");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),

            Expires = DateTime.UtcNow.AddHours(2),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
        )
        };

         var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
}

    public bool DeleteUser(int id)
    {
        var deletedUser = _context.Users.FirstOrDefault(u => u.Id == id);

        if(deletedUser == null)
        {
            return false;
        }

        _context.Users.Remove(deletedUser);
        _context.SaveChanges();

        return true;
    }

    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    }
}
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        var user = _authService.Register(dto);

        if(user == null)
        {
            return BadRequest("User already exist!");
        }

        return Ok(user);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var token = _authService.Login(dto);

        if(token == null)
        {
            return Unauthorized();
        }

        return Ok(new {token});

    }

    [HttpGet]
    public ActionResult<List<User>> GetUsers()
    {
        var users =  _authService.GetUsers();

        return Ok(users);
    }



    [HttpDelete("Users{id}")]
    public IActionResult DeleteUser(int id)
    {
        var deletedUser = _authService.DeleteUser(id);

        if(deletedUser == false)
        {
            return NotFound();
        }

        return NoContent();
    }
}

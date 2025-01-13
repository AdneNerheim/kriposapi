using Microsoft.AspNetCore.Mvc;
using SecurityWorkshop.Models;
using SecurityWorkshop.Services.Interfaces;
using SecurityWorkshop.Utils;

namespace SecurityWorkshop.Controllers;


[ApiController]
[Route("api/v1/user")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ILogger _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<string>> LogIn([FromBody] UserLogin user)
    {
        try
        {
            string? token = await _userService.getToken(user.email, user.password);
            if (token == null)
            {
                return StatusCode(StatusCodes.Status200OK, $"SELECT * FROM dbo.users WHERE Email = {user.email} AND Password = {user.password}");
            }
            return StatusCode(StatusCodes.Status200OK, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError, "Whoops...");
        }
    }
    
    [HttpGet("resetrequest")]
    public async Task<ActionResult<int>> ResetRequest(string email)
    {
        try
        {
            int? pin = await _userService.getResetPin(email);
            if (pin == null)
            {
                return StatusCode(
                    StatusCodes.Status200OK,
                    "User doesn't exi-I mean if that user exist they have received a pin."
                    );
            }
            return StatusCode(StatusCodes.Status200OK, $"SELECT * FROM dbo.users WHERE Email = {email}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError, "Whoops...");
        }
    }
    
    
    
    [HttpPut]
    public async Task<ActionResult<bool>> ResetPassword(int pin, string email, string newPassword)
    {
        try
        {
            bool success = await _userService.resetPassword(email, pin, newPassword);
            return Ok(success);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError, "Whoops...");
        }
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<bool>> CreateUser([FromBody] UserLogin user)
    {
        try
        {
            bool success = await _userService.createUser(user.email, user.password);
            if (success)
                return Created();
            return Ok(success);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError, "Whoops...");
        }
    }
}
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

    [HttpGet]
    public async Task<ActionResult<string>> LogIn(string email, string password)
    {
        try
        {
            string? token = await _userService.getToken(email, password);
            if (token == null)
            {
                return StatusCode(StatusCodes.Status200OK, "Invalid username or password, please try again");
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
    public async Task<ActionResult<int>> LogIn(string email)
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
            return StatusCode(StatusCodes.Status200OK, pin);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError, "Whoops...");
        }
    }
    
    
    
    [HttpPut]
    public async Task<ActionResult<bool>> ResetPassword(int pin, string newPassword)
    {
        try
        {
            bool success = await _userService.resetPassword(pin, newPassword);
            return Ok(success);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError, "Whoops...");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<bool>> CreateUser(string email, string password)
    {
        try
        {
            bool success = await _userService.createUser(email, password);
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
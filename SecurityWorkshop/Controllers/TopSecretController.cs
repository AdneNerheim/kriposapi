using Microsoft.AspNetCore.Mvc;
using SecurityWorkshop.Models;
using SecurityWorkshop.Services.Interfaces;
using SecurityWorkshop.Utils;

namespace SecurityWorkshop.Controllers;

[ApiController]
[Route("api/v1/topsecret")]
public class TopSecretController : Controller
{
    private readonly ITopSecretService _topSecretService;
    private readonly ILogger _logger;

    public TopSecretController(ITopSecretService topSecretService, ILogger<TopSecretController> logger)
    {
        _topSecretService = topSecretService;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TopSecret>>> GetAll(string token)
    {
        try
        {
            TokenManager tokenManager = new TokenManager();
            if (!tokenManager.ValidateToken(token))
                return Unauthorized("Nice try");
            return Ok(await _topSecretService.getAll());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError, "Whoops...");
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddAgent (string token, string codeName, string realName)
    {
        try
        {
            TokenManager tokenManager = new TokenManager();
            if (!tokenManager.ValidateToken(token))
                return Unauthorized("Nice try");
            bool result = await _topSecretService.addEntry(codeName, realName);
            if (result)
                return Created();
            return Ok(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError, "Whoops...");
        }
    }
}
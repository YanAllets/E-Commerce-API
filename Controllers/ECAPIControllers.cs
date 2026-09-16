using Microsoft.AspNetCore.Mvc;
using ECAPI.Models;
using ECAPI.CommerceService;
namespace E_CommerceApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ECAPIController : ControllerBase
{
    [HttpPost("{LogIn}")]
    public IActionResult LogIn(UserClass user)
    {
        var result = CommerceService.LogIn(user);
        if (result.success)
        {
            return Ok(result.user);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpPost]
    public IActionResult CreateUser(UserClass user)
    {
        CommerceService.SingUp(user);
        return Ok("deu bom");
    }
}

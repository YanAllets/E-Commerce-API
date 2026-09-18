using Microsoft.AspNetCore.Mvc;
using ECAPI.Models;
using ECAPI.CommerceService;
using System.Diagnostics.CodeAnalysis;
namespace E_CommerceApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ECAPIController : ControllerBase
{
    [HttpPost("Login")]
    public IActionResult LogIn(UserClass user)
    {
        var result = CommerceService.LogIn(user);
        if (result.success == true && result.user != null)
        {
            return Ok(result.user);
        }
        else if (result.success == false && result.user != null)
        {
            return NotFound("Wrong Password");
        }
        else if(result.success == false && result.user == null)
        {
            return NotFound("This email in non registered");
        }
        return NotFound();
    }
    
    [HttpPost]
    public IActionResult CreateUser(UserClass user)
    {
        CommerceService.SingUp(user);
        return Ok("deu bom");
    }
}

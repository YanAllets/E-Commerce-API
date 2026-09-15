using Microsoft.AspNetCore.Mvc;
using ECAPI.Models;
using ECAPI.CommerceService;
namespace E_CommerceApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ECAPIController : ControllerBase
{
    [HttpGet]
    public IActionResult LogIn(UserClass user)
    {
        return Ok();
    }
    [HttpPost]
    public IActionResult CreateUser(UserClass user)
    {
        CommerceService.SingUp(user);
        return Ok("deu bom");
    }
}

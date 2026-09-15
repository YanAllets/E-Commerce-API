using Microsoft.AspNetCore.Mvc;
using ECAPI.Models;
namespace E_CommerceApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ECAPIController : ControllerBase
{
    public IActionResult CreateUser(UserClass user)
    {
        return Ok();
    }
}

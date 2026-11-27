using Microsoft.AspNetCore.Mvc;
using ECAPI.Models;
using ECAPI.CommerceService;
using ECAPI.StoreService;
using ECAPI.AccountService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace E_CommerceApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ECAPIController : ControllerBase
{
    private readonly AccountService _accountService;
    //ta chamando por instacia pq o safado nao vem se for cru na classe (readonly la de baixo das 
    // classes subsequentes)
    public ECAPIController(AccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpPost("Login")]
    [AllowAnonymous]
    public IActionResult LogIn(UserClass User)
    {
        var result = _accountService.LogIn(User);
        if (result.success == true && result.user != null)
        {
            return Ok(result.token);
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
    
    [HttpPost("SingUp")]
    [AllowAnonymous]
    public IActionResult CreateUser(UserClass User)
    {
        _accountService.SingUp(User);
        return Ok("deu bom");
    }
    [HttpPost("CreateProduct")]
    [Authorize]
    public IActionResult CreateProduct(ProductClass Product)
    {
        StoreService.CreateProduct(Product);
        return Ok("ok");
    }
    [HttpPost("CreateStore")]
    [Authorize]
    public IActionResult CreateStore(StoreClass Store)
    {
        CommerceService.CreateStore(Store);
        return Ok("ok");
    }
    [HttpPost("CreateAd")]
    [Authorize]
    public IActionResult CreateAd(AdvertisementClass Ad)
    {
        var result = StoreService.CreateAd(Ad);
        if(result.success)
        {
            return Ok("ok");
        }
        else
        {
            return NotFound(result.message);
        }
    }
    [HttpPost("AddToCart")]
    [Authorize]
    public IActionResult AddToCart(int AdId,int quantity)
    {
        string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(id, out int UserId))
        {
            return Forbid();
        }
        if (!CommerceService.AddToCart(AdId,quantity,UserId))
        {
            return NotFound();
        }
        return Ok();
    }
    [HttpPost("RemoveFromCart")]
    [Authorize]
    public IActionResult RemoveFromCart(int AdId,int quantity)
    {
        string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(id, out int UserId))
        {
            return Forbid();
        }
    
        CommerceService.RemoveFromCart(quantity,AdId,UserId);
        return Ok();
    }
    [HttpPost("Buy")]
    [Authorize]
    public IActionResult Buy(int cartId,string paymentMethod,int installments)
    {
        return Ok(CommerceService.Buy(cartId,paymentMethod,installments));
    }
    [HttpPost("MakeReview")]
    [Authorize]
    public IActionResult MakeReview(ReviewClass review)
    {
        CommerceService.MakeReview(review);
        return Ok();
    }
    [HttpPost("ViewAccount")]
    [Authorize]
    public IActionResult ViewAccount()
    {
        string? idValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(idValue, out int userId))
        {
            return Forbid();
        }
        return Ok(_accountService.ViewAccount(userId));
    }
    [HttpPost("EditProfile")]
    [Authorize]
    public IActionResult EditProfile(UserClass EditedUser)
    {
        string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(id, out int UserId))
        {
            return Forbid();
        }
        return Ok(_accountService.EditProfile(UserId,EditedUser));
    }
}

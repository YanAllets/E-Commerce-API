using Microsoft.AspNetCore.Mvc;
using ECAPI.Models;
using ECAPI.CommerceService;
using ECAPI.StoreService;
namespace E_CommerceApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ECAPIController : ControllerBase
{
    [HttpPost("Login")]
    public IActionResult LogIn(UserClass User)
    {
        var result = CommerceService.LogIn(User);
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
    
    [HttpPost("SingUp")]
    public IActionResult CreateUser(UserClass User)
    {
        CommerceService.SingUp(User);
        return Ok("deu bom");
    }
    [HttpPost("CreateProduct")]
    public IActionResult CreateProduct(ProductClass Product)
    {
        StoreService.CreateProduct(Product);
        return Ok("ok");
    }
    [HttpPost("CreateStore")]
    public IActionResult CreateStore(StoreClass Store)
    {
        CommerceService.CreateStore(Store);
        return Ok("ok");
    }
    [HttpPost("CreateAd")]
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
    public IActionResult AddToCart(int AdId,int quantity,int cartId)
    {
        CommerceService.AddToCart(AdId,quantity,cartId);
        return Ok();
    }
    [HttpPost("RemoveFromCart")]
    public IActionResult RemoveFromCart(int AdId,int quantity,int cartId)
    {
        CommerceService.RemoveFromCart(quantity,cartId,AdId);
        return Ok();
    }
    [HttpPost("Buy")]
    public IActionResult Buy(int cartId,string paymentMethod,int installments)
    {
        return Ok(CommerceService.Buy(cartId,paymentMethod,installments));
    }
}

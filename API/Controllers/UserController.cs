namespace API.Controller;
using Service;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
public UserController(UserService userService)
{
_userService = userService;
}
[HttpPost("login")]
public async Task<IActionResult> Login(string email, string password){
var user = await _userService.Login(email, password);
return Ok(user);
}
[HttpPost("register")]
public async Task<IActionResult> Register(User user){
user = await _userService.Register(user);
return Ok(user);
}
[HttpGet("{id}")]
public async Task<IActionResult> GetUserById(int id){
var user = await _userService.GetUserById(id);
return Ok(user);
}
}

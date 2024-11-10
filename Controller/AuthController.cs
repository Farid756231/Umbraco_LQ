using Microsoft.AspNetCore.Mvc;
using umbraco_lingoquest.Dto;
using umbraco_lingoquest.Helpers;
using Umbraco.Cms.Core.Services;
using Microsoft.Win32;
using Org.BouncyCastle.Crypto.Generators;
using Serilog;

namespace umbraco_lingoquest.Controller;

[ApiController]
[Route("api/umbraco/[controller]")]
[ApiExplorerSettings(GroupName = "Auth")]
public class AuthController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService, IMemberService memberService)
    {
        _memberService = memberService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login(Login login)
    {
        var user = _memberService.GetByEmail(login.Email);
        if (user == null)
        {
            return BadRequest("Invalid Credentials");
        }

        if (!BCrypt.Net.BCrypt.Verify(login.Password, user.RawPasswordValue))
        {
            return BadRequest("Invalid Credentials");
        }

        var token = _jwtService.GenerateJwtToken(user.Id);

        Response.Cookies.Append("token", token, new CookieOptions { HttpOnly = true });
        return Ok("success");
    }

    [HttpPost("register")]
    public IActionResult Register(Register register)
    {
        var existingMember = _memberService.GetByEmail(register.Email);
        if (existingMember != null)
        {
            return BadRequest("Email is already registered.");
        }


        var newMember = _memberService.CreateMember(register.Email, register.Email, register.Email, "Member");
        newMember.RawPasswordValue = BCrypt.Net.BCrypt.HashPassword(register.Password);
        _memberService.Save(newMember);

        var token = _jwtService.GenerateJwtToken(newMember.Id);


        Response.Cookies.Append("token", token, new CookieOptions { HttpOnly = true });


        return Ok(new { message = "Registration successful!", token });
    }

    [HttpGet("user")]
    public IActionResult User()
    {
        try
        {
            var jwt = Request.Cookies["token"];
            var token = _jwtService.VerifyJwtToken(jwt);
            int userId = int.Parse(token.Issuer);
            var user = _memberService.GetById(userId);

            return Ok(user);
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");
        return Ok("Logged Out");
    }
}
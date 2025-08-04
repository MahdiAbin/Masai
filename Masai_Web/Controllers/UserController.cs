using System.Security.Claims;
using Core.DTOs.UserDTO;
using Core.Security;
using Data.Entities.User;
using Data.Repository;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Masai_Web.Controllers;

public class UserController : Controller
{
    IUserService _userService;
    IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Profile");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        if (ModelState.IsValid)
        {
            var Exist = await _userService.Login(dto.UserName, dto.Password.Hash());
            if (Exist)
            {
                var claim = new Claim(ClaimTypes.Name, dto.UserName);
                var claimiddIdentity = new ClaimsIdentity(new[] { claim },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var claimpro = new ClaimsPrincipal(claimiddIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimpro,new AuthenticationProperties{
                
                    ExpiresUtc = dto.IsRememberMe? DateTime.Now.AddYears(7):DateTime.Now.AddHours(1)
                        });
                return RedirectToAction("Profile");
            }

            ModelState.AddModelError("Login", "نام کاربری یا رمز عبور اشتباه است");
            return View(dto);
        }

        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> SignUp()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpDTO dto)
    {
        if (ModelState.IsValid&& dto.Is)
        {
            var map=_mapper.Map<User>(dto);
            await _userService.AddUser(map);
            return RedirectToAction("Login");
        }
        return View(dto);
    }
}
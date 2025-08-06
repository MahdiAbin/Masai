using System.Security.Claims;
using Core.DTOs;
using Core.DTOs.UserDTO;
using Core.Security;
using Data.Entities.User;
using Data.Repository;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimpro,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = dto.IsRememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddHours(1)
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
        if (ModelState.IsValid && dto.Is)
        {
            var exist = new
            {
                username = await _userService.CheckUserNameExists(dto.UserName),
                email = await _userService.CheckEmailExists(dto.Email),
                mobile = await _userService.CheckMobileExists(dto.MobileNumber)
            };
            if (exist.username)
            {
                ModelState.AddModelError("Exist", "نام کاربری تکراری است!!!");
            }

            if (exist.email)
            {
                ModelState.AddModelError("Exist", "ایمیل تکراری است!!!");
            }

            if (exist.mobile)
            {
                ModelState.AddModelError("Exist", "شماره تلفن تکراری است!!!");
            }

            if (exist.email || exist.mobile || exist.username)
            {
                return View(dto);
            }

            var map = _mapper.Map<User>(dto);
            await _userService.AddUser(map);
            return RedirectToAction("Login");
        }

        return View(dto);
    }

    [Authorize]
    [Route("/Profile")]
    public async Task<IActionResult> Profile()
    {
        var user = await _userService.GetUserByUserName(User.Identity.Name);
        var map = _mapper.Map<ProfileDTO>(user);
        return View(map);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> EditProfile()
    {
        var user = await _userService.GetUserByUserName(User.Identity.Name);
        var map = _mapper.Map<EditProfile>(user);
        return View(map);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditProfile(EditProfile dto)
    {
        if (ModelState.IsValid)
        {
            var map = _mapper.Map<User>(dto);
            var user = await _userService.EditProfile(map, User.Identity.Name);
            var map2 = _mapper.Map<EditProfile>(user);
            return RedirectToAction("Logout");
        }

        return View(dto);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> UpdatePassword()
    {
        return View();
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UpdatePassword(UpdatePassDTO dto)
    {
        if (ModelState.IsValid)
        {
            await _userService.EditPassword(User.Identity.Name, dto.Password);
            return RedirectToAction("Logout");
        }
        return View(dto);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SiparisApp.Business.Abstract;
using SiparisApp.Entities;
using SiparisApp.Web.EmailServices;
using SiparisApp.Web.Identity;
using SiparisApp.Web.Models;

namespace SiparisApp.Web.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IBasketService _basketService;
        private IMyEmailSender _smtpemailSender;

        public AccountController(IBasketService basketService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMyEmailSender smtpemailSender)
        {
            _basketService = basketService;
            _userManager = userManager;
            _signInManager = signInManager;
            _smtpemailSender = smtpemailSender;
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
               
                 
                
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
             
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });

            
                await _smtpemailSender.SendEmailAsync(model.Email, "Hesabınızı Onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:52950{callbackUrl}'>tıklayınız.</a>");


                return RedirectToAction("Login", "Account");
            }


            ModelState.AddModelError("", "Bilinmeyen hata oluştu lütfen tekrar deneyiniz.");
            return View(model);
        }


        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu email ile daha önce hesap oluşturulmamış.");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen hesabınızı email ile onaylayınız.");
                return View(model);
            }


            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }

            ModelState.AddModelError("", "Email veya parola yanlış");
            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData["message"] = "Geçersiz token.";
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    _basketService.InitializeBasket(user.Id );
                    TempData["message"] = "Hesabınız onaylandı";

                    return View();
                }
            }
            TempData["message"] = "Hesabınız onaylanmadı.";
            return View();


        }
        public IActionResult Manage()
        {
            var userId = _userManager.GetUserId(User);
            var basket = _basketService.GetBasketByUserId(userId);

            return View(basket);
        }
      
        [HttpPost]
        public IActionResult Manage(Basket bas)
        {
            var userId = _userManager.GetUserId(User);
            var basket = _basketService.GetBasketByUserId(userId);

            basket.CustomerAddress = bas.CustomerAddress;
            _basketService.Update(basket);

            return RedirectToAction("Manage");


        }



    }
}
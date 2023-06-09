﻿using FourthTask.Core.Abstractions;
using FourthTask.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FourthTask.Core.DataTransferObjects;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FourthTask.Controllers
{
    
    public class UserController : Controller
    { 
        private readonly IUserService userService; 
        private readonly IMapper mapper;
        private readonly IStatusService statusService; 

        public UserController(IMapper mapper, IUserService userService, IStatusService statusService )
        {  
            this.mapper = mapper;
            this.userService = userService; 
            this.statusService = statusService; 
        }


        [HttpGet]
        public IActionResult Registration()
        {

            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationModel model)
        {

            if (ModelState.IsValid)
            {
                var email = model.Email;

                if (userService.IsEmailExist(email) == false)
                {
                    var user = mapper.Map<UserDTO>(model);
                    user.StatusId = await statusService.GetStatusByName("Active");
                    user.RegistrationDate = DateTime.Now.ToString();
                    user.LastLoginDate = DateTime.Now.ToString();
                    var entity = await userService.CreateUserAsync(user);

                    if (entity > 0)
                    {

                        await Authenticate(email);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);

        }


        [HttpGet]
        public IActionResult Authentication()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticationAsync(AuthenticationModel model)
        {
            var isPasswordCorrect = await userService.CheckUserPassword(mapper.Map<UserDTO>(model));
            var IsActive = await userService.CheckUserStatus(mapper.Map<UserDTO>(model));
            if (isPasswordCorrect && IsActive )
            {
                var user = await userService.GetUserByEmailAsync(model.Email);
                await userService.EditUsersLastLoginDateAsync(user);

                await Authenticate(model.Email); 
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (IsActive)
                {
                    ViewData["ErrorMessage"] = "Entered an incorrect password";
                    return View(model);
                }
                else
                {
                    ViewData["ErrorMessage"] = "You have been blocked";
                    return View(model);
                }
               
               
            } 
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home"); 
        }

        private async Task Authenticate(string email)
        {

            var User = await userService.GetUserWithIncludesByEmailAsync(email);
                
            var claims = new List<Claim>()
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, User.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, User.StatusName) };

            if (claims != null)
            {
                var identity = new ClaimsIdentity(claims,
                    "ApplicationCookie",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            } 
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult IsLoggedIn()
        {

            if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                return Ok(true);
            }
            else return Ok(false);

        }

        [HttpGet]
        public async Task<IActionResult> UserLoginPreview()
        {

            if (User.Identities.Any(identity => identity.IsAuthenticated))
            {

                var UserEmail = User.Identity?.Name;

                if (string.IsNullOrEmpty(UserEmail))
                {
                    return NotFound();
                }


                var user = mapper.Map<UserShortDataModel>(await userService.GetUserByEmailAsync(UserEmail));

                return View(user);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var userEmail = User.Identity?.Name;
            var currentUser = await userService.GetUserByEmailAsync(userEmail);
            if (currentUser != null && await statusService.GetStatusByName("Active") == currentUser.StatusId)
            {
                var users = await userService.GetAllUsersWithIncluds();
                var userModels = mapper.Map<List<UserModel>>(users).ToList();
                

                return View(userModels);
            }
            else
            await HttpContext.SignOutAsync();
            return View("Authentication");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateStatus(List<UserModel> users, string action)
        {
            List<UserModel> usersForAction;

            var userEmail = User.Identity?.Name;
            var currentUser = await userService.GetUserByEmailAsync(userEmail);
            if (currentUser != null && await statusService.GetStatusByName("Active") == currentUser.StatusId)
            {

                if (action == "lock") {
                    var statusid = await statusService.GetStatusByName("Blocked");
                    usersForAction = users.Where(x => x.IsChosen == true).Select(x => x).ToList();
                    foreach (var n in usersForAction)
                    n.StatusId = statusid;
                     
                    await userService.UpdateUserRangeAsync(usersForAction.Select(x => mapper.Map<UserDTO>(x)).ToList());
                     
                    if (usersForAction.FirstOrDefault(x => x.Email.Equals(userEmail)) != null)
                    {
                        await HttpContext.SignOutAsync();
                        return View("Authentication");
                    }

                } else if (action == "unlock")
                {
                    var statusid = await statusService.GetStatusByName("Active");
                    usersForAction = users.Where(x => x.IsChosen == true).Select(x => x).ToList();
                    foreach (var n in usersForAction)
                    n.StatusId = statusid;

                    await userService.UpdateUserRangeAsync(usersForAction.Select(x => mapper.Map<UserDTO>(x)).ToList());
                }
                else if (action == "delete")
                { 
                    usersForAction = users.Where(x => x.IsChosen == true).Select(x =>x).ToList(); 
                    await userService.RemoveRangeUserAsync(usersForAction.Select(x=> mapper.Map<UserDTO>(x)).ToList());

                    if (usersForAction.FirstOrDefault(x=>x.Email.Equals(userEmail))!=null)
                    { 
                        await HttpContext.SignOutAsync();
                        return View("Authentication");
                    }
                }


            }
            return RedirectToAction("Index", "Home");
            
            } 

        } 
}

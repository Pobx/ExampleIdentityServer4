using System.Threading.Tasks;
using AuthServer.Constants;
using AuthServer.Data.Identity;
using AuthServer.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers {

  [Route ("api/[controller]")]
  public class AccountController : Controller {
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IClientStore _clientStore;
    private readonly IEventService _events;

    public AccountController (SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider schemeProvider, IClientStore clientStore, IEventService events) {
      _userManager = userManager;
      _interaction = interaction;
      _schemeProvider = schemeProvider;
      _clientStore = clientStore;
      _events = events;
      _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost ("Register")]
    public async Task<IActionResult> Register ([FromBody] RegisterRequestViewModel model) {

      var user = new AppUser { UserName = model.Email, Name = model.Name, Email = model.Email };

      var result = await _userManager.CreateAsync (user, model.Password);

      if (!result.Succeeded) return BadRequest (result.Errors);

      await _userManager.AddClaimAsync (user, new System.Security.Claims.Claim ("userName", user.UserName));
      await _userManager.AddClaimAsync (user, new System.Security.Claims.Claim ("name", user.Name));
      await _userManager.AddClaimAsync (user, new System.Security.Claims.Claim ("email", user.Email));
      await _userManager.AddClaimAsync (user, new System.Security.Claims.Claim ("role", Roles.Consumer));

      return Created ("", new RegisterResponseViewModel (user));
    }

    [AllowAnonymous]
    [HttpPost ("Login")]
    public async Task<IActionResult> Login ([FromBody] RegisterRequestViewModel model) {

      var user = new AppUser { UserName = model.Email, Name = model.Name, Email = model.Email };

      var result = await _userManager.CreateAsync (user, model.Password);

      if (!result.Succeeded) return BadRequest (result.Errors);

      await _userManager.AddClaimAsync (user, new System.Security.Claims.Claim ("userName", user.UserName));
      await _userManager.AddClaimAsync (user, new System.Security.Claims.Claim ("name", user.Name));
      await _userManager.AddClaimAsync (user, new System.Security.Claims.Claim ("email", user.Email));
      await _userManager.AddClaimAsync (user, new System.Security.Claims.Claim ("role", Roles.Consumer));

      return Created ("", new RegisterResponseViewModel (user));
    }
  }
}
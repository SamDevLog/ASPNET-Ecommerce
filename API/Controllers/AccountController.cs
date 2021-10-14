using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    public class AccountController : BaseApiController {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper) {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser () {
            //var email = HttpContext.User?.Claims?.FirstOrDefault (x => x.Type == ClaimTypes.Email)?.Value;

            var user = await userManager.FindByEmailFromClaimsPrincipalAsync (User);

            return new UserDto {
                Email = user.Email,
                    Token = tokenService.CreateToken (user),
                    DisplayName = user.DisplayName
            };
        }

        [HttpGet ("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync ([FromQuery] string email) {
            //helper function for client project
            return await userManager.FindByEmailAsync (email) != null;
        }

        [Authorize]
        [HttpGet ("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress () {
            //var email = User.FindFirstValue (ClaimTypes.Email);
            var user = await userManager.FindUserByClaimsPrincipalWithAddressAsync (User);

            return mapper.Map<Address, AddressDto> (user.Address);
        }

        [Authorize]
        [HttpPut ("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress (AddressDto address) {
            var user = await userManager.FindUserByClaimsPrincipalWithAddressAsync (User);

            user.Address = mapper.Map<AddressDto, Address> (address);

            var result = await userManager.UpdateAsync (user);

            if (result.Succeeded) {
                return Ok (mapper.Map<Address, AddressDto> (user.Address));
            }

            return BadRequest ("Problem updating the user");
        }

        [HttpPost ("login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto) {
            var user = await userManager.FindByEmailAsync (loginDto.Email);

            if (user == null) return Unauthorized (new ApiResponse (401));

            var result = await signInManager.CheckPasswordSignInAsync (user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized (new ApiResponse (401));

            return new UserDto {
                Email = user.Email,
                    Token = tokenService.CreateToken (user),
                    DisplayName = user.DisplayName
            };
        }

        [HttpPost ("register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto) {

            if (CheckEmailExistsAsync (registerDto.Email).Result.Value) {
                return new BadRequestObjectResult (new ApiValidationErrorResponse { Errors = new [] { "Email address is in use" } });
            }

            var user = new AppUser {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await userManager.CreateAsync (user, registerDto.Password);

            if (!result.Succeeded) return BadRequest (new ApiResponse (400));

            return new UserDto {
                DisplayName = user.DisplayName,
                    Token = tokenService.CreateToken (user),
                    Email = user.Email
            };
        }

    }
}
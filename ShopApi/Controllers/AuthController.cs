using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Filters;
using ShopApi.Models;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _authRepo;

        public AuthController(IGenericUnitOfWork uow, IMapper mapper,
                            IAuthRepository authRepo, IConfiguration config) : base(uow, mapper)
        {
            _config = config;
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            if (!string.IsNullOrEmpty(userForRegisterDto.UserName))
                userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if (await _authRepo.UserExists(userForRegisterDto.UserName))
            {
                ModelState.AddModelError("UserName", "User exists");
            }

            var newUserRepo = _mapper.Map<User>(userForRegisterDto);

            var createUser = await _authRepo.Register(newUserRepo, userForRegisterDto.Password);

            var usertToReturn = _mapper.Map<UserForListDto>(createUser);

            return CreatedAtRoute("GetUserId", new { Controller = "Users", id = createUser.Id },usertToReturn);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authRepo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            //generate toke
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name ,userFromRepo.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var user = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new
            {
                tokenString,
                user
            });
        }
    }
}
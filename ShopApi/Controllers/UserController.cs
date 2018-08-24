using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Filters;
using ShopApi.Models;
using AutoMapper;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace ShopApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {

        public UserController(IGenericUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _uow.Repository<User>().Get();

            var usersToReturn = _mapper.Map<IEnumerable<UserDetailDto>>(users);

            return Json(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUserId")]
        public async Task<IActionResult> GetUserId(int id)
        {
            var user = await _uow.Repository<User>().GetByIDAsync(x => x.Id == id, x=> x.Photos);

            var userToReturn = _mapper.Map<UserDetailDto>(user);

            return Json(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody]UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var userFromRepo = await _uow.Repository<User>().GetByIDAsync(x => x.Id == id);
            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _uow.SaveChangesAsync())
                return NoContent();

            throw new Exception($"Zmiana danych użytkownika {userFromRepo.UserName} nie powiodła się");
        }
    }
}
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

namespace ShopApi.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ValidateModel]
    public class UserController : BaseController
    {
        private IMapper _mapper;

        public UserController(IGenericUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _uow.Repository<User>().Get();

            var usersToReturn = _mapper.Map<IEnumerable<UserDetailedDto>>(users);

            return Json(usersToReturn);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserId(int id)
        {
            var user = await _uow.Repository<User>().GetByID(id);

            var userToReturn = _mapper.Map<UserDetailedDto>(user);

            return Json(userToReturn);
        }
    }
}
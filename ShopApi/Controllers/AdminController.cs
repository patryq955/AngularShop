using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Models;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : BaseController
    {
        public AdminController(IGenericUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("userWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await _uow.Repository<UserRole>().GetListAsync(
                null,
                null,
                x => x.Role, x => x.User
            );

            var users = userList.Select( x=> new{
                Id = x.User.Id,
                UserName = x.User.UserName,
                Roles = x.Role.Name
            });

            return Ok(users);
        }
    }
}
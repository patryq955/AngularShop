using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Filters;
using ShopApi.Models;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    // [Authorize]
    public class HouseController : BaseController
    {
        DataContext _db;
        public HouseController(IGenericUnitOfWork uow, IMapper mapper, DataContext db) : base(uow, mapper)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHouse(int id)
        {
            var house = await _uow.Repository<House>().GetByID(x=> x.Id == id, x => x.User);

            var houseToReturn = _mapper.Map<HouseDetailDto>(house);
            return Json(houseToReturn);
        }

        [HttpGet("getHouses")]
        public async Task<IActionResult> GetHouses()
        {
            var houses = await _uow.Repository<House>().Get(null,null,x => x.User);

            var housesToReturn = _mapper.Map<IEnumerable<HouseForListDto>>(houses);

            return Json(housesToReturn);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Filters;
using ShopApi.Helpers;
using ShopApi.Models;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var house = await _uow.Repository<House>().GetByIDAsync(x => x.Id == id, x => x.User);

            var houseToReturn = _mapper.Map<HouseDetailDto>(house);
            return Json(houseToReturn);
        }

        [HttpGet("getHouses")]
        public async Task<IActionResult> GetHouses([FromQuery]TParams tParams, [FromQuery] HouseDetailDto houseDetailDto, [FromQuery] string orderBy)
        {
            var houses = await _uow.Repository<House>().GetPagedListAsync(
                    tParams,
                    GetOrder(orderBy),
                    FilterHouses(houseDetailDto),
                    x => x.User
                );

            var housesToReturn = _mapper.Map<IEnumerable<HouseForListDto>>(houses);

            Response.AddPagination(houses.CurrentPage, houses.PageSize, houses.TotalCount, houses.TotalPages);

            return Json(housesToReturn);
        }


        #region private method

        private Func<IQueryable<House>, IOrderedQueryable<House>> GetOrder(string orderBy)
        {
            switch (orderBy) 
            {
                case "city":
                    return q => q.OrderByDescending(x => x.City);
                case "value":
                    return q => q.OrderByDescending(x => x.Value);
                default:
                    return q => q.OrderByDescending(x => x.Id);
            }
        }

        private Expression<Func<House, bool>> FilterHouses(HouseDetailDto houseDetailDto)
        {
            return
            x =>
            (x.numberRooms == houseDetailDto.numberRooms || houseDetailDto.numberRooms == 0)
            && (x.Value == houseDetailDto.Value || houseDetailDto.Value == 0);
        }

        #endregion
    }
}
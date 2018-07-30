using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Data;
using ShopApi.Filters;
using ShopApi.Models;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class HouseController : Controller
    {
        private IGenericUnitOfWork _uow;

        public HouseController(IGenericUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("GetHouse")]
        public async Task<IActionResult> GetHouse(int id)
        {
            var test = _uow.Repository<House>().GetByID(id);
            return Json(test);
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}
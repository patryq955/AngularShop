using Microsoft.AspNetCore.Mvc;
using ShopApi.Data;

namespace ShopApi.Controllers
{
    public class BaseController : Controller
    {
        protected IGenericUnitOfWork _uow;

        public BaseController(IGenericUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Data;
using ShopApi.Helpers;

namespace ShopApi.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected IGenericUnitOfWork _uow;
        protected IMapper _mapper;

        public BaseController(IGenericUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
    }
}
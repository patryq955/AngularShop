using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Data;

namespace ShopApi.Controllers
{
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
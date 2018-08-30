using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopApi.Data;
using Microsoft.Extensions.DependencyInjection;
using ShopApi.Models;
using System;

namespace ShopApi.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var repo = resultContext.HttpContext.RequestServices.GetService<IGenericUnitOfWork>();

            var user = repo.Repository<User>().GetByID(x => x.Id == userId);
            user.LastActive = DateTime.Now;
            await repo.SaveChangesAsync();



        }
    }
}
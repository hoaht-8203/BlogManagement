using System;
using blog_server.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace blog_server.CustomAttributes;

public class TransactionAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var dbContext =
            context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

        using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var result = await next();
            if (result.Exception == null)
            {
                await transaction.CommitAsync();
            }
            else
            {
                await transaction.RollbackAsync();
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

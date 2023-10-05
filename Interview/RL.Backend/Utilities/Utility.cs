using MediatR;
using Microsoft.AspNetCore.Mvc; 
using RL.BackEnd.Common.Model;

namespace RL.Backend.Utilities
{
   public static class ApiResponseExtensions
        {
            public static IActionResult ToActionResult<T>(this ApiResponse<T> response) where T : new()
            {
                if (!response.Succeeded)
                    return new BadRequestObjectResult(response.Exception);
                else if (typeof(T) == typeof(Unit) || response.Value is null)
                    return new OkResult();
                else
                    return new OkObjectResult(response.Value);
            }
        }
     
}

using FlexiApi.Utils;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

public abstract class FlexiController : Controller
{

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        return result switch
        {
            { IsFailed: true } => BadRequest(ApiResponse<T>.Error(result.Errors.ElementAt(0).Message)),
            { IsSuccess: true } => Ok(ApiResponse<T>.Success(result.Value)),
            _ => Ok()
        };
    }
    
    protected IActionResult HandleResult(Result result)
    {
        return result switch
        {
            { IsFailed: true } => BadRequest(ApiResponse<string>.Error(result.Errors.ElementAt(0).Message)),
            { IsSuccess: true } => Ok(ApiResponse<string>.Success(result.Successes.ElementAt(0).Message)),
            _ => Ok()
        };
    }
}
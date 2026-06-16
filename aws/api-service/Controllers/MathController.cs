using Microsoft.AspNetCore.Mvc;

namespace APIService.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MathController(IConfiguration configuration) : ControllerBase
{
    private readonly string serviceDisplayName = configuration["Service:DisplayName"] ?? "API Service";

    [HttpGet("add")]
    public ActionResult<MathOperationResult> Add([FromQuery] double left, [FromQuery] double right)
    {
        return Ok(new MathOperationResult(left, right, "+", left + right));
    }

    [HttpGet("subtract")]
    public ActionResult<MathOperationResult> Subtract([FromQuery] double left, [FromQuery] double right)
    {
        return Ok(new MathOperationResult(left, right, "-", left - right));
    }

    [HttpGet("multiply")]
    public ActionResult<MathOperationResult> Multiply([FromQuery] double left, [FromQuery] double right)
    {
        return Ok(new MathOperationResult(left, right, "*", left * right));
    }

    [HttpGet("divide")]
    public ActionResult<MathOperationResult> Divide([FromQuery] double left, [FromQuery] double right)
    {
        if (right == 0)
        {
            return BadRequest(new
            {
                Error = "Division by zero is not allowed."
            });
        }

        return Ok(new MathOperationResult(left, right, "/", left / right));
    }

    [HttpGet("configuration")]
    public ActionResult<ServiceConfigurationResult> Configuration()
    {
        return Ok(new ServiceConfigurationResult(serviceDisplayName));
    }
}

public sealed record MathOperationResult(double Left, double Right, string Operator, double Result);

public sealed record ServiceConfigurationResult(string ServiceDisplayName);

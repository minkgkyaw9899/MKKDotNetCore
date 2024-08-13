using Microsoft.AspNetCore.Mvc;
using MKKDotNetCore.SnakeApi.Models;

namespace MKKDotNetCore.SnakeApi.Domains;

[ApiController]
[Route("[controller]")]
public class SnakeController: BaseController
{
    private readonly SnakeDl _snakeDl = new SnakeDl();

    [HttpGet]
    public async Task<IActionResult> GetAllSnakes()
    {
        // var snakes = await _snakeDl.GetSnakes();
        var snakes1 =  _snakeDl.GetSnakes();
        
        var snakes2 =  _snakeDl.GetSnakes();

         await Task.WhenAll(snakes1, snakes2);

        // return Ok(snakes1.Result);
        // return StatusCode(200, snakes1.Result);
        return OkResponse(new
        {
            status = 200,
            data = snakes1.Result
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSnakeById(int id)
    {
        try
        {
            var snake = await _snakeDl.GetSnake(id);

            if (snake.Count == 0)
            {
                return NotFound();
            }
            
            return OkResponse(new
            {
                status = 200,
                data = snake.FirstOrDefault()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class BaseController : ControllerBase
{
    protected IActionResult OkResponse(object ojb)
    {
        return StatusCode(200, ojb);
    }
}
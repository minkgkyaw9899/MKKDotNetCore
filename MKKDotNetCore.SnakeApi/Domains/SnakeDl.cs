using Microsoft.AspNetCore.Http.HttpResults;
using MKKDotNetCore.SnakeApi.Models;
using Refit;

namespace MKKDotNetCore.SnakeApi.Domains;

public class SnakeDl
{
    private readonly ISnakeApi _service = RestService.For<ISnakeApi>("https://burma-project-ideas.vercel.app/");

    public SnakeDl()
    {
    }

    public async Task<IEnumerable<SnakeModel>> GetSnakes()
    {
        try
        {
            IEnumerable<SnakeModel> snakes = await _service.GetSnakes();

            return snakes;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }
    
    public async Task<List<SnakeModel>> GetSnake(int id)
    {
        try
        {
            var snake = await _service.GetSnakeById(id);

            return snake!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }
}
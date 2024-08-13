using MKKDotNetCore.SnakeApi.Models;
using Refit;

namespace MKKDotNetCore.SnakeApi.Domains;

public interface ISnakeApi
{
    [Get("/snakes")]
    Task<IEnumerable<SnakeModel>> GetSnakes();

    [Get("/snakes/{id}")]
    Task<List<SnakeModel>> GetSnakeById(int id);
}
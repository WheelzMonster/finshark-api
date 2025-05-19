using finshark_api.Models;

namespace finshark_api.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
}
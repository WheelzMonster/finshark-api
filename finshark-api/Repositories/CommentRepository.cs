using finshark_api.Data;
using finshark_api.Interfaces;
using finshark_api.Models;
using Microsoft.EntityFrameworkCore;

namespace finshark_api.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _dbContext;

    public CommentRepository(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Comment>> GetAllAsync()
    {
        return await _dbContext.Comments.ToListAsync();
    }
}
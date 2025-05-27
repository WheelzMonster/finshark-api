using finshark_api.Data;
using finshark_api.DTOs.Comment;
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


    public async Task<List<Comment?>> GetAllAsync()
    {
        return await _dbContext.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _dbContext.Comments.FindAsync(id);
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _dbContext.Comments.AddAsync(commentModel);
        await _dbContext.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto)
    {
        var comment = await _dbContext.Comments.FindAsync(id);
        if (comment == null)
        {
            return null;
        }
        comment.Title = updateCommentRequestDto.Title;
        comment.Content = updateCommentRequestDto.Content;
        
        await _dbContext.SaveChangesAsync();
        return comment;
    }
}
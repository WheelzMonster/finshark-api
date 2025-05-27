﻿using finshark_api.DTOs.Comment;
using finshark_api.Models;

namespace finshark_api.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment?>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto);
    Task<Comment?> DeleteAsync(int id);
}
﻿using finshark_api.DTOs.Comment;
using finshark_api.Models;

namespace finshark_api.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto()
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId

        };
    }
    
    public static Comment ToCommentFromCreate(this CreateCommentRequestDto createdCommentModel, int stockId)
    {
        return new Comment()
        {
            Title = createdCommentModel.Title,
            Content = createdCommentModel.Content,
            StockId = stockId
        };
    }
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using Application_Layer.Common;
using Application_Layer.Conversation;
using Application_Layer.DTO_s.Request;
using Application_Layer.DTO_s.Response;
using Application_Layer.Services.Interfaces;
using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories;
using Core_Layer.InterfaceRepositories.IGenericRepo;
using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;
using FluentValidation;
using Microsoft.VisualBasic;

namespace ChatX.Application.Conversations;

public class ConversationService : IConversationService
{

    private readonly IUnitOfWork _uow;
    private readonly IValidator<ConversationRequest> _validator;
    private readonly IConversationAccessGuard _guard;

    public ConversationService(IConversationRepo conversations, IUnitOfWork uow, IValidator<ConversationRequest> validator, IConversationAccessGuard guard)
    {

        _uow = uow;
        _validator = validator;
        _guard = guard;
    }


    public async Task<ConversationResponse> StartConversationAsync(Guid currentUserId, ConversationRequest request)
    {
        await _validator.ValidateAndThrowAsync(request);


        if (!await _uow.UserRepo.IsActiveUserAsync(currentUserId) || !await _uow.UserRepo.IsActiveUserAsync(request.ParticipantId))
            throw new InvalidOperationException("Both users must exist and be active.");

        if (request.ParticipantId == currentUserId)
            throw new InvalidOperationException("You cannot start a conversation with yourself.");


        var existingId = await _uow.ConversationRepo.FindPrivateConversationIdAsync(currentUserId, request.ParticipantId);

        Guid conversationId;
        if (existingId is not null)
        {
            conversationId = existingId.Value;
        }
        else
        {
            var now = DateTime.UtcNow;
            var conversation = new Conversation { CreatedAt = now, IsActive = true };
            var members = new List<ConversationMember> {

            new ConversationMember { ConversationId = conversation.ConversationId, UserId = currentUserId, JoinedAt = now, IsActive = true },
            new ConversationMember { ConversationId = conversation.ConversationId, UserId = request.ParticipantId, JoinedAt = now, IsActive = true }
            };

            await _uow.ConversationRepo.AddConversationAsync(conversation, members);
            await _uow.SaveChangesAsync();
            conversationId = conversation.ConversationId;
        }
        var currentUser = await _uow.UserRepo.GetByIdAsync(currentUserId);
        var participant = await _uow.UserRepo.GetByIdAsync(request.ParticipantId);

        var participants = new List<ConversationParticipant>
{
    new ConversationParticipant
    {
        UserId = currentUser.UserId,
        DisplayName = currentUser.UserName,
        ProfileImageUrl = currentUser.ProfileImageUrl
    },
    new ConversationParticipant
    {
        UserId = participant.UserId,
        DisplayName = participant.UserName,
        ProfileImageUrl = participant.ProfileImageUrl
    }
};

        return new ConversationResponse
        {
            ConversationId = conversationId,
            CreatedAt = DateTime.UtcNow,
            Participants = participants
        };
    }
    public async Task<ConversationResponse> GetConversationByIdAsync(Guid currentUserId, Guid conversationId)
    {
        await _guard.EnsureActiveMemberAsync(conversationId, currentUserId);

        var conversation = await _uow.ConversationRepo.GetConversationByIdAsync(conversationId)
            ?? throw new ConversationAccessDeniedException();

        var other = await _uow.ConversationRepo.GetOtherParticipantAsync(conversationId, currentUserId)
            ?? throw new ConversationAccessDeniedException();

        return new ConversationResponse
        {
            ConversationId = conversation.ConversationId,

            Participants = new List<ConversationParticipant>
        {
            new ConversationParticipant
            {
                UserId = other.UserId,
                DisplayName = other.UserName,
                ProfileImageUrl = other.ProfileImageUrl
            }
        },

            CreatedAt = conversation.CreatedAt,
            LastMessageAt = conversation.LastMessageAt
        };


    }
}



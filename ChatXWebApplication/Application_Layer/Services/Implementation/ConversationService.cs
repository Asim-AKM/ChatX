using System;
using System.Collections.Generic;
using System.Linq;
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

public  class ConversationService : IConversationService
{
    private readonly IConversationRepo _conversations;
    private readonly IUserLookUp _users;
    private readonly IUnitOfWork _uow;
    private readonly IValidator<ConversationRequest> _validator;

    public ConversationService(IConversationRepo conversations, IUserLookUp users,IUnitOfWork uow, IValidator<ConversationRequest> validator)
    {
        _conversations = conversations; 
        _users = users; _uow = uow; 
        _validator = validator;
    }

    public async Task<ConversationResponse> StartConversationAsync(Guid currentUserId, ConversationRequest request)
    {
        await _validator.ValidateAndThrowAsync(request);


        if (!await _users.IsActiveUserAsync(currentUserId) || !await _users.IsActiveUserAsync(request.ParticipantId))
            throw new InvalidOperationException("Both users must exist and be active.");

        if (request.ParticipantId == currentUserId)
            throw new InvalidOperationException("You cannot start a conversation with yourself.");

       
         var existingId = await _conversations.FindPrivateConversationIdAsync(currentUserId, request.ParticipantId);

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

            new ConversationMember { ConversationId = conversation.Id, UserId = currentUserId, JoinedAt = now, IsActive = true },
            new ConversationMember { ConversationId = conversation.Id, UserId = request.ParticipantId, JoinedAt = now, IsActive = true }
            };

            await _conversations.AddConversationAsync(conversation,members);
                await _uow.SaveChangesAsync();
                conversationId = conversation.Id;
           }
        var currentUser = await _users.GetByIdAsync(currentUserId);
        var participant = await _users.GetByIdAsync (request.ParticipantId);

        var participants = new List<ConversationParticipant>
{
    new ConversationParticipant
    {
        UserId = currentUser.UserId,
        UserName = currentUser.UserName,
        ProfileImageUrl = currentUser.ProfileImageUrl
    },
    new ConversationParticipant
    {
        UserId = participant.UserId,
        UserName = participant.UserName,
        ProfileImageUrl = participant.ProfileImageUrl
    }
};

        return new ConversationResponse
        {
            Id = conversationId,
            CreatedAt = DateTime.UtcNow,
            Participants = participants
        };




    }
        
 }

   

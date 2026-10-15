
using Application_Layer.DTO_s.Request;
using FluentValidation;

namespace Application_Layer.Validators
{   public class StartConversationValidator
            : AbstractValidator<ConversationRequest>
        {
            public StartConversationValidator()
            {
                RuleFor(x => x.ParticipantId)
                    .NotEmpty()
                    .WithMessage("Participant ID is required.");
            }
        }
    }


using CoberfuziFileManager.Domain.DTOs;
using FluentValidation;

namespace CoberfuziFileManager.Domain.Validatores.Client;

public class WorkCompleteDTOValidator : AbstractValidator<WorkCompleteDTO>
{

    public WorkCompleteDTOValidator()
    {
        RuleFor(work => work.ClientID)
            .GreaterThan(0).WithMessage("ClientID must be greater than 0");

        RuleFor(work => work.Address)
            .NotEmpty().WithMessage("Address cannot be empty")
            .MaximumLength(200).WithMessage("Address cannot be more than 200 characters");

        RuleFor(work => work.PostCode)
            .NotEmpty().WithMessage("PostCode cannot be empty")
            .Matches(@"^\d{4}-\d{3}$").WithMessage("PostCode is not valid");
        
        RuleFor(work => work.WorkID)
            .Must(workID => workID == null || workID > 0 || workID == 0)
            .WithMessage("WorkID must be either empty or greater than 0");
    }
    
}
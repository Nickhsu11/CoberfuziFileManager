using CoberfuziFileManager.Domain.DTOs.Supply;
using CoberfuziFileManager.Models;
using FluentValidation;

namespace CoberfuziFileManager.Domain.Validatores.Client;

public class SupplyCompleteDTOValidator : AbstractValidator<SupplyCompleteDTO>
{

    public SupplyCompleteDTOValidator()
    {
        RuleFor(supply => supply.Name )
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(100).WithMessage("Name cannot be more than 100 characters.");
        
        RuleFor(supply => supply.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than zero.");
    }
}
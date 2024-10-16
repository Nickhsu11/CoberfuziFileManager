using System.Data;
using CoberfuziFileManager.Domain.DTOs.Budget;
using CoberfuziFileManager.Models;
using FluentValidation;

namespace CoberfuziFileManager.Domain.Validatores.Client;

public class BudgetCompleteDTOValidator : AbstractValidator<BudgetCompleteDTO>
{
    public BudgetCompleteDTOValidator()
    {
        RuleFor(budget => budget.Value)
            .GreaterThan(0)
            .WithMessage("Value should be greater than 0");
    }
}
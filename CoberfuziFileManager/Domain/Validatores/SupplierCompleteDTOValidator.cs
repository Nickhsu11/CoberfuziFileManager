using CoberfuziFileManager.Domain.DTOs;
using FluentValidation;

namespace CoberfuziFileManager.Domain.Validatores.Client;

public class SupplierCompleteDTOValidator : AbstractValidator<SupplierCompleteDTO>
{
    public SupplierCompleteDTOValidator()
    {
        RuleFor( supplier => supplier.Name )
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(100).WithMessage("Name cannot be more than 100 characters");

        RuleFor(supplier => supplier.Phone)
            .NotEmpty().WithMessage("Phone Cannot be empty")
            .Matches(@"^\+?\d{8,13}$").WithMessage("Phone cannot be more than 13 characters");
        
        RuleFor( supplier => supplier.Email )
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email address is not valid");
        
        RuleFor( supplier => supplier.PostCode )
            .NotEmpty().WithMessage("PostCode cannot be empty")
            .Matches(@"^\d{4}-\d{3}$").WithMessage("PostCode is not valid");

        RuleFor(supplier => supplier.Address)
            .NotEmpty().WithMessage("Address cannot be empty")
            .MaximumLength(200).WithMessage("Address cannot be more than 200 characters");

        RuleFor(supplier => supplier.Nif)
            .InclusiveBetween(100000000, 999999999).WithMessage("NIF must be exactly 9 digits");
    }
}
using FluentValidation;
using FluentValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCart.Validators
{
    public class AddShoppingItemValidator : AbstractValidator<ShoppingItem>
    {
        public AddShoppingItemValidator()
        {
            RuleFor(x => x.Name)
                            .NotEmpty();

            RuleFor(x => x.Price)
                .GreaterThan(0);
        }
    }
}

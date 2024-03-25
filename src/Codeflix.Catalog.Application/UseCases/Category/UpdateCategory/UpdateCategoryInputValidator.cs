using FluentValidation;

namespace Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
public class UpdateCategoryInputValidator : AbstractValidator<UpdateCategoryInput>
{
  public UpdateCategoryInputValidator()
  {
    this.RuleFor(x => x.Id).NotEmpty();
  }
}

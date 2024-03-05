using FluentValidation;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory;
public class GetCategoryInputValidator : AbstractValidator<GetCategoryInput>
{
  public GetCategoryInputValidator()
  {
    this.RuleFor(x => x.Id).NotEmpty();
  }
}

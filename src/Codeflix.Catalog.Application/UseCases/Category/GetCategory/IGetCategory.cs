using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory;
public interface IGetCategory : IRequestHandler<GetCategoryInput, GetCategoryOutput>
{
}

using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Domain.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory;
public class GetCategory : IGetCategory
{
  private readonly ICategoryRepository categoryRepository;

  public GetCategory(ICategoryRepository categoryRepository)
  {
    this.categoryRepository = categoryRepository;
  }

  public async Task<CategoryModelOutput> Handle(GetCategoryInput input, CancellationToken cancellationToken)
  {
    Domain.Entity.Category category = await this.categoryRepository.Get(input.Id, cancellationToken);
    return CategoryModelOutput.FromCategory(category);
  }
}

using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Application.UseCases.Category.ListCategories;
public class ListCategories : IListCategories
{
  private readonly ICategoryRepository categoryRepository;

  public ListCategories(ICategoryRepository categoryRepository)
  {
    this.categoryRepository = categoryRepository;
  }

  public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
  {
    SearchOutput<DomainEntity.Category> searchOutput = await this.categoryRepository.Search(new(request.Page, request.PerPage, request.Search, request.Sort, request.Dir), cancellationToken);
    return new(
      searchOutput.CurrentPage,
      searchOutput.PerPage,
      searchOutput.Total,
      searchOutput.Items.Select(CategoryModelOutput.FromCategory).ToList()
    );
  }
}

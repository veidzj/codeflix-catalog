using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Domain.Repository;
using System.Threading;
using System.Threading.Tasks;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
public class UpdateCategory : IUpdateCategory
{
  private readonly ICategoryRepository categoryRepository;
  private readonly IUnitOfWork unitOfWork;

  public UpdateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
  {
    this.categoryRepository = categoryRepository;
    this.unitOfWork = unitOfWork;
  }

  public async Task<CategoryModelOutput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
  {
    DomainEntity.Category category = await this.categoryRepository.Get(request.Id, cancellationToken);
    category.Update(request.Name, request.Description);
    if (request.IsActive != null && request.IsActive != category.IsActive)
    {
      if ((bool)request.IsActive)
      {
        category.Activate();
      }
      else
      {
        category.Deactivate();
      }
    }
    await this.categoryRepository.Update(category, cancellationToken);
    await this.unitOfWork.Commit(cancellationToken);
    return CategoryModelOutput.FromCategory(category);
  }
}

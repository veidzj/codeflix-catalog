using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Domain.Repository;
using System.Threading;
using System.Threading.Tasks;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
public class CreateCategory : ICreateCategory
{
  private readonly ICategoryRepository categoryRepository;
  private readonly IUnitOfWork unitOfWork;

  public CreateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
  {
    this.categoryRepository = categoryRepository;
    this.unitOfWork = unitOfWork;
  }

  public async Task<CategoryModelOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken)
  {
    DomainEntity.Category category = new(input.Name, input.Description, input.IsActive);
    await this.categoryRepository.Insert(category, cancellationToken);
    await this.unitOfWork.Commit(cancellationToken);
    return CategoryModelOutput.FromCategory(category);
  }
}

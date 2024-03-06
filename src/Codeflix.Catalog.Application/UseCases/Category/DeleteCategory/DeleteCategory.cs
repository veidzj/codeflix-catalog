using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;
public class DeleteCategory : IDeleteCategory
{
  private readonly ICategoryRepository categoryRepository;
  private readonly IUnitOfWork unitOfWork;

  public DeleteCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
  {
    this.categoryRepository = categoryRepository;
    this.unitOfWork = unitOfWork;
  }

  public async Task<Unit> Handle(DeleteCategoryInput request, CancellationToken cancellationToken)
  {
    DomainEntity.Category category = await this.categoryRepository.Get(request.Id, cancellationToken);
    await this.categoryRepository.Delete(category, cancellationToken);
    await this.unitOfWork.Commit(cancellationToken);
    return Unit.Value;
  }
}

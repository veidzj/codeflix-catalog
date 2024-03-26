using Codeflix.Catalog.Application.Exceptions;
using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using UseCase = Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;

namespace Codeflix.Catalog.UnitTests.Application.DeleteCategory;

[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest
{
  private readonly DeleteCategoryTestFixture fixture;

  public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
  {
    this.fixture = fixture;
  }

  [Fact(DisplayName = nameof(DeleteCategory))]
  [Trait("Application", "DeleteCategory - Use Cases")]
  public async Task DeleteCategory()
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.GetUnitOfWorkMock();
    Category category = this.fixture.GetCategory();
    repositoryMock.Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>())).ReturnsAsync(category);
    UseCase.DeleteCategoryInput input = new(category.Id);
    UseCase.DeleteCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);

    await useCase.Handle(input, CancellationToken.None);

    repositoryMock.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
    repositoryMock.Verify(x => x.Delete(category, It.IsAny<CancellationToken>()), Times.Once);
    unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
  }

  [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
  [Trait("Application", "DeleteCategory - Use Cases")]
  public async Task ThrowWhenCategoryNotFound()
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.GetUnitOfWorkMock();
    Guid guid = Guid.NewGuid();
    repositoryMock.Setup(x => x.Get(guid, It.IsAny<CancellationToken>())).ThrowsAsync(new NotFoundException($"Category '${guid} not found"));
    UseCase.DeleteCategoryInput input = new(guid);
    UseCase.DeleteCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);

    Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

    await task.Should().ThrowAsync<NotFoundException>();

    repositoryMock.Verify(x => x.Get(guid, It.IsAny<CancellationToken>()), Times.Once);
  }
}

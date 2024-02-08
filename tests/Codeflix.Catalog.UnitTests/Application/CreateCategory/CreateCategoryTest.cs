using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;
using UseCases = Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
  private readonly CreateCategoryTestFixture fixture;

  public CreateCategoryTest(CreateCategoryTestFixture fixture)
  {
    this.fixture = fixture;
  }

  [Fact(DisplayName = nameof(CreateCategory))]
  [Trait("Application", "CreateCategory - Use Cases")]
  public async void CreateCategory()
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.MakeRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.MakeUnitOfWorkMock();
    UseCases.CreateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);
    UseCases.CreateCategoryInput input = this.fixture.MakeInput();

    UseCases.CreateCategoryOutput output = await useCase.Handle(input, CancellationToken.None);

    repositoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
    unitOfWorkMock.Verify(unityOfWork => unityOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once);
    output.Should().NotBeNull();
    output.Name.Should().Be(input.Name);
    output.Description.Should().Be(input.Description);
    output.IsActive.Should().Be(input.IsActive);
    output.Id.Should().NotBeEmpty();
    output.CreatedAt.Should().NotBeSameDateAs(default);
  }
}

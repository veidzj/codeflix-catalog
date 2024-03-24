using Codeflix.Catalog.Application.Exceptions;
using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using UseCase = Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace Codeflix.Catalog.UnitTests.Application.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
  private readonly UpdateCategoryTestFixture fixture;

  public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
  {
    this.fixture = fixture;
  }

  [Theory(DisplayName = nameof(UpdateCategory))]
  [Trait("Application", "UpdateCategory - Use Cases")]
  [MemberData(
    nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
    parameters: 10,
    MemberType = typeof(UpdateCategoryTestDataGenerator)
  )]
  public async Task UpdateCategory(Category category, UpdateCategoryInput input)
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.GetUnitOfWorkMock();
    repositoryMock.Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>())).ReturnsAsync(category);
    UseCase.UpdateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);

    CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);

    output.Should().NotBeNull();
    output.Name.Should().Be(input.Name);
    output.Description.Should().Be(input.Description);
    output.IsActive.Should().Be((bool)input.IsActive!);
    repositoryMock.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
    repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
    unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
  }

  [Theory(DisplayName = nameof(UpdateCategoryWithoutIsActive))]
  [Trait("Application", "UpdateCategory - Use Cases")]
  [MemberData(
    nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
    parameters: 10,
    MemberType = typeof(UpdateCategoryTestDataGenerator)
  )]
  public async Task UpdateCategoryWithoutIsActive(Category category, UpdateCategoryInput input)
  {
    UpdateCategoryInput inputWithoutIsActive = new(input.Id, input.Name, input.Description);
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.GetUnitOfWorkMock();
    repositoryMock.Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>())).ReturnsAsync(category);
    UseCase.UpdateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);

    CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);

    output.Should().NotBeNull();
    output.Name.Should().Be(input.Name);
    output.Description.Should().Be(input.Description);
    output.IsActive.Should().Be((bool)input.IsActive!);
    repositoryMock.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
    repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
    unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
  }

  [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
  [Trait("Application", "UpdateCategory - Use Cases")]
  public async Task ThrowWhenCategoryNotFound()
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.GetUnitOfWorkMock();
    UpdateCategoryInput input = this.fixture.GetValidInput();
    repositoryMock.Setup(x => x.Get(input.Id, It.IsAny<CancellationToken>())).ThrowsAsync(new NotFoundException($"category '{input.Id}' not found"));
    UseCase.UpdateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);

    Func<Task> task = async() => await useCase.Handle(input, CancellationToken.None);

    await task.Should().ThrowAsync<NotFoundException>();

    repositoryMock.Verify(x => x.Get(input.Id, It.IsAny<CancellationToken>()), Times.Once);
  }
}

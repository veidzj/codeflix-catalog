using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using Xunit;
using UseCases = Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTest
{
  [Fact(DisplayName = nameof(CreateCategory))]
  [Trait("Application", "CreateCategory - Use Cases")]
  public async void CreateCategory()
  {
    Mock<ICategoryRepository> repositoryMock = new();
    Mock<IUnitOfWork> unitOfWorkMock = new();
    UseCases.CreateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);
    UseCases.CreateCategoryInput input = new("Category Name", "Category Description", true);

    UseCases.CreateCategoryOutput output = await useCase.Handle(input, CancellationToken.None);

    repositoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
    unitOfWorkMock.Verify(unityOfWork => unityOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once);
    output.Should().NotBeNull();
    output.Name.Should().Be("Category Name");
    output.Description.Should().Be("Category Description");
    output.IsActive.Should().Be(true);
    output.Id.Should().NotBeEmpty();
    output.CreatedAt.Should().NotBeSameDateAs(default);
  }
}

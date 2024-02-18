using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using UseCase = Codeflix.Catalog.Application.UseCases.Category.GetCategory;

namespace Codeflix.Catalog.UnitTests.Application.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
  private readonly GetCategoryTestFixture fixture;

  public GetCategoryTest(GetCategoryTestFixture fixture)
  {
    this.fixture = fixture;
  }

  [Fact(DisplayName = nameof(GetCategory))]
  [Trait("Application", "GetCategory - Use Cases")]
  public async Task GetCategory()
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Category category = this.fixture.GetValidCategory();
    repositoryMock.Setup(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(category);
    UseCase.GetCategoryInput input = new(category.Id);
    UseCase.GetCategory useCase = new(repositoryMock.Object);

    CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);

    repositoryMock.Verify(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

    output.Should().NotBeNull();
    output.Name.Should().Be(category.Name);
    output.Description.Should().Be(category.Description);
    output.IsActive.Should().Be(category.IsActive);
    output.Id.Should().Be(category.Id);
    output.CreatedAt.Should().Be(category.CreatedAt);
  }
}

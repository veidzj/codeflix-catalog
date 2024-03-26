using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using UseCase = Codeflix.Catalog.Application.UseCases.Category.ListCategories;

namespace Codeflix.Catalog.UnitTests.Application.ListCategories;

[Collection(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTest
{
  private readonly ListCategoriesTestFixture fixture;

  public ListCategoriesTest(ListCategoriesTestFixture fixture)
  {
    this.fixture = fixture;
  }

  [Fact(DisplayName = nameof(List))]
  [Trait("Application", "ListCategories - Use Cases")]
  public async Task List()
  {
    List<Category> categoriesList = this.fixture.GetCategoriesList();
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    ListCategoriesInput input = this.fixture.GetListCategoriesInput();
    SearchOutput<Category> outputRepositorySearch = new(
      currentPage: input.Page,
      perPage: input.PerPage,
      items: categoriesList,
      total: new Random().Next(50, 200)
    );
    repositoryMock.Setup(x => x.Search(
      It.Is<SearchInput>(
        searchInput => searchInput.Page == input.Page
        && searchInput.PerPage == input.PerPage
        && searchInput.Search == input.Search
        && searchInput.OrderBy == input.Sort
        && searchInput.Order == input.Dir),
      It.IsAny<CancellationToken>()
    )).ReturnsAsync(outputRepositorySearch);
    UseCase.ListCategories useCase = new(repositoryMock.Object);

    ListCategoriesOutput output = await useCase.Handle(input, CancellationToken.None);

    output.Should().NotBeNull();
    output.Page.Should().Be(outputRepositorySearch.CurrentPage);
    output.PerPage.Should().Be(outputRepositorySearch.PerPage);
    output.Total.Should().Be(outputRepositorySearch.Total);
    output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
    ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
    {
      Category? repositoryCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
      outputItem.Should().NotBeNull();
      outputItem.Name.Should().Be(repositoryCategory!.Name);
      outputItem.Description.Should().Be(repositoryCategory.Description);
      outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
      outputItem.Id.Should().Be(repositoryCategory.Id);
      outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
    });
    repositoryMock.Verify(x => x.Search(
      It.Is<SearchInput>(
        searchInput => searchInput.Page == input.Page
        && searchInput.PerPage == input.PerPage
        && searchInput.Search == input.Search
        && searchInput.OrderBy == input.Sort
        && searchInput.Order == input.Dir),
      It.IsAny<CancellationToken>()
    ), Times.Once);
  }

  [Theory(DisplayName = nameof(ListInputWithoutParameters))]
  [Trait("Application", "ListCategories - Use Cases")]
  [MemberData(
    nameof(ListCategoriesTestDataGenerator.GetInputsWithoutParameters),
    parameters: 10,
    MemberType = typeof(ListCategoriesTestDataGenerator)
  )]
  public async Task ListInputWithoutParameters(ListCategoriesInput input)
  {
    List<Category> categoriesList = this.fixture.GetCategoriesList();
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    SearchOutput<Category> outputRepositorySearch = new(
      currentPage: input.Page,
      perPage: input.PerPage,
      items: categoriesList,
      total: new Random().Next(50, 200)
    );
    repositoryMock.Setup(x => x.Search(
      It.Is<SearchInput>(
        searchInput => searchInput.Page == input.Page
        && searchInput.PerPage == input.PerPage
        && searchInput.Search == input.Search
        && searchInput.OrderBy == input.Sort
        && searchInput.Order == input.Dir),
      It.IsAny<CancellationToken>()
    )).ReturnsAsync(outputRepositorySearch);
    UseCase.ListCategories useCase = new(repositoryMock.Object);

    ListCategoriesOutput output = await useCase.Handle(input, CancellationToken.None);

    output.Should().NotBeNull();
    output.Page.Should().Be(outputRepositorySearch.CurrentPage);
    output.PerPage.Should().Be(outputRepositorySearch.PerPage);
    output.Total.Should().Be(outputRepositorySearch.Total);
    output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
    ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
    {
      Category? repositoryCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
      outputItem.Should().NotBeNull();
      outputItem.Name.Should().Be(repositoryCategory!.Name);
      outputItem.Description.Should().Be(repositoryCategory.Description);
      outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
      outputItem.Id.Should().Be(repositoryCategory.Id);
      outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
    });
    repositoryMock.Verify(x => x.Search(
      It.Is<SearchInput>(
        searchInput => searchInput.Page == input.Page
        && searchInput.PerPage == input.PerPage
        && searchInput.Search == input.Search
        && searchInput.OrderBy == input.Sort
        && searchInput.Order == input.Dir),
      It.IsAny<CancellationToken>()
    ), Times.Once);
  }
}

using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

  [Fact(DisplayName = nameof(CreateCategoryWithOnlyName))]
  [Trait("Application", "CreateCategory - Use Cases")]
  public async void CreateCategoryWithOnlyName()
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.MakeRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.MakeUnitOfWorkMock();
    UseCases.CreateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);
    UseCases.CreateCategoryInput input = new(this.fixture.MakeValidCategoryName());

    UseCases.CreateCategoryOutput output = await useCase.Handle(input, CancellationToken.None);

    repositoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
    unitOfWorkMock.Verify(unityOfWork => unityOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once);
    output.Should().NotBeNull();
    output.Name.Should().Be(input.Name);
    output.Description.Should().BeEmpty();
    output.IsActive.Should().BeTrue();
    output.Id.Should().NotBeEmpty();
    output.CreatedAt.Should().NotBeSameDateAs(default);
  }

  [Theory(DisplayName = nameof(ThrowWhenInstantiateAggregateThrows))]
  [Trait("Application", "CreateCategory - Use Cases")]
  [MemberData(nameof(MakeInvalidInputs))]
  public async void ThrowWhenInstantiateAggregateThrows(CreateCategoryInput input, string exceptionMessage)
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.MakeRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.MakeUnitOfWorkMock();
    UseCases.CreateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);

    Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

    await task.Should().ThrowAsync<EntityValidationException>().WithMessage(exceptionMessage);
  }

  public static IEnumerable<object[]> MakeInvalidInputs()
  {
    CreateCategoryTestFixture fixture = new();
    List<object[]> invalidInputs = [];

    CreateCategoryInput invalidInputShortName = fixture.MakeInput();
    invalidInputShortName.Name = invalidInputShortName.Name[..2];
    invalidInputs.Add(
    [
      invalidInputShortName,
      "Name should be at least 3 characters long"
    ]);

    CreateCategoryInput invalidInputLongName = fixture.MakeInput();
    string longName = fixture.Faker.Commerce.ProductName();
    while (longName.Length <= 255)
    {
      longName = $"{longName} {fixture.Faker.Commerce.ProductName()}";
    }
    invalidInputLongName.Name = longName;
    invalidInputs.Add(
    [
      invalidInputLongName,
      "Name should be less or equal 255 characters long"
    ]);

    CreateCategoryInput invalidInputDescriptionNull = fixture.MakeInput();
    invalidInputDescriptionNull.Description = null!;
    invalidInputLongName.Name = longName;
    invalidInputs.Add(
    [
      invalidInputDescriptionNull,
      "Description should not be null"
    ]);

    CreateCategoryInput invalidInputLongDescription = fixture.MakeInput();
    string longDescription = fixture.Faker.Commerce.ProductDescription();
    while (longDescription.Length <= 10_000)
    {
      longDescription = $"{longDescription} {fixture.Faker.Commerce.ProductDescription()}";
    }
    invalidInputLongDescription.Description = longDescription;
    invalidInputs.Add(
    [
      invalidInputLongDescription,
      "Description should be less or equal 10000 characters long"
    ]);
    return invalidInputs;
  }
}

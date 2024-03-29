﻿using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;
using UseCases = Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

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
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.GetUnitOfWorkMock();
    UseCases.CreateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);
    CreateCategoryInput input = this.fixture.GetInput();

    CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);

    repositoryMock.Verify(repository => repository.Insert(It.IsAny<DomainEntity.Category>(), It.IsAny<CancellationToken>()), Times.Once);
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
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.GetUnitOfWorkMock();
    UseCases.CreateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);
    CreateCategoryInput input = new(this.fixture.GetCategoryName());

    CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);

    repositoryMock.Verify(repository => repository.Insert(It.IsAny<DomainEntity.Category>(), It.IsAny<CancellationToken>()), Times.Once);
    unitOfWorkMock.Verify(unityOfWork => unityOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once);
    output.Should().NotBeNull();
    output.Name.Should().Be(input.Name);
    output.Description.Should().BeEmpty();
    output.IsActive.Should().BeTrue();
    output.Id.Should().NotBeEmpty();
    output.CreatedAt.Should().NotBeSameDateAs(default);
  }

  [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameAndDescription))]
  [Trait("Application", "CreateCategory - Use Cases")]
  public async void CreateCategoryWithOnlyNameAndDescription()
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.GetUnitOfWorkMock();
    UseCases.CreateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);
    CreateCategoryInput input = new(this.fixture.GetCategoryName(), this.fixture.GetCategoryDescription());

    CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);

    repositoryMock.Verify(repository => repository.Insert(It.IsAny<DomainEntity.Category>(), It.IsAny<CancellationToken>()), Times.Once);
    unitOfWorkMock.Verify(unityOfWork => unityOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once);
    output.Should().NotBeNull();
    output.Name.Should().Be(input.Name);
    output.Description.Should().Be(input.Description);
    output.IsActive.Should().BeTrue();
    output.Id.Should().NotBeEmpty();
    output.CreatedAt.Should().NotBeSameDateAs(default);
  }

  [Theory(DisplayName = nameof(ThrowWhenInstantiateCategoryThrows))]
  [Trait("Application", "CreateCategory - Use Cases")]
  [MemberData(
    nameof(CreateCategoryTestDataGenerator.GetInvalidInputs),
    parameters: 24,
    MemberType = typeof(CreateCategoryTestDataGenerator)
  )]
  public async void ThrowWhenInstantiateCategoryThrows(CreateCategoryInput input, string exceptionMessage)
  {
    Mock<ICategoryRepository> repositoryMock = this.fixture.GetRepositoryMock();
    Mock<IUnitOfWork> unitOfWorkMock = this.fixture.GetUnitOfWorkMock();
    UseCases.CreateCategory useCase = new(repositoryMock.Object, unitOfWorkMock.Object);

    Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

    await task.Should().ThrowAsync<EntityValidationException>().WithMessage(exceptionMessage);
  }
}

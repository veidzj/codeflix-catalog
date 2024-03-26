using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Codeflix.Catalog.UnitTests.Application.Common;
using System;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.UpdateCategory;
public class UpdateCategoryTestFixture : CategoryUseCasesBaseFixture
{
  public UpdateCategoryInput GetInput(Guid? id = null)
  {
    return new(
      id ?? Guid.NewGuid(),
      this.GetCategoryName(),
      this.GetCategoryDescription(),
      this.GetRandomBoolean()
    );
  }

  public UpdateCategoryInput GetInvalidInputShortName()
  {
    UpdateCategoryInput invalidInputShortName = this.GetInput();
    invalidInputShortName.Name = invalidInputShortName.Name[..2];
    return invalidInputShortName;
  }

  public UpdateCategoryInput GetInvalidInputLongName()
  {
    UpdateCategoryInput invalidInputLongName = this.GetInput();
    string longName = this.Faker.Commerce.ProductName();
    while (longName.Length <= 255)
    {
      longName = $"{longName} {this.Faker.Commerce.ProductName()}";
    }
    invalidInputLongName.Name = longName;
    return invalidInputLongName;
  }

  public UpdateCategoryInput GetInvalidInputLongDescription()
  {
    UpdateCategoryInput invalidInputLongDescription = this.GetInput();
    string longDescription = this.Faker.Commerce.ProductDescription();
    while (longDescription.Length <= 10_000)
    {
      longDescription = $"{longDescription} {this.Faker.Commerce.ProductDescription()}";
    }
    invalidInputLongDescription.Description = longDescription;
    return invalidInputLongDescription;
  }
}

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture> { }

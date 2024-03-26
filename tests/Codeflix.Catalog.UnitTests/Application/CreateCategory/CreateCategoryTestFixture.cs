using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.UnitTests.Application.Common;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTestFixture : CategoryUseCasesBaseFixture
{
  public CreateCategoryInput GetInput()
  {
    return new(this.GetCategoryName(), this.GetCategoryDescription(), this.GetRandomBoolean());
  }

  public CreateCategoryInput GetInvalidInputShortName()
  {
    CreateCategoryInput invalidInputShortName = this.GetInput();
    invalidInputShortName.Name = invalidInputShortName.Name[..2];
    return invalidInputShortName;
  }

  public CreateCategoryInput GetInvalidInputLongName()
  {
    CreateCategoryInput invalidInputLongName = this.GetInput();
    string longName = this.Faker.Commerce.ProductName();
    while (longName.Length <= 255)
    {
      longName = $"{longName} {this.Faker.Commerce.ProductName()}";
    }
    invalidInputLongName.Name = longName;
    return invalidInputLongName;
  }

  public CreateCategoryInput GetInvalidInputNullDescription()
  {
    CreateCategoryInput invalidInputNullDescription = this.GetInput();
    invalidInputNullDescription.Description = null!;
    return invalidInputNullDescription;
  }

  public CreateCategoryInput GetInvalidInputLongDescription()
  {
    CreateCategoryInput invalidInputLongDescription = this.GetInput();
    string longDescription = this.Faker.Commerce.ProductDescription();
    while (longDescription.Length <= 10_000)
    {
      longDescription = $"{longDescription} {this.Faker.Commerce.ProductDescription()}";
    }
    invalidInputLongDescription.Description = longDescription;
    return invalidInputLongDescription;
  }
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }


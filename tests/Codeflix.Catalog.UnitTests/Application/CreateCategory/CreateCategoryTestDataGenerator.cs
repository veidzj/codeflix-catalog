using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using System.Collections.Generic;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTestDataGenerator
{
  public static IEnumerable<object[]> GetInvalidInputs()
  {
    CreateCategoryTestFixture fixture = new();
    List<object[]> invalidInputs = [];

    CreateCategoryInput invalidInputShortName = fixture.GetInput();
    invalidInputShortName.Name = invalidInputShortName.Name[..2];
    invalidInputs.Add(
    [
      invalidInputShortName,
      "Name should be at least 3 characters long"
    ]);

    CreateCategoryInput invalidInputLongName = fixture.GetInput();
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

    CreateCategoryInput invalidInputDescriptionNull = fixture.GetInput();
    invalidInputDescriptionNull.Description = null!;
    invalidInputLongName.Name = longName;
    invalidInputs.Add(
    [
      invalidInputDescriptionNull,
      "Description should not be null"
    ]);

    CreateCategoryInput invalidInputLongDescription = fixture.GetInput();
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

using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.UnitTests.Application.CreateCategory;
using System.Collections.Generic;

namespace Codeflix.Catalog.UnitTests.Application.UpdateCategory;
public class UpdateCategoryTestDataGenerator
{
  public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
  {
    UpdateCategoryTestFixture fixture = new();
    for (int index = 0; index < times; index++)
    {
      Category category = fixture.GetCategory();
      UpdateCategoryInput input = fixture.GetInput(category.Id);
      yield return new object[]
      {
        category,
        input
      };
    }
  }

  public static IEnumerable<object[]> GetInvalidInputs(int times)
  {
    UpdateCategoryTestFixture fixture = new();
    List<object[]> invalidInputs = [];
    int totalInvalidCases = 3;

    for (int index = 0; index < times; index++)
    {
      switch (index % totalInvalidCases)
      {
        case 0:
          invalidInputs.Add([
             fixture.GetInvalidInputShortName(),
              "Name should be at least 3 characters long"
           ]);
          break;
        case 1:
          invalidInputs.Add([
            fixture.GetInvalidInputLongName(),
            "Name should be less or equal 255 characters long"
          ]);
          break;
        case 2:
          invalidInputs.Add([
            fixture.GetInvalidInputLongDescription(),
            "Description should be less or equal 10000 characters long"
          ]);
          break;
        default:
          break;
      }
    }
    return invalidInputs;
  }
}

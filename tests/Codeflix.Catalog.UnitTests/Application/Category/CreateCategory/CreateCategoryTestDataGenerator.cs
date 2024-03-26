using System.Collections.Generic;

namespace Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;
public class CreateCategoryTestDataGenerator
{
  public static IEnumerable<object[]> GetInvalidInputs(int times)
  {
    CreateCategoryTestFixture fixture = new();
    List<object[]> invalidInputs = [];
    int totalInvalidCases = 4;

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
            fixture.GetInvalidInputNullDescription(),
            "Description should not be null"
          ]);
          break;
        case 3:
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

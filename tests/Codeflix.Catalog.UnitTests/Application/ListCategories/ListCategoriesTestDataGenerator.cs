using Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using System.Collections.Generic;

namespace Codeflix.Catalog.UnitTests.Application.ListCategories;
public class ListCategoriesTestDataGenerator
{
  public static IEnumerable<object[]> GetInputsWithoutParameters(int times = 10)
  {
    ListCategoriesTestFixture fixture = new();
    ListCategoriesInput input = fixture.GetListCategoriesInput();
    for (int i = 0; i < times; i++)
    {
      switch (i % 5)
      {
        case 0:
          yield return new object[]
          {
            new ListCategoriesInput()
          };
          break;
        case 1:
          yield return new object[]
          {
            new ListCategoriesInput(input.Page)
          };
          break;
        case 2:
          yield return new object[]
          {
            new ListCategoriesInput(input.Page, input.PerPage)
          };
          break;
        case 3:
          yield return new object[]
          {
            new ListCategoriesInput(input.Page, input.PerPage, input.Search)
          };
          break;
        case 4:
          yield return new object[]
          {
            new ListCategoriesInput(input.Page, input.PerPage, input.Search, input.Sort)
          };
          break;
        case 5:
          yield return new object[]
          {
            input
          };
          break;
        default:
          yield return new object[]
          {
            new ListCategoriesInput()
          };
          break;
      }
    }
  }
}

using Codeflix.Catalog.UnitTests.Application.Common;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.GetCategory;
public class GetCategoryTestFixture : CategoryUseCasesBaseFixture
{

}

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection : ICollectionFixture<GetCategoryTestFixture> { }

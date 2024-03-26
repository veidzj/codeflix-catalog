using Codeflix.Catalog.UnitTests.Application.Common;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.DeleteCategory;
public class DeleteCategoryTestFixture : CategoryUseCasesBaseFixture
{

}

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture> { }

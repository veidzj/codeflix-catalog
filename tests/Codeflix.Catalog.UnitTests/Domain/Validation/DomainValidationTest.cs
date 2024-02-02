using Bogus;
using Codeflix.Catalog.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Domain.Validation;
public class DomainValidationTest
{
  private Faker Faker { get; set; } = new Faker();

  [Fact(DisplayName = nameof(NotNullDoesNotThrowsOnSuccess))]
  [Trait("Domain", "DomainValidation - Validation")]
  public void NotNullDoesNotThrowsOnSuccess()
  {
    string value = this.Faker.Commerce.ProductName();
    Action action = () => DomainValidation.NotNull(value, "Value");
    action.Should().NotThrow();
  }
}

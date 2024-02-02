using Bogus;
using Codeflix.Catalog.Domain.Exceptions;
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
    Action action = () => DomainValidation.NotNull(value, "FieldName");
    action.Should().NotThrow();
  }

  [Fact(DisplayName = nameof(NotNullThrowsWhenNull))]
  [Trait("Domain", "DomainValidation - Validation")]
  public void NotNullThrowsWhenNull()
  {
    string? value = null;
    Action action = () => DomainValidation.NotNull(value, "FieldName");
    action.Should().Throw<EntityValidationException>().WithMessage("FieldName should not be null");
  }
}

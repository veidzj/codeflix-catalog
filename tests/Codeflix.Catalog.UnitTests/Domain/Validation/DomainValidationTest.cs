using Bogus;
using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.Validation;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Domain.Validation;
public class DomainValidationTest
{
  private Faker Faker { get; set; } = new Faker();

  public static IEnumerable<object[]> MakeValuesLessThanMin(int numberOfTests = 5)
  {
    Faker faker = new();
    for (int i = 0; i < numberOfTests; i++)
    {
      string value = faker.Commerce.ProductName();
      int minLength = value.Length + new Random().Next(1, 20);
      yield return new object[]
      {
        value,
        minLength
      };
    }
  }

  public static IEnumerable<object[]> MakeValuesGreaterThanMin(int numberOfTests = 5)
  {
    Faker faker = new();
    for (int i = 0; i < numberOfTests; i++)
    {
      string value = faker.Commerce.ProductName();
      int minLength = value.Length - new Random().Next(1, 5);
      yield return new object[]
      {
        value,
        minLength
      };
    }
  }

  public static IEnumerable<object[]> MakeValuesGreaterThanMax(int numberOfTests = 5)
  {
    Faker faker = new();
    for (int i = 0; i < numberOfTests; i++)
    {
      string value = faker.Commerce.ProductName();
      int maxLength = value.Length - new Random().Next(1, 5);
      yield return new object[]
      {
        value,
        maxLength
      };
    }
  }

  [Fact(DisplayName = nameof(NotNullThrowsWhenNull))]
  [Trait("Domain", "DomainValidation - Validation")]
  public void NotNullThrowsWhenNull()
  {
    string? value = null;
    Action action = () => DomainValidation.NotNull(value, "FieldName");
    action.Should().Throw<EntityValidationException>().WithMessage("FieldName should not be null");
  }

  [Fact(DisplayName = nameof(NotNullDoesNotThrowsOnSuccess))]
  [Trait("Domain", "DomainValidation - Validation")]
  public void NotNullDoesNotThrowsOnSuccess()
  {
    string value = this.Faker.Commerce.ProductName();
    Action action = () => DomainValidation.NotNull(value, "FieldName");
    action.Should().NotThrow();
  }

  [Theory(DisplayName = nameof(NotNullOrEmptyThrowsWhenNullOrEmpty))]
  [Trait("Domain", "DomainValidation - Validation")]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData(null)]
  public void NotNullOrEmptyThrowsWhenNullOrEmpty(string? target)
  {
    Action action = () => DomainValidation.NotNullOrEmpty(target, "FieldName");
    action.Should().Throw<EntityValidationException>().WithMessage("FieldName should not be null or empty");
  }

  [Fact(DisplayName = nameof(NotNullOrEmptyDoesNotThrowsOnSuccess))]
  [Trait("Domain", "DomainValidation - Validation")]
  public void NotNullOrEmptyDoesNotThrowsOnSuccess()
  {
    string target = this.Faker.Commerce.ProductName();
    Action action = () => DomainValidation.NotNullOrEmpty(target, "FieldName");
    action.Should().NotThrow();
  }

  [Theory(DisplayName = nameof(MinLengthThrowsWhenLess))]
  [Trait("Domain", "DomainValidation - Validation")]
  [MemberData(nameof(MakeValuesLessThanMin), parameters: 10)]
  public void MinLengthThrowsWhenLess(string target, int minLength)
  {
    Action action = () => DomainValidation.MinLength(target, minLength, "FieldName");
    action.Should().Throw<EntityValidationException>().WithMessage($"FieldName should not be less than {minLength} characters long");
  }

  [Theory(DisplayName = nameof(MinLengthDoesNotThrowsOnSuccess))]
  [Trait("Domain", "DomainValidation - Validation")]
  [MemberData(nameof(MakeValuesGreaterThanMin), parameters: 10)]
  public void MinLengthDoesNotThrowsOnSuccess(string target, int minLength)
  {
    Action action = () => DomainValidation.MinLength(target, minLength, "FieldName");
    action.Should().NotThrow();
  }

  [Theory(DisplayName = nameof(MaxLengthThrowsWhenGreater))]
  [Trait("Domain", "DomainValidation - Validation")]
  [MemberData(nameof(MakeValuesGreaterThanMax), parameters: 10)]
  public void MaxLengthThrowsWhenGreater(string target, int maxLength)
  {
    Action action = () => DomainValidation.MaxLength(target, maxLength, "FieldName");
    action.Should().Throw<EntityValidationException>().WithMessage($"FieldName should not be greater than {maxLength} characters long");
  }
}

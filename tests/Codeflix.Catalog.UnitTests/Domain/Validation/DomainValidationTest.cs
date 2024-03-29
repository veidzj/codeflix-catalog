﻿using Bogus;
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

  public static IEnumerable<object[]> GetValuesLessThanMin(int numberOfTests = 5)
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

  public static IEnumerable<object[]> GetValuesLessThanMax(int numberOfTests = 5)
  {
    Faker faker = new();
    for (int i = 0; i < numberOfTests; i++)
    {
      string value = faker.Commerce.ProductName();
      int maxLength = value.Length + new Random().Next(0, 5);
      yield return new object[]
      {
        value,
        maxLength
      };
    }
  }

  public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTests = 5)
  {
    Faker faker = new();
    for (int i = 0; i < numberOfTests; i++)
    {
      string value = faker.Commerce.ProductName();
      int minLength = value.Length - new Random().Next(0, 5);
      yield return new object[]
      {
        value,
        minLength
      };
    }
  }

  public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOfTests = 5)
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
    string fieldName = this.Faker.Commerce.ProductName().Replace(" ", "");

    Action action = () => DomainValidation.NotNull(value, fieldName);

    action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be null");
  }

  [Fact(DisplayName = nameof(NotNullDoesNotThrowsOnSuccess))]
  [Trait("Domain", "DomainValidation - Validation")]
  public void NotNullDoesNotThrowsOnSuccess()
  {
    string value = this.Faker.Commerce.ProductName();
    string fieldName = this.Faker.Commerce.ProductName().Replace(" ", "");

    Action action = () => DomainValidation.NotNull(value, fieldName);

    action.Should().NotThrow();
  }

  [Theory(DisplayName = nameof(NotNullOrEmptyThrowsWhenNullOrEmpty))]
  [Trait("Domain", "DomainValidation - Validation")]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData(null)]
  public void NotNullOrEmptyThrowsWhenNullOrEmpty(string? target)
  {
    string fieldName = this.Faker.Commerce.ProductName().Replace(" ", "");

    Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

    action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be empty or null");
  }

  [Fact(DisplayName = nameof(NotNullOrEmptyDoesNotThrowsOnSuccess))]
  [Trait("Domain", "DomainValidation - Validation")]
  public void NotNullOrEmptyDoesNotThrowsOnSuccess()
  {
    string target = this.Faker.Commerce.ProductName();
    string fieldName = this.Faker.Commerce.ProductName().Replace(" ", "");

    Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

    action.Should().NotThrow();
  }

  [Theory(DisplayName = nameof(MinLengthThrowsWhenLess))]
  [Trait("Domain", "DomainValidation - Validation")]
  [MemberData(nameof(GetValuesLessThanMin), parameters: 10)]
  public void MinLengthThrowsWhenLess(string target, int minLength)
  {
    string fieldName = this.Faker.Commerce.ProductName().Replace(" ", "");

    Action action = () => DomainValidation.MinLength(target, minLength, fieldName);

    action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should be at least {minLength} characters long");
  }

  [Theory(DisplayName = nameof(MinLengthDoesNotThrowsOnSuccess))]
  [Trait("Domain", "DomainValidation - Validation")]
  [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
  public void MinLengthDoesNotThrowsOnSuccess(string target, int minLength)
  {
    string fieldName = this.Faker.Commerce.ProductName().Replace(" ", "");

    Action action = () => DomainValidation.MinLength(target, minLength, fieldName);

    action.Should().NotThrow();
  }

  [Theory(DisplayName = nameof(MaxLengthThrowsWhenGreater))]
  [Trait("Domain", "DomainValidation - Validation")]
  [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
  public void MaxLengthThrowsWhenGreater(string target, int maxLength)
  {
    string fieldName = this.Faker.Commerce.ProductName().Replace(" ", "");

    Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

    action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should be less or equal {maxLength} characters long");
  }

  [Theory(DisplayName = nameof(MaxLengthDoesNotThrowsOnSuccess))]
  [Trait("Domain", "DomainValidation - Validation")]
  [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
  public void MaxLengthDoesNotThrowsOnSuccess(string target, int maxLength)
  {
    string fieldName = this.Faker.Commerce.ProductName().Replace(" ", "");

    Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

    action.Should().NotThrow();
  }
}

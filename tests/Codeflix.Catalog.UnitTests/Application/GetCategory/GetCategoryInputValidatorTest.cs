using Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FluentAssertions;
using FluentValidation.Results;
using System;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryInputValidatorTest
{
  private readonly GetCategoryTestFixture fixture;

  public GetCategoryInputValidatorTest(GetCategoryTestFixture fixture)
  {
    this.fixture = fixture;
  }

  [Fact(DisplayName = nameof(ValidationOk))]
  [Trait("Application", "GetCategoryInputValidation - UseCases")]
  public void ValidationOk()
  {
    GetCategoryInput validInput = new(Guid.NewGuid());
    GetCategoryInputValidator validator = new();

    ValidationResult validationResult = validator.Validate(validInput);

    validationResult.Should().NotBeNull();
    validationResult.IsValid.Should().BeTrue();
    validationResult.Errors.Should().HaveCount(0);
  }

  [Fact(DisplayName = nameof(ValidationOk))]
  [Trait("Application", "GetCategoryInputValidation - UseCases")]
  public void ValidationErrorWhenEmptyId()
  {
    GetCategoryInput invalidInput = new(Guid.Empty);
    GetCategoryInputValidator validator = new();

    ValidationResult validationResult = validator.Validate(invalidInput);

    validationResult.Should().NotBeNull();
    validationResult.IsValid.Should().BeFalse();
    validationResult.Errors.Should().HaveCount(1);
    validationResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
  }
}

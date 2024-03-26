using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FluentAssertions;
using FluentValidation.Results;
using System;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryInputValidatorTest
{
  private readonly UpdateCategoryTestFixture fixture;

  public UpdateCategoryInputValidatorTest(UpdateCategoryTestFixture fixture)
  {
    this.fixture = fixture;
  }

  [Fact(DisplayName = nameof(DontValidateWhenEmptyGuid))]
  [Trait("Application", "UpdateCategoryInputValidator - Use Cases")]
  public void DontValidateWhenEmptyGuid()
  {
    UpdateCategoryInput input = this.fixture.GetInput(Guid.Empty);
    UpdateCategoryInputValidator validator = new();

    ValidationResult validationResult = validator.Validate(input);

    validationResult.Should().NotBeNull();
    validationResult.IsValid.Should().BeFalse();
    validationResult.Errors.Should().HaveCount(1);
    validationResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
  }

  [Fact(DisplayName = nameof(ValidateWhenValid))]
  [Trait("Application", "UpdateCategoryInputValidator - Use Cases")]
  public void ValidateWhenValid()
  {
    UpdateCategoryInput input = this.fixture.GetInput();
    UpdateCategoryInputValidator validator = new();

    ValidationResult validationResult = validator.Validate(input);

    validationResult.Should().NotBeNull();
    validationResult.IsValid.Should().BeTrue();
    validationResult.Errors.Should().HaveCount(0);
  }
}

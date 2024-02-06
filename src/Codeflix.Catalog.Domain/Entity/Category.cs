using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.SeedWork;
using Codeflix.Catalog.Domain.Validation;
using System;

namespace Codeflix.Catalog.Domain.Entity;
public class Category : AggregateRoot
{
  public string Name { get; private set; }
  public string Description { get; private set; }
  public bool IsActive { get; private set; }
  public DateTime CreatedAt { get; private set; }

  public Category(string name, string description, bool isActive = true) : base()
  {
    this.Name = name;
    this.Description = description;
    this.IsActive = isActive;
    this.CreatedAt = DateTime.Now;
    this.Validate();
  }

  private void Validate()
  {
    DomainValidation.NotNullOrEmpty(this.Name, nameof(this.Name));
    DomainValidation.MinLength(this.Name, 3, nameof(this.Name));
    DomainValidation.MaxLength(this.Name, 255, nameof(this.Name));

    DomainValidation.NotNull(this.Description, nameof(this.Description));
    DomainValidation.MaxLength(this.Description, 10_000, nameof(this.Description));
  }

  public void Activate()
  {
    this.IsActive = true;
    this.Validate();
  }

  public void Deactivate()
  {
    this.IsActive = false;
    this.Validate();
  }

  public void Update(string name, string? description = null)
  {
    this.Name = name;
    this.Description = description ?? this.Description;
    this.Validate();
  }
}

using System;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Application.UseCases.Category.Common;
public class CategoryModelOutput
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }

  public CategoryModelOutput(Guid id, string name, string description, bool isActive, DateTime createdAt)
  {
    this.Id = id;
    this.Name = name;
    this.Description = description ?? string.Empty;
    this.IsActive = isActive;
    this.CreatedAt = createdAt;
  }

  public static CategoryModelOutput FromCategory(DomainEntity.Category category)
  {
    return new(category.Id, category.Name, category.Description, category.IsActive, category.CreatedAt);
  }
}

using System;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory;
public class GetCategoryOutput
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }

  public GetCategoryOutput(Guid id, string name, string description, bool isActive, DateTime createdAt)
  {
    this.Id = id;
    this.Name = name;
    this.Description = description ?? "";
    this.IsActive = isActive;
    this.CreatedAt = createdAt;
  }

  public static GetCategoryOutput FromCategory(DomainEntity.Category category)
  {
    return new(category.Id, category.Name, category.Description, category.IsActive, category.CreatedAt);
  }
}

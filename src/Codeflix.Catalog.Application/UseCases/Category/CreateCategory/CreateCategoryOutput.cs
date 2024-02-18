﻿using DomainEntity = Codeflix.Catalog.Domain.Entity;
using System;

namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
public class CreateCategoryOutput
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }

  public CreateCategoryOutput(Guid id, string name, string description, bool isActive, DateTime createdAt)
  {
    this.Id = id;
    this.Name = name;
    this.Description = description ?? "";
    this.IsActive = isActive;
    this.CreatedAt = createdAt;
  }

  public static CreateCategoryOutput FromCategory(DomainEntity.Category category)
  {
    return new(category.Id, category.Name, category.Description, category.IsActive, category.CreatedAt);
  }
}

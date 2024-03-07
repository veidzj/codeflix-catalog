﻿using Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;
using System;

namespace Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
public class UpdateCategoryInput : IRequest<CategoryModelOutput>
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public bool IsActive { get; set; }

  public UpdateCategoryInput(Guid id, string name, string description, bool isActive)
  {
    this.Id = id;
    this.Name = name;
    this.Description = description;
    this.IsActive = isActive;
  }
}

﻿using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.SeedWork;
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
    if (string.IsNullOrWhiteSpace(this.Name))
    {
      throw new EntityValidationException($"{nameof(this.Name)} should not be empty or null");
    }

    if (this.Name.Length < 3)
    {
      throw new EntityValidationException($"{nameof(this.Name)} should be at least 3 characters long");
    }

    if (this.Name.Length > 255)
    {
      throw new EntityValidationException($"{nameof(this.Name)} should be less or equal 255 characters long");
    }

    if (this.Description == null)
    {
      throw new EntityValidationException($"{nameof(this.Description)} should not be null");
    }

    if (this.Description.Length > 10_000)
    {
      throw new EntityValidationException($"{nameof(this.Description)} should be less or equal 10.000 characters long");
    }
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

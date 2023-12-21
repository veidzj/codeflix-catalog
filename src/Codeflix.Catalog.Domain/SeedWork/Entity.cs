﻿using System;

namespace Codeflix.Catalog.Domain.SeedWork;
public abstract class Entity
{
  public Guid Id { get; protected set; }

  protected Entity()
  {
    this.Id = Guid.NewGuid();
  }
}

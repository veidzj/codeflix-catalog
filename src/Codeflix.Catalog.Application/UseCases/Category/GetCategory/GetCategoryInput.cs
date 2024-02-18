using Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;
using System;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory;
public class GetCategoryInput : IRequest<CategoryModelOutput>
{
  public Guid Id { get; set; }

  public GetCategoryInput(Guid id)
  {
    this.Id = id;
  }
}

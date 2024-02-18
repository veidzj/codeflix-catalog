using MediatR;
using System;

namespace Codeflix.Catalog.Application.UseCases.Category.GetCategory;
public class GetCategoryInput : IRequest<GetCategoryOutput>
{
  public Guid Id { get; set; }

  public GetCategoryInput(Guid id)
  {
    this.Id = id;
  }
}

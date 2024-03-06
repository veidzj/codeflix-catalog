using MediatR;
using System;

namespace Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;
public class DeleteCategoryInput : IRequest
{
  public Guid Id { get; set; }

  public DeleteCategoryInput(Guid id)
  {
    this.Id = id;
  }
}

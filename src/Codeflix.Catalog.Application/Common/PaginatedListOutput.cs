using System.Collections.Generic;

namespace Codeflix.Catalog.Application.Common;
public abstract class PaginatedListOutput<TOutputItem>
{
  public int Page { get; set; }
  public int PerPage { get; set; }
  public int Total { get; set; }
  public IReadOnlyList<TOutputItem> Items { get; set; }

  public PaginatedListOutput(int page, int perPage, int total, IReadOnlyList<TOutputItem> items)
  {
    this.Page = page;
    this.PerPage = perPage;
    this.Total = total;
    this.Items = items;
  }
}

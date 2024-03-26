using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

namespace Codeflix.Catalog.Application.Common;
public abstract class PaginatedListInput
{
  public int Page { get; set; }
  public int PerPage { get; set; }
  public string Search { get; set; }
  public string Sort { get; set; }
  public SearchOrder Dir { get; set; }

  public PaginatedListInput(int page, int perPage, string search, string sort, SearchOrder dir)
  {
    this.Page = page;
    this.PerPage = perPage;
    this.Search = search;
    this.Sort = sort;
    this.Dir = dir;
  }
}

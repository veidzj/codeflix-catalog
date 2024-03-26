using System.Threading;
using System.Threading.Tasks;

namespace Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
public interface ISearchableRepository<TAggregate> where TAggregate : AggregateRoot
{
  Task<SearchOutput<TAggregate>> Search(SearchInput input, CancellationToken cancellationToken);
}

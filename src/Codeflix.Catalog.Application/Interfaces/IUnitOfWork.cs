using System.Threading;
using System.Threading.Tasks;

namespace Codeflix.Catalog.Application.Interfaces;
public interface IUnitOfWork
{
  public Task Commit(CancellationToken cancellationToken);
}

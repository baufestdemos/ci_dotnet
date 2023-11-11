using Microsoft.EntityFrameworkCore;

namespace Core.Cross.Transactions;

public class EFImplicitTransactor<DC> : ITransactor
    where DC : DbContext
{
    protected readonly DC _context;

    public EFImplicitTransactor(DC context) => _context = context;

    public TR Begin<TR>(Func<TR> action)
    {
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }
        using (_context)
        {
            var result = action();
            _context.SaveChanges();
            return result;
        }
    }

    public async Task<TR> BeginAsync<TR>(Func<Task<TR>> action)
    {
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }
        using (_context)
        {
            var result = await action().ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }
    }
}
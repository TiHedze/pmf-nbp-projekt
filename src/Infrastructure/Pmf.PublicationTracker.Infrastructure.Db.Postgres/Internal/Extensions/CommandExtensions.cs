namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using System.Data.Common;

    internal static class CommandExtensions
    {
        internal static async Task<List<T>> GetAsync<T>(
            this DbCommand command,
            DbContext dbContext,
            CancellationToken cancellationToken)
        {
            try
            {
                command.Connection?.Open();

                await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                if (!reader.HasRows)
                {
                    return new();
                }

                return await reader.ReadListAsync<T>(dbContext, cancellationToken);
            }
            finally
            {
                command.Connection?.Close();
            }
        }

        internal static async Task<T?> GetSingleAsync<T>(
            this DbCommand command,
            DbContext dbContext,
            CancellationToken cancellationToken)
        {
            try
            {
                command.Connection?.Open();

                await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                if (!reader.HasRows)
                {
                    return default;
                }

                return await reader.ReadSingleAsync<T>(dbContext, cancellationToken);

            }
            finally
            {
                command?.Connection?.Close();
            }
        }
    }
}

namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal.Extensions
{
    using System.Data.Common;
    using System.Net.Http.Headers;

    internal static class CommandExtensions
    {
        internal static async Task<List<T>> GetAsync<T>(
            this DbCommand command,
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

                return await reader.ReadListAsync<T>(cancellationToken);
            }
            finally
            {
                command.Connection?.Close();
            }
        }

        internal static async Task<T?> GetSingleAsync<T>(
            this DbCommand command,
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

                return await reader.ReadSingleAsync<T>(cancellationToken);

            }
            finally
            {
                command?.Connection?.Close();
            }
        }
    }
}

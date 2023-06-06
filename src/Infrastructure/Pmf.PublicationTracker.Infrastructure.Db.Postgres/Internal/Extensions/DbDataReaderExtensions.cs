namespace Pmf.PublicationTracker.Infrastructure.Db.Postgres.Internal.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    internal static class DbDataReaderExtensions
    {
        private const string ColumnName = nameof(ColumnName);

        internal static async Task<List<T>> ReadListAsync<T>(this DbDataReader reader, DbContext dbContext, CancellationToken cancellationToken)
        {
            var result = new List<T>();

            while (await reader.ReadAsync(cancellationToken))
            {
                T? instance = reader.ReadSingle<T>(dbContext);

                if (instance is not null)
                {
                    result.Add(instance);
                }
            }

            return result;
        }

        internal static async Task<T?> ReadSingleAsync<T>(this DbDataReader reader, DbContext dbContext, CancellationToken cancellationToken)
        {
            T? instance = default;

            while (await reader.ReadAsync(cancellationToken))
            {
                instance = reader.ReadSingle<T>(dbContext);
            }

            return instance;
        }

        internal static T? ReadSingle<T>(this DbDataReader reader, DbContext dbContext)
        {
            T? instance = Activator.CreateInstance<T>();

            if (instance is null)
            {
                return instance;
            }

            foreach (PropertyInfo propertyInfo in instance.GetType().GetProperties())
            {
                var entityType = dbContext.Model.FindEntityType(typeof(T));
                ArgumentNullException.ThrowIfNull(entityType, nameof(entityType));

                var property = entityType.FindProperty(propertyInfo.Name);

                if (property is null)
                {
                    continue;
                }

                if (reader.TryGetValue(property.GetColumnName(), out object? value))
                {
                    propertyInfo.SetValue(instance, value);
                }
            }

            return instance;
        }

        internal static bool TryGetValue(this DbDataReader reader, string columnName, out object? value)
        {
            try
            {
                if (!reader.HasColumn(columnName))
                {
                    value = null;
                    return false;
                }

                value = reader[columnName];
                value = DBNull.Value.Equals(value) ? null : value;
                return true;
            }
            catch (Exception)
            {
                value = null;
                return false;
            }
        }

        private static bool HasColumn(this DbDataReader reader, string columnName)
        {
            DataTable? schema = reader.GetSchemaTable();

            return schema is not null &&
                schema.Rows
                .Cast<DataRow>()
                .Any(row => row[ColumnName].ToString() == columnName);
        }
    }
}

using System.Data.Common;
using System.Reflection;

namespace Shared.Extensions
{
    public static class DbDataReaderExtensions
    {
        public static async Task<List<T>> MapToListAsync<T>(this DbDataReader reader) where T : new()
        {
            var result = new List<T>();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (await reader.ReadAsync())
            {
                var obj = new T();
                foreach (var prop in props)
                {
                    if (!reader.HasColumn(prop.Name) || reader[prop.Name] is DBNull)
                        continue;

                    prop.SetValue(obj, reader[prop.Name]);
                }
                result.Add(obj);
            }
            return result;
        }

        private static bool HasColumn(this DbDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}

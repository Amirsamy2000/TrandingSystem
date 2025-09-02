using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Domain.Abstractions
{
    public interface IDomainInterface<Domain>
    {
        Domain Create(Domain Object);
        List<Domain> Read();
        Domain ReadById(int Id);
        Domain Update(Domain Element);
        Domain Delete(int Id);
        Domain SwitchColumn(int Id, string ColumnName)
        {
            var entity = ReadById(Id);
            if (entity == null)
                throw new ArgumentException($"Entity with Id={Id} not found.");

            var prop = typeof(Domain).GetProperty(ColumnName, BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
                throw new ArgumentException($"Property '{ColumnName}' not found on {typeof(Domain).Name}.");

            if (!prop.CanRead || !prop.CanWrite)
                throw new InvalidOperationException($"Property '{ColumnName}' must be readable and writable.");

            var currentValue = prop.GetValue(entity);

            if (prop.PropertyType == typeof(bool))
            {
                bool newValue = !(bool)currentValue;
                prop.SetValue(entity, newValue);
            }
            else
            {
                throw new NotSupportedException($"SwitchColumn only supports boolean properties. '{ColumnName}' is {prop.PropertyType.Name}.");
            }

            return Update(entity);
        }
    }
}

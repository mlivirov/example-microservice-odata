using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace ProjectName.Dal.Core
{
    public static class DatabaseHelper
    {
        public static IReadOnlyList<Type> GetEntityTypes(Assembly assembly)
        {
            return assembly.ExportedTypes.Where(p =>
                p.GetInterfaces().Contains(typeof(IEntity))).ToList();
        }

        public static IReadOnlyList<Type> GetViewTypes(Assembly assembly)
        {
            return assembly.ExportedTypes.Where(p =>
                p.GetInterfaces().Contains(typeof(IView))).ToList();
        }

        public static PropertyInfo GetKeyProperty<T>()
        {
            return typeof(T).GetProperties().Single(p => p.GetCustomAttribute<KeyAttribute>() != null);
        }
    }
}
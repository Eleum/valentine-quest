using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;

namespace Valentine.Application.Extensions
{
    public static class DbContextExtensions
    {
        public static IQueryable<T> Set<T>(this IApplicationDbContext context)
        {
            // Get the generic type definition 
            var method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // Build a method with the specific type argument you're interested in 
            method = method.MakeGenericMethod(typeof(T));

            return method.Invoke(context, null) as IQueryable<T>;
        }
    }
}

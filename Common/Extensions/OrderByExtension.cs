using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class OrderByExtension
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty, bool isDescending = true)
        {
            if (source == null)
                throw new ArgumentNullException("source", "source is null.");

            if (string.IsNullOrEmpty(sortProperty))
                throw new ArgumentException("sortExpression is null or empty.", "sortExpression");

            var tType = typeof(T);


            PropertyInfo prop = tType.GetProperty(sortProperty);

            if (prop == null)
            {
                throw new ArgumentException(string.Format("No property '{0}' on type '{1}'", sortProperty, tType.Name));
            }

            var funcType = typeof(Func<,>)
                .MakeGenericType(tType, prop.PropertyType);

            var lambdaBuilder = typeof(Expression)
                .GetMethods()
                .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
                .MakeGenericMethod(funcType);

            var parameter = Expression.Parameter(tType);
            var propExpress = Expression.Property(parameter, prop);

            var sortLambda = lambdaBuilder
                .Invoke(null, new object[] { propExpress, new ParameterExpression[] { parameter } });

            var sorter = typeof(Queryable)
                .GetMethods()
                .FirstOrDefault(x => x.Name == (isDescending ? "OrderByDescending" : "OrderBy") && x.GetParameters().Length == 2)
                .MakeGenericMethod(new[] { tType, prop.PropertyType });

            return (IQueryable<T>)sorter
                .Invoke(null, new object[] { source, sortLambda });
        }
    }
}

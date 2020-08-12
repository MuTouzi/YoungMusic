using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WebAPI.Utility
{
    public static class LinqExtend
    {

        public static IQueryable<TSource> WhereIn<TSource, TKey>(this IQueryable<TSource> source, IEnumerable<TKey> source2,
            Expression<Func<TSource, TKey>> keySelector)
        {
            Expression inExpression = GetInExpression(source2, keySelector);
            return source.Where(Expression.Lambda<Func<TSource, bool>>(inExpression, keySelector.Parameters));
        }
        public static IQueryable<TSource> WhereNotIn<TSource, TKey>(this IQueryable<TSource> source, IEnumerable<TKey> source2,
            Expression<Func<TSource, TKey>> keySelector)
        {
            Expression inExpression = GetInExpression(source2, keySelector);
            Expression notInExpression = Expression.Not(inExpression);
            return source.Where(Expression.Lambda<Func<TSource, bool>>(notInExpression, keySelector.Parameters));
        }
        private static Expression GetInExpression<TSource, TKey>(IEnumerable<TKey> source2,
            Expression<Func<TSource, TKey>> keySelector)
        {
            if (null == keySelector)
                throw new ArgumentNullException("keySelector");
            if (null == source2)
                throw new ArgumentNullException("source2");
            if (source2.Count() == 0)
                return Expression.Constant(true);

            Type genericType = typeof(List<>).MakeGenericType(typeof(TKey));
            var conExp = Expression.Constant(source2.ToList(), genericType);
            MethodInfo methodInfo = genericType.GetMethods().FirstOrDefault(m => m.Name.Equals("Contains")
                && m.GetParameters().Count() == 1 && m.GetParameters().FirstOrDefault().ParameterType == typeof(TKey));
            var bodyExp = Expression.Call(conExp, methodInfo, keySelector.Body);
            return bodyExp;
        }
    }
}
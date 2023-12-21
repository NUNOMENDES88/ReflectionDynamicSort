namespace ReflectionDynamicSort.Helpers
{
    using Enumerations;
    using Models;
    using ReflectionDynamicSort.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class OrderHelper
    {
        private static readonly string CommandOrderAsc = "OrderBy";
        private static readonly string CommandOrderDsc = "OrderByDescending";
        private static readonly string CommandOrderThenByAsc = "ThenBy";
        private static readonly string CommandOrderThenByDsc = "ThenByDescending";

        public static IQueryable<T> Order<T>(
            this IQueryable<T> source,
            List<OrderModel> sorterList)
        {
            if (!sorterList.CheckNotNullAndAny())
                return source;

            try
            {
                ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
                MethodCallExpression resultExpression = GetExpression<T>(parameter, sorterList[0], source.Expression, true);
                if (sorterList.Count > 1)
                {
                    for (int i = 1; i < sorterList.Count; i++)
                    {
                        resultExpression = GetExpression<T>(parameter, sorterList[i], resultExpression, false);
                    }
                }
                return source.Provider.CreateQuery<T>(resultExpression);
            }
            catch (Exception)
            {
                throw new Exception("Error Invalid Sort Field");
            }
        }


        public static MethodCallExpression GetExpression<T>(
            ParameterExpression parameterExpression,
            OrderModel orderModel,
            Expression expressionSource,
            bool first)
        {
            string command = "";
            if (first)
            {
                command = orderModel.OrderType == OrderTypeEnum.Ascending ? CommandOrderAsc : CommandOrderDsc;
            }
            else
            {
                command = orderModel.OrderType == OrderTypeEnum.Ascending ? CommandOrderThenByAsc : CommandOrderThenByDsc;
            }

            MemberExpression property = Expression.PropertyOrField(parameterExpression, orderModel.PropertyName);
            Expression orderByExpression = Expression.Lambda(property, parameterExpression);
            return Expression.Call(
                typeof(Queryable), command,
                new Type[] { typeof(T), property.Type },
                expressionSource,
                Expression.Quote(orderByExpression));


        }
    }
}
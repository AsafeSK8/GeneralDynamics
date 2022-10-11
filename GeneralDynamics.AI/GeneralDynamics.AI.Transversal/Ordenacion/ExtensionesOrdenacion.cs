using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace GeneralDynamics.AI.Transversal.Ordenacion
{

    public static class ExtensionesOrdenacion
    {

        /// <summary>
        ///  Obtener la expresion de ordenación 
        /// </summary>
        /// <typeparam name="TEntity">Entidad</typeparam>
        /// <param name="listaOrdenacion">Lista de objetos Orden</param>
        /// <param name="mapeoOrdenacion">Lista de campos a mapear</param>
        /// <returns></returns>
        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy<TEntity>(this List<Orden> listaOrdenacion, params KeyValuePair<string, string>[] mapeoOrdenacion)
        {
            listaOrdenacion.MapearCampos(mapeoOrdenacion);

            return OrderBy<TEntity>(listaOrdenacion);
        }
       

        /// <summary>
        ///  Obtener la expresion de ordenación 
        /// </summary>
        /// <typeparam name="TEntity">Entidad</typeparam>
        /// <param name="listaOrdenacion">Lista de objetos Orden</param>
        /// <returns></returns>
        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy<TEntity>(this List<Orden> listaOrdenacion)
        {
            Type typeQueryable = typeof(IQueryable<TEntity>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
            Type entityType = typeof(TEntity);
          //  Type entityTypeAux = typeof(TEntity);

            ParameterExpression parameterExpression = Expression.Parameter(entityType, "x");

            Expression resultExpression = argQueryable;
           // Expression expr = parameterExpression;
            if (listaOrdenacion.Count>0)
            {
                //Hay campos para ordenar
                for (int i = 0; i < listaOrdenacion.Count; i++)
                {
                    Type entityTypeAux = typeof(TEntity);
                    Expression expr = parameterExpression;
                    string[] propiedades = listaOrdenacion[i].Campo.Split(".");
                                     
                    PropertyInfo pi = null;
                    foreach (var propiedad in propiedades)
                    {
                        pi = entityTypeAux.GetProperty(propiedad, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        expr = Expression.Property(expr, pi);
                        entityTypeAux = pi.PropertyType;
                    }
                    LambdaExpression lambdaExp = Expression.Lambda(expr, parameterExpression);
                    Type propertyType = pi.PropertyType;



                    //PropertyInfo propertyInfo = entityType.GetProperty(listaOrdenacion[i].Campo, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    //Expression propertyExpr = Expression.Property(parameterExpression, propertyInfo);
                    //Type propertyType = propertyInfo.PropertyType;
                    //LambdaExpression lambdaExp = Expression.Lambda(propertyExpr, parameterExpression);


                    string methodName;

                    if (i==0)
                    {
                        methodName = listaOrdenacion[i].Modo == ModoOrdenacion.Ascendente ? "OrderBy" : "OrderByDescending";
                    }
                    else
                    {
                        methodName = listaOrdenacion[i].Modo == ModoOrdenacion.Ascendente ? "ThenBy" : "ThenByDescending";
                    }              

                    resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { entityType, propertyType }, resultExpression, Expression.Quote(lambdaExp));
                }
            }
            else
            {
                //No hay campos para ordenar.
                LambdaExpression lambdaExp = Expression.Lambda(Expression.Constant(true), parameterExpression);
                resultExpression = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { entityType, typeof(bool) }, resultExpression, Expression.Quote(lambdaExp));
            }


            LambdaExpression orderedLambda = Expression.Lambda(resultExpression, argQueryable);
            return (Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>)orderedLambda.Compile();
        }

        /// <summary>
        /// Mapea los campos de ordenación
        /// </summary>
        /// <typeparam name="TEntity">Entidad</typeparam>
        /// <param name="listaOrdenacion">Lista de objetos Orden</param>
        /// <param name="mapeoOrdenacion">Lista de campos a mapear</param>
        /// <returns></returns>
        public  static List<Orden> MapearCampos(this List<Orden> listaOrdenacion, KeyValuePair<string, string>[] mapeoOrdenacion)
        {
            if (mapeoOrdenacion != null && listaOrdenacion!=null)
            {
                foreach (KeyValuePair<string, string> camposMapeo in mapeoOrdenacion)
                {
                    if (listaOrdenacion.Where(x => x.Campo == camposMapeo.Key)?.Any() ?? false)
                    {
                        var campos = listaOrdenacion.Where(x => x.Campo == camposMapeo.Key);
                        foreach (Orden orden in campos)
                        {
                            orden.Campo = camposMapeo.Value;
                        }
                    }
                }
            }
            return listaOrdenacion;
        }

        public static List<T> OrdenarLista<T>(List<T> lista, ModoOrdenacion sorting, string field, string primario = null) where T : new()
        {

            if (lista != null)
            {
                if (primario != null && primario != field)
                {
                    if (sorting == ModoOrdenacion.Ascendente)
                        lista = lista.OrderBy(s => s.GetType().GetProperty(primario).GetValue(s)).ThenBy(s => s.GetType().GetProperty(field).GetValue(s)).ToList();
                    else
                        lista = lista.OrderBy(s => s.GetType().GetProperty(primario).GetValue(s)).ThenByDescending(s => s.GetType().GetProperty(field).GetValue(s)).ToList();
                }
                else
                {
                    if (sorting == ModoOrdenacion.Ascendente)
                        lista = lista.OrderBy(s => s.GetType().GetProperty(field).GetValue(s)).ToList();
                    else
                        lista = lista.OrderByDescending(s => s.GetType().GetProperty(field).GetValue(s)).ToList();
                }
                return lista;
            }
            else
            {
                return new List<T>();
            }
        }

        public static List<T> OrderBy<T>(List<T> list, string campo, ModoOrdenacion ordenacion)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].
                                         GetType().GetProperty(campo);

            if (ordenacion ==  ModoOrdenacion.Ascendente)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList();
            }
            if (ordenacion ==  ModoOrdenacion.Descendente )
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

    }
}

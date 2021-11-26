using AutoMapper;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// Utility mapper extension
    /// </summary>
    public static class MapperExtension
    {
        /// <summary>
        /// Maps any list to TDestination
        /// </summary>
        /// <typeparam name="TSource">Source parameter</typeparam>
        /// <typeparam name="TDestination">Destination parameter</typeparam>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="source">Source collection</param>
        /// <returns>Destination collection</returns>
        public static List<TDestination> MapList<TSource, TDestination>(this IMapper mapper, IEnumerable<TSource> source)
        {
            List<TDestination> result = new List<TDestination>();

            if (source == null)
                return null;

            foreach (var item in source)
            {
                result.Add(mapper.Map<TSource, TDestination>(item));
            }

            return result;
        }
    }
}

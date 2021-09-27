using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace WebForum
{
    /// <summary>
    /// Utility mapper extension
    /// </summary>
    public static class MapperExtension
    {
        /// <summary>
        /// Maps any list to TDestination
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="mapper"></param>
        /// <param name="source"></param>
        /// <returns></returns>
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

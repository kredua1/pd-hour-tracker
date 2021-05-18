using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Extensions
{
    /// <summary>
    /// Extensions for AutoMapper so lists can be mapped to save some loops.
    /// </summary>
    public static class AutoMapperListExtensions
    {
        /// <summary>
        /// Maps one list to another
        /// </summary>
        /// <typeparam name="TSource">The source type to map from</typeparam>
        /// <typeparam name="TDestination">The destination type to map to</typeparam>
        /// <param name="mapper"></param>
        /// <param name="sourceList">The source list to copy from</param>
        /// <returns></returns>
        public static List<TDestination> MapList<TSource, TDestination>(this IMapper mapper, List<TSource> sourceList)
        {
            var destinationList = new List<TDestination>();

            foreach(var source in sourceList)
            {
                destinationList.Add(mapper.Map<TDestination>(source));
            }

            return destinationList;
        }
    }
}

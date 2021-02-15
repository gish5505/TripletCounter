using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripletCounter
{
    public class TripletCounter
    {
        public List<TripletSet> CountTriplets(List<string> inputTriplets)
        {
            var countResult = inputTriplets
                .GroupBy(x => x)
                .Select(x => new TripletSet
                {
                    Name = x.Key,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.Name)
                .ToList();

             return countResult.GetRange(0,10);
        }        
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TripletCounter
{
    public class TripletSlicer
    {
        public Task<List<string>> SliceWordsIntoTriplets(List<string> inputWords, CancellationToken cancellationToken)
        {
            ConcurrentBag<string> listOfTriplets = new ConcurrentBag<string>();

            ParallelOptions parallelOptions = new ParallelOptions()
            { 
                CancellationToken = cancellationToken
            };            

            try
            {
                Parallel.ForEach(inputWords, parallelOptions, word =>
                {
                    if (word.Length >= 3)
                    {
                        for (int startIndex = 0; (startIndex + 2) < word.Length; startIndex++)
                        {
                            listOfTriplets.Add(word.Substring(startIndex, 3));
                        }
                    }
                });

            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"Отмена операции: {e.Message}");
            }                        

            return Task.FromResult(listOfTriplets.ToList());
        }

    }
}

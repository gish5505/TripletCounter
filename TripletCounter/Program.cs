using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TripletCounter
{
    class Program
    {
        private static readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Введите полный путь до считываемого файла");
            string path = Console.ReadLine();
            string text = File.ReadAllText(path);

            Console.WriteLine("Работа идет");
            Console.WriteLine("Нажмите кнопку для отмены \n");

            var task =  Task.Factory.StartNew(() => GetTripletSetsAsync(text));
                        
            Console.ReadKey();

            if (!task.IsCompleted)
            {
                _cancellationTokenSource.Cancel();
                Console.ReadKey();
            }
        }


        
        private static async Task<List<TripletSet>> GetTripletSetsAsync(string text)
        {
            var timer = new Stopwatch();
            
            timer.Start();

            var resultList = await SortTriplets(text);

            timer.Stop();

            Console.WriteLine($"Времени прошло - {timer.ElapsedMilliseconds} мс");

            resultList.ForEach(x => 
            { 
                Console.WriteLine($"{x.Name} - {x.Count}");                
            });            

            return resultList;
        }

        private static async Task<List<TripletSet>> SortTriplets(string text)
        {
            StringSlicer stringSlicer = new StringSlicer();
            TripletSlicer tripletSlicer = new TripletSlicer();
            TripletCounter tripletCounter = new TripletCounter();

            List<TripletSet> sortedTriplets = new List<TripletSet>();

            var words = stringSlicer.SliceString(text);
            
            var triplets = await tripletSlicer.SliceWordsIntoTriplets(words, _cancellationTokenSource.Token);
            sortedTriplets = tripletCounter.CountTriplets(triplets);
            
            return sortedTriplets;
        }      
               
    }
}

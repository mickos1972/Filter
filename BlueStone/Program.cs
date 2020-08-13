using System;
using System.Collections.Generic;
using System.IO;
using BlueStone.Sorters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Take a string of numbers and apply filters and sorting.
/// A list of numbers and filter and sorter names can be found in app config.
/// </summary>

namespace BlueStone
{
    public class Program
    {
        private static IConfiguration _configuration;

        //FilterServiceResolver and SortServiceResolver are so we can use DI with different types of filters and sorts, as we can
        //have many filter of type IFilter and sorts of ISort. This allows us to declare in config (or somewhere else) what
        //Kind of filter and sort we want.
        public delegate IFilter FilterServiceResolver(string key);
        public delegate ISort SortServiceResolver(string key);


        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ISortNumbers, SortNumbers>()
                .AddTransient<PrimeFilter>()
                .AddTransient<EvenNumbersFilter>()
                .AddTransient<SortAscending>()
                .AddTransient<IGetNumberList, GetNumberList>()
                .AddSingleton(_configuration)
                .AddTransient<FilterServiceResolver>(serviceProvider => key =>
                {
                    return key switch
                    {
                        "Prime" => serviceProvider.GetService<PrimeFilter>(),
                        "Even" => serviceProvider.GetService<EvenNumbersFilter>(),
                        _ => throw new KeyNotFoundException()
                    };
                })
                .AddTransient<SortServiceResolver>(serviceProvider => key =>
                {
                    return key switch
                    {
                        "asc" => serviceProvider.GetService<SortAscending>(),
                        _ => throw new KeyNotFoundException()
                    };
                })
                .AddTransient<IFilterNumbers, FilterNumbers>()
                .BuildServiceProvider();


                //This could all be split out into separate classes if necessary
                //I've left it all in here as its 32 degrees and I'm melting.
                
                var getNumbers = serviceProvider.GetService<IGetNumberList>();
                var integerList = getNumbers.GetMyNumberList();

                //Get the filter numbers service and filter the numbers
                var filterNumbers = serviceProvider.GetService<IFilterNumbers>();
                var filteredNumbers = filterNumbers.applyFilter(integerList);

                Messages.numberFilterMessage();
                filteredNumbers.ForEach(x => Console.WriteLine($"{x}"));

                //Then get the sorting service and sort them
                var letSortSomeNumbers = serviceProvider.GetService<ISortNumbers>();
                var sortedNumbers = letSortSomeNumbers.applySort(filteredNumbers);

                Messages.numberSortedMessage();
                sortedNumbers.ForEach(x => Console.WriteLine($"{x}"));

                Console.ReadLine();
        }
    }
}
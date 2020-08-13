using System;
using System.Collections.Generic;
using BlueStone.Sorters;
using Microsoft.Extensions.Configuration;

namespace BlueStone
{
    public class GetNumberList : IGetNumberList
    {
        private readonly IConfiguration _configuration;

        public GetNumberList(IConfiguration config)
        {
            _configuration = config;
        }

        public List<int> GetMyNumberList()
        {
            //Get number string from configuration file 
            var input = _configuration["numbers"];

            //convert to a list of integers
            var integerList = Utilities.SplitString(input);
            return integerList;
        }
    }
}
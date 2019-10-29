// <copyright>
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Oana Leva</author>
using Newtonsoft.Json;
using refarmo.Models;
using refarmo.Models.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace refarmo.Utility
{
    public class GeoJsonDataSeeder
    {
        private readonly IDataRepository<FeatureCollection> _dataRepository;

        public GeoJsonDataSeeder(IDataRepository<FeatureCollection> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public void Seed()
        {
            FeatureCollection fc;
            using (StreamReader r = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\TestDataOrig.geojson"))
            {
                string jsonString = r.ReadToEnd();
                fc = JsonConvert.DeserializeObject<FeatureCollection>(jsonString);
            }

            _dataRepository.Add(fc);
        }
    }
}

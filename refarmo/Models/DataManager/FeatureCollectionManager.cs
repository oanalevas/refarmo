// <copyright>
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Oana Leva</author>
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using refarmo.Models.Repository;
using refarmo.Utility;

namespace refarmo.Models.DataManager
{
    public class FeatureCollectionManager : IDataRepository<FeatureCollection>
    {
        readonly RNTSContext _rContext;

        public FeatureCollectionManager(RNTSContext context)
        {
            _rContext = context;
        }

        public IEnumerable<FeatureCollection> GetAll()
        {
            return _rContext.FeatureCollection.Include(_rContext.GetIncludePaths(typeof(FeatureCollection))).ToList();
        }

        public void Add(FeatureCollection entity)
        {
            _rContext.FeatureCollection.Add(entity);
            _rContext.SaveChanges();
        }

    }
}
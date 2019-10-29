// <copyright>
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Oana Leva</author>
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using refarmo.Models;
using refarmo.Models.Repository;

namespace refarmo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataRepository<FeatureCollection> _featureCRepository;

        public HomeController(IDataRepository<FeatureCollection> featureCRepository)
        {
            _featureCRepository = featureCRepository;
        }

        public IActionResult Index()
        {
            FeatureCollection fc = _featureCRepository.GetAll().FirstOrDefault();
            return View(fc);
        }
        public JsonResult OnGetPoints()
        {

            FeatureCollection fc = _featureCRepository.GetAll().FirstOrDefault();
            return Json(fc);
        }
    }
}

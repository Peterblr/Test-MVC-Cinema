using Cinema.Data;
using Cinema.Models;
using Cinema.Repository.Interfaces;
using Cinema.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    //[Authorize]
    public class ProducerController : Controller
    {

        private readonly IProducer _service;

        public ProducerController(IProducer service)
        {
            _service = service;
        }

        //private SortModel ApplySort(string sortExpression)
        //{
        //    ViewData["SortParamFullName"] = "fullname";
        //    ViewData["SortParamBio"] = "biografy";
        //    ViewData["SortIconFullName"] = "";
        //    ViewData["SortIconBio"] = "";


        //    SortModel sortModel = new SortModel();

        //    switch (sortExpression.ToLower())
        //    {
        //        case "fullname_desc":
        //            sortModel.SortedOrder = SortOrder.Descending;
        //            sortModel.SortedProperty = "fullname";
        //            ViewData["SortIconFullName"] = "bi bi-file-arrow-up-fill";
        //            ViewData["SortParamFullName"] = "fullname";
        //            break;

        //        case "biografy":
        //            sortModel.SortedOrder = SortOrder.Ascending;
        //            sortModel.SortedProperty = "biografy";
        //            ViewData["SortIconBio"] = "bi bi-file-arrow-down-fill";
        //            ViewData["SortParamBio"] = "biografy_desc";
        //            break;
        //        case "biografy_desc":
        //            sortModel.SortedOrder = SortOrder.Descending;
        //            sortModel.SortedProperty = "biografy";
        //            ViewData["SortIconBio"] = "bi bi-file-arrow-up-fill";
        //            ViewData["SortParamBio"] = "biografy";
        //            break;
        //        default:
        //            sortModel.SortedOrder = SortOrder.Ascending;
        //            sortModel.SortedProperty = "fullname";
        //            ViewData["SortIconFullName"] = "bi bi-file-arrow-down-fill";
        //            ViewData["SortParamFullName"] = "fullname_desc";
        //            break;
        //    }

        //    return sortModel;
        //}



        public IActionResult Index(string sortExpression = "", string searchText = "", int pg = 1, int pageSize = 3)
        {
            //SortModel sortModel = ApplySort(sortExpression);

            PaginatedList<Producer> producers;

            SortModel sortModel = new SortModel();

            sortModel.AddColumn("fullname");
            sortModel.AddColumn("biografy");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.searchText = searchText;

            producers = _service.GetAll(sortModel.SortedProperty, sortModel.SortedOrder, searchText, pg, pageSize);

            //PaginatedList<Actor> retActor = new PaginatedList<Actor>(actors, pg, pageSize);

            //int totalRecords = ((PaginatedList<Actor>)actors).TotalRecords;

            var pager = new Pager(producers.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            //actors = actors.Skip((pg - 1) * pageSize).Take(pageSize).ToList();

            //const int pageSize = 3;

            //if (pg < 1)
            //{
            //    pg = 1;
            //}

            //int recsCount = _service.GetAll().Count();

            //var pager = new Pager(recsCount, pg, pageSize);

            //int recSkip = (pg - 1) * pageSize;

            //actors = _service.GetAll().Skip(recSkip).Take(pager.PageSize).ToList();

            //ViewBag.Pager = pager;


            //List<Actor> actors = _service.GetAll(sortModel.SortedProperty, sortModel.SortedOrder);



            return View(producers);

            //return View(retActor);
        }

        public IActionResult Create()
        {
            Producer producer = new Producer();

            return View(producer);
        }

        [HttpPost]
        public IActionResult Create(Producer producer)
        {
            try
            {
                _service.Add(producer);
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Producer producer = _service.GetById(id);

            return View(producer);
        }

        public IActionResult Edit(int id)
        {
            Producer producer = _service.GetById(id);

            return View(producer);
        }

        [HttpPost]
        public IActionResult Edit(Producer producer)
        {
            try
            {
                _service.Update(producer);
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Producer producer = _service.GetById(id);

            return View(producer);
        }

        [HttpPost]
        public IActionResult Delete(Producer producer)
        {
            try
            {
                _service.Delete(producer);
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}


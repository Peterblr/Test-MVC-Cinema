using Cinema.Data;
using Cinema.Models;
using Cinema.Repository.Interfaces;
using Cinema.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {

        private readonly IMovie _service;
        private readonly IActor _serviceActor;

        public MovieController(IMovie service, IActor serviceActor)
        {
            _service = service;
            _serviceActor = serviceActor;
        }

        private List<SelectListItem> GetActors()
        {
            var listActors = new List<SelectListItem>();

            PaginatedList<Actor> actors = _serviceActor.GetAll("FullName", SortOrder.Ascending, "", 1, 100);

            listActors = actors.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.FullName
            }).ToList();

            var item = new SelectListItem()
            {
                Value = "",
                Text = "---select---"
            };

            listActors.Insert(0, item);

            return listActors;
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

            PaginatedList<Movie> movies;

            SortModel sortModel = new SortModel();

            sortModel.AddColumn("title");
            sortModel.AddColumn("actor");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.searchText = searchText;

            movies = _service.GetAll(sortModel.SortedProperty, sortModel.SortedOrder, searchText, pg, pageSize);

            //PaginatedList<Movie> retMovie = new PaginatedList<Movie>(movies, pg, pageSize);

            //int totalRecords = ((PaginatedList<Movie>)movies).TotalRecords;

            var pager = new Pager(movies.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            //movies = movies.Skip((pg - 1) * pageSize).Take(pageSize).ToList();

            //const int pageSize = 3;

            //if (pg < 1)
            //{
            //    pg = 1;
            //}

            //int recsCount = _service.GetAll().Count();

            //var pager = new Pager(recsCount, pg, pageSize);

            //int recSkip = (pg - 1) * pageSize;

            //movies = _service.GetAll().Skip(recSkip).Take(pager.PageSize).ToList();

            //ViewBag.Pager = pager;


            //List<Movie> movies = _service.GetAll(sortModel.SortedProperty, sortModel.SortedOrder);



            return View(movies);

            //return View(retMovie);
        }

        public IActionResult Create()
        {
            Movie movie = new Movie();

            ViewBag.Actors = GetActors();

            return View(movie);
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            try
            {
                _service.Add(movie);
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Movie movie = _service.GetById(id);

            return View(movie);
        }

        public IActionResult Edit(int id)
        {
            Movie movie = _service.GetById(id);

            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            try
            {
                _service.Update(movie);
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Movie movie = _service.GetById(id);

            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            try
            {
                _service.Delete(movie);
            }
            catch
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}

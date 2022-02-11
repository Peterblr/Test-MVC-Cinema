using Cinema.Data;
using Cinema.Models;
using Cinema.Repository.Interfaces;
using Cinema.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Repository.Implementations
{
    public class ActorService : IActor
    {
        private readonly AppDbContext _context;

        public ActorService(AppDbContext context)
        {
            _context = context;
        }

        public Actor Add(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();

            return actor;
        }

        public Actor Update(Actor actor)
        {
            _context.Entry(actor).State = EntityState.Modified;
            _context.SaveChanges();

            return actor;
        }

        public Actor GetById(int id)
        {
            Actor actor = _context.Actors.Where(a => a.Id == id).FirstOrDefault();

            return actor;
        }

        //public List<Actor> GetAll()
        //{
        //    return _context.Actors.ToList();
        //}

        private List<Actor> DoSort(List<Actor> actors, string sortProperty, SortOrder sortOrder)
        {
            //List<Actor> actors = _context.Actors.ToList();



            if (sortProperty.ToLower() == "fullname")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    actors = actors.OrderBy(a => a.FullName).ToList();
                }
                else
                {
                    actors = actors.OrderByDescending(a => a.FullName).ToList();
                }
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    actors = actors.OrderBy(a => a.Biografy.Count()).ToList();
                }
                else
                {
                    actors = actors.OrderByDescending(a => a.Biografy.Count()).ToList();
                }
            }

            return actors;
        }

        public PaginatedList<Actor> GetAll(string sortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 3)
        {
            //List<Actor> actors = _context.Actors.ToList();
            List<Actor> actors;

            if (!string.IsNullOrEmpty(searchText))
            {
                actors = _context.Actors.Where(n => n.FullName.Contains(searchText) || n.Biografy.Contains(searchText)).ToList();
            }
            else
            {
                actors = _context.Actors.ToList();
            }

            actors = DoSort(actors, sortProperty, sortOrder);

            PaginatedList<Actor> retActor = new PaginatedList<Actor>(actors, pageIndex, pageSize);

            return retActor;
        }

        public Actor Delete(Actor actor)
        {
            _context.Entry(actor).State = EntityState.Deleted;
            _context.SaveChanges();

            return actor;
        }
    }
}

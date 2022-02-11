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
    public class ProducerService : IProducer
    {
        private readonly AppDbContext _context;

        public ProducerService(AppDbContext context)
        {
            _context = context;
        }

        public Producer Add(Producer producer)
        {
            _context.Producers.Add(producer);
            _context.SaveChanges();

            return producer;
        }

        public Producer Update(Producer producer)
        {
            _context.Entry(producer).State = EntityState.Modified;
            _context.SaveChanges();

            return producer;
        }

        public Producer GetById(int id)
        {
            Producer producer = _context.Producers.Where(a => a.Id == id).FirstOrDefault();

            return producer;
        }

        //public List<Actor> GetAll()
        //{
        //    return _context.Actors.ToList();
        //}

        private List<Producer> DoSort(List<Producer> producers, string sortProperty, SortOrder sortOrder)
        {
            //List<Actor> actors = _context.Actors.ToList();



            if (sortProperty.ToLower() == "fullname")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    producers = producers.OrderBy(a => a.FullName).ToList();
                }
                else
                {
                    producers = producers.OrderByDescending(a => a.FullName).ToList();
                }
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    producers = producers.OrderBy(a => a.Biografy.Count()).ToList();
                }
                else
                {
                    producers = producers.OrderByDescending(a => a.Biografy.Count()).ToList();
                }
            }

            return producers;
        }

        public PaginatedList<Producer> GetAll(string sortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 3)
        {
            //List<Actor> actors = _context.Actors.ToList();
            List<Producer> producers;

            if (!string.IsNullOrEmpty(searchText))
            {
                producers = _context.Producers.Where(n => n.FullName.Contains(searchText) || n.Biografy.Contains(searchText)).ToList();
            }
            else
            {
                producers = _context.Producers.ToList();
            }

            producers = DoSort(producers, sortProperty, sortOrder);

            PaginatedList<Producer> retProducer = new PaginatedList<Producer>(producers, pageIndex, pageSize);

            return retProducer;
        }

        public Producer Delete(Producer producer)
        {
            _context.Entry(producer).State = EntityState.Deleted;
            _context.SaveChanges();

            return producer;
        }
    }
}


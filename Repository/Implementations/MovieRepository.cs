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
    public class MovieRepository : IMovie
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }



        public Movie Add(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return movie;
        }

        public Movie Update(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();

            return movie;
        }

        public Movie GetById(int id)
        {
            Movie movie = _context.Movies.Where(a => a.MovieId == id)
                .Include(a => a.Actors)
                .Include(c => c.Comments)
                .FirstOrDefault();

            return movie;
        }

        //public List<Movie> GetAll()
        //{
        //    return _context.Movies.ToList();
        //}

        private List<Movie> DoSort(List<Movie> movies, string sortProperty, SortOrder sortOrder)
        {
            //List<Movie> movies = _context.Movies.ToList();



            if (sortProperty.ToLower() == "fullname")
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    movies = movies.OrderBy(a => a.Title).ToList();
                }
                else
                {
                    movies = movies.OrderByDescending(a => a.Title).ToList();
                }
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    movies = movies.OrderBy(a => a.Description.Count()).ToList();
                }
                else
                {
                    movies = movies.OrderByDescending(a => a.Description.Count()).ToList();
                }
            }

            return movies;
        }

        public PaginatedList<Movie> GetAll(string sortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 3)
        {
            //List<Movie> movies = _context.Movies.ToList();
            List<Movie> movies;

            if (!string.IsNullOrEmpty(searchText))
            {
                movies = _context.Movies.Where(n => n.Title.Contains(searchText) || n.Description.Contains(searchText))
                    .Include(n => n.Actors)
                    .ToList();
            }
            else
            {
                movies = _context.Movies.Include(n => n.Actors).ToList();
            }

            movies = DoSort(movies, sortProperty, sortOrder);

            PaginatedList<Movie> retMovie = new PaginatedList<Movie>(movies, pageIndex, pageSize);

            return retMovie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Deleted;
            _context.SaveChanges();

            return movie;
        }

        public Comment GetComment(int commentId)
        {
            return _context.Comments
                .Include(c => c.CommentItem)
                .FirstOrDefault(c => c.CommentId == commentId);
        }

        public Comment Add(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment;
        }

    }
}

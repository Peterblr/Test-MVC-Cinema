using Cinema.Models;
using Cinema.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Repository.Interfaces
{
    public interface IMovie
    {
        PaginatedList<Movie> GetAll(string sortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 3);

        Movie GetById(int id);

        Movie Add(Movie movie);

        Movie Update(Movie movie);

        Movie Delete(Movie movie);
    }
}

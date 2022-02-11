using Cinema.Models;
using Cinema.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Repository.Interfaces
{
    public interface IActor
    {
        PaginatedList<Actor> GetAll(string sortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 3);

        Actor GetById(int id);

        Actor Add(Actor actor);

        Actor Update(Actor actor);

        Actor Delete(Actor actor);
    }
}

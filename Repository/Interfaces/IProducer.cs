using Cinema.Models;
using Cinema.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Repository.Interfaces
{
    public interface IProducer
    {
        PaginatedList<Producer> GetAll(string sortProperty, SortOrder sortOrder, string searchText = "", int pageIndex = 1, int pageSize = 3);

        Producer GetById(int id);

        Producer Add(Producer producer);

        Producer Update(Producer producer);

        Producer Delete(Producer producer);
    }
}

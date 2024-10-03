using RentalStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        int GetMaxId();
        Category GetCategoryByName(string name);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Dapper.CategoryAgg
{
    public static class CategoryQueries
    {
        public const string GetAll = "Select Id ,Title , ImagePath from Categories Where IsDeleted =0";
     
    }
}

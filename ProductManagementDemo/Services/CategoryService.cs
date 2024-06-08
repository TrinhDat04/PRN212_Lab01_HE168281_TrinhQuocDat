using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository iCategoryRepository;
        public CategoryService()
        {
            iCategoryRepository = new CategoryRepository();

        }
        public List<Category> GetCategory()
        {
            return iCategoryRepository.GetCategories();
        }
        public string GetCategoryName(int id)
        {
            return iCategoryRepository.GetCategoryByID(id);
        }
    }
}

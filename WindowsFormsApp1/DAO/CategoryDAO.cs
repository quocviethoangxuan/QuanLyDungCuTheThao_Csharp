using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DTO;

namespace WindowsFormsApp1.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance 
        {   get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance;  }
            private set => instance = value; 
        }
        private CategoryDAO() { }

        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select * from Category";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                Category category = new Category(dr);
                list.Add(category);
            }
            return list;
        }
        public Category GetCategoryById(int id)
        {
            Category category = null;
            string query = "select * from Category where id = " +id;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                category = new Category(dr);
                return category;
            }
            return category;
        }
        public Category GetCategoryByName(string name)
        {
            Category category = null;
            string query = $"select * from Category where name = N'{name}'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                category = new Category(dr);
                return category;
            }
            return category;
        }
        public void AddCate(string name)
        {
            string query = $"insert Category(name) values (N'{name}')";
            DataProvider.Instance.ExcuteNonQuery(query);
        }
        public void UpdateCate(int id, string name)
        {
            string query = $"update Category set name=N'{name}' where id={id}";
            DataProvider.Instance.ExcuteNonQuery(query);
        }
    }
}

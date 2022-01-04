using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DTO
{
    public class Category
    {
        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public Category(DataRow row)
        {
            this.id = (int)row["id"];
            this.name = row["name"].ToString();
        }
        private string name;
        private int id;
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
    }
}

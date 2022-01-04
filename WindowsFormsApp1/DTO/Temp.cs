using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DTO
{
    public class Temp
    {
        private int id;
        private string name;
        private int amount;

        public Temp(int id, string name, int amount)
        {
            this.id = id;
            this.name = name;
            this.amount = amount;
        }

        public Temp(DataRow row)
        {
            this.id = (int)row["id"];
            this.name = row["name"].ToString();
            this.amount = (int)row["amount"];
        }
        public int Amount { get => amount; set => amount = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
    }
}

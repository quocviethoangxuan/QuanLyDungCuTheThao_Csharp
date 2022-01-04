using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DTO
{
    public class SportEquipment
    {
        private int id;
        private string name;
        private int idCategory;
        private int price;
        private int amount;
        private string picture;
        public SportEquipment(int id, string name, int idCategory, int price, int amount, string picture)
        {
            this.id = id;
            this.name = name;
            this.idCategory = idCategory;
            this.price = price;
            this.amount = amount;
            this.picture = picture;
        }
        public SportEquipment(DataRow row)
        {
            this.id = (int)row["id"];
            this.name = row["name"].ToString();
            this.idCategory = (int)row["idCategory"];
            this.price = (int)row["price"];
            this.amount = (int)row["amount"];
            this.picture = row["picture"].ToString();
        }
        public string Picture { get => picture; set => picture = value; }
        public int Amount { get => amount; set => amount = value; }
        public int Price { get => price; set => price = value; }
        public int IdCategory { get => idCategory; set => idCategory = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
    }
}

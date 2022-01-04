using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DTO;

namespace WindowsFormsApp1.DAO
{
    public class SportEquipmentDAO
    {
        private static SportEquipmentDAO instance;

        public static SportEquipmentDAO Instance
        {
            get { if (instance == null) instance = new SportEquipmentDAO(); return SportEquipmentDAO.instance; }
            private set => instance = value;
        }
        private SportEquipmentDAO() { }

        public List<SportEquipment> GetSEByIdCategory(int id)
        {
            List<SportEquipment> list = new List<SportEquipment>();
            string query = "select * from SportEquipment where idCategory = "+id;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                SportEquipment se = new SportEquipment(row);
                list.Add(se);
            }
            return list;
        }
        public int GetAmountByIdSE(int id)
        {
            return (int)DataProvider.Instance.ExcuteScalar("Select amount from SportEquipment where id="+id);
        }
        public DataTable GetListSE()
        {
            string query = "Select id as [Mã dụng cụ], name as [Tên dụng cụ], idCategory as [Mã loại], amount as [Số lượng], price as [Giá]   From SportEquipment ";
            return DataProvider.Instance.ExcuteQuery(query);
        }
        public void UpdateSEbyAmount(int id, int amount)
        {
            string query = $"update SportEquipment set amount=amount-{amount} where id={id}";
            DataProvider.Instance.ExcuteNonQuery(query);
        }
        public void AddSE(string name, int idCategory, int price, int amount)
        {
            string query = $"INSERT SportEquipment(name, idCategory, price, amount) VALUES (N'{name}', {idCategory}, {price}, {amount})";
            DataProvider.Instance.ExcuteNonQuery(query);
        }
        public void UpdateSE(int id, string name, int idCategory, int price, int amount)
        {
            string query = $"update SportEquipment set name=N'{name}', idCategory={idCategory}, price={price}, amount={amount} where id={id}";
            DataProvider.Instance.ExcuteNonQuery(query);
        }

    }
}

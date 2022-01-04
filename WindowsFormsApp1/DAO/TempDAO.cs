using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAO
{
    public class TempDAO
    {
        private static TempDAO instance;

        public static TempDAO Instance 
        {
            get { if (instance == null) instance = new TempDAO(); return instance; }
            set => instance = value;
        }
        private TempDAO() { }

        public void InsertTemp(int id, string name, int count)
        {
            string sql = $"insert temp(id, name, amount) values ({id}, N'{name}', {count})";
            DataProvider.Instance.ExcuteNonQuery(sql);
        }
        public int GetAmount(int id)
        {
            return (int)DataProvider.Instance.ExcuteScalar("Select amount from temp where id=" + id);
        }
    }
}

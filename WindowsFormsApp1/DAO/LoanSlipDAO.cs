using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAO
{
    public class LoanSlipDAO
    {
        private static LoanSlipDAO instance;

        public static LoanSlipDAO Instance 
        { 
            get { if(instance == null) instance = new LoanSlipDAO(); return instance; } 
            private set => instance = value; 
        }

        private LoanSlipDAO() { }
        
        public void InsertLoan()
        {
            string  sql = "insert LoanSlip(DateCheckIn, DateCheckOut, status) values (getdate(), null, 0)";
            DataProvider.Instance.ExcuteNonQuery(sql);
        }
        public int GetMaxID()
        {
            return (int)DataProvider.Instance.ExcuteScalar("Select max(id) from LoanSlip");
        }
        public DataTable GetLoanSlip()
        {
            string sql = "Select id as [Mã], DateCheckIn as [Ngày vào], DateCheckOut as [Ngày trả] From LoanSlip where  status = 1";
            return DataProvider.Instance.ExcuteQuery(sql);
        }
    }
}

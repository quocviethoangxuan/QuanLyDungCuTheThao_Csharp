using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAO
{
    public class LoanSlipInfoDAO
    {
        private static LoanSlipInfoDAO instance;

        public static LoanSlipInfoDAO Instance
        { 
            get { if(instance == null) instance = new LoanSlipInfoDAO(); return instance; } 
            set => instance = value; 
        }
        private LoanSlipInfoDAO() { }

        public void InsertLoanInfo(int idLoan, int idSE, int count)
        {
            string sql = $"insert LoanSlipInfo(idLoanSlip, idSE, count) values ({idLoan}, {idSE}, {count})";
            DataProvider.Instance.ExcuteNonQuery(sql);
        }
    }
}

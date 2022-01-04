using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DTO
{
    public class LoanSlipInfo
    {
        private int id;
        private int idLoanSlip;
        private int idSE;
        private int count;

        public LoanSlipInfo(int id, int idLoanSlip, int idSE, int count)
        {
            this.id = id;
            this.idLoanSlip = idLoanSlip;
            this.idSE = idSE;
            this.count = count;
        }

        public LoanSlipInfo(DataRow Row)
        {
            this.id = (int)Row["id"];
            this.idLoanSlip = (int)Row["idLoanSlip"];
            this.idSE = (int)Row["idSE"];
            this.count = (int)Row["count"];
        }
        public int Count { get => Count1; set => Count1 = value; }
        public int Count1 { get => Count2; set => Count2 = value; }
        public int Count2 { get => Count3; set => Count3 = value; }
        public int Count3 { get => count; set => count = value; }
    }
}

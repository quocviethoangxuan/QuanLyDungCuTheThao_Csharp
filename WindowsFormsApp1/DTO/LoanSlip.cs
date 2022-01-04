using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DTO
{
    public class LoanSlip
    {
        private int id;
        private DateTime DateCheckIn;
        private DateTime DateCheckOut;
        private int status;

        public LoanSlip(int id, DateTime DateCheckIn, DateTime DateCheckOut, int status)
        {
            this.id = id;
            this.DateCheckIn = DateCheckIn;
            this.DateCheckOut = DateCheckOut;
            this.status = status;
        }

        public LoanSlip(DataRow Row)
        {
            this.id = (int)Row["id"];
            this.DateCheckIn = (DateTime)Row["DateCheckIn"];
            this.DateCheckOut = (DateTime)Row["DateCheckOut"];
            this.status = (int)Row["status"];
        }
        public int Id { get => id; set => id = value; }
        public DateTime DateCheckIn1 { get => DateCheckIn; set => DateCheckIn = value; }
        public DateTime DateCheckOut1 { get => DateCheckOut; set => DateCheckOut = value; }
        public int status1 { get => status; set => status = value; }
    }
}

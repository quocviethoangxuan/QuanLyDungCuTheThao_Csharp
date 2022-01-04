using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {  
          get {if(instance == null) instance = new AccountDAO(); return instance;}
          set {instance = value;}
        }

        private AccountDAO() { }

        public bool Login(string username, string password)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] pwHash = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hashPass = "";
            foreach (byte b in pwHash)
            {
                hashPass += b;
            }
            MessageBox.Show(hashPass);
            string query = "Select * From dbo.Account Where username = '"+username+"' and password = '"+hashPass+"' ";
            DataTable result = DataProvider.Instance.ExcuteQuery(query);
            return result.Rows.Count > 0;
        }
    }
}

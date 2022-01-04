using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;
using WindowsFormsApp1.DTO;

namespace WindowsFormsApp1
{
    public partial class fManager : Form
    {
        BindingSource ListTemp = new BindingSource();
        BindingSource ListLoan = new BindingSource();
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbSCE2.DataSource = listCategory;
            cbSCE2.DisplayMember = "name";
        }
        void LoadSEByIdCategory(int id)
        {
            List<SportEquipment> listSE = SportEquipmentDAO.Instance.GetSEByIdCategory(id);
            cbSE.DataSource = listSE;
            cbSE.DisplayMember = "name";
        }
        void LoadSEList()
        {
            string query = "Select s.name as [Tên dụng cụ], c.name as [Loại dụng cụ], s.amount as [Số lượng] from SportEquipment as s join Category as c on s.idCategory = c.id";
            dgvList.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void LoadNUD(int id)
        {
            int amount = SportEquipmentDAO.Instance.GetAmountByIdSE(id);
            nudSE.Maximum = amount;
        }
        void LoadTemp()
        {
            string query = "Select t.id as [Mã dụng cụ] , t.name as [Tên dụng cụ], t.amount as [Số lượng] from Temp as t";
            ListTemp.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void DeleteTemp()
        {
            string query = "Delete From temp";
            dgvTemp.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void AddBinding()
        {
            tbID.DataBindings.Add(new Binding("Text", dgvTemp.DataSource, "Mã dụng cụ"));
            txbIDLoan.DataBindings.Add(new Binding("Text", dgvLoan.DataSource, "Mã phiếu"));
        }
        void LoadTime()
        {
            DateTime date = DateTime.Now;
            txbTime.Text = date.ToLongDateString();
        }
        void LoadLoan()
        {
            string query = "Select id as [Mã phiếu], DateCheckIn as [Ngày lập phiếu] From LoanSlip Where status = 0";
            ListLoan.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        public fManager()
        {
            InitializeComponent();
            dgvLoan.DataSource = ListLoan;
            dgvTemp.DataSource = ListTemp;
            LoadSEList();
            LoadTemp();
            LoadLoan();
            LoadCategory();
            LoadTime();
            AddBinding();
        }

        private void quảnLýLoạiDụngCụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fMain f = new fMain();
            f.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbSCE2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            Category selected = cb.SelectedItem as Category;
            id = selected.Id;
            LoadSEByIdCategory(id);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int idSE = (cbSE.SelectedItem as SportEquipment).Id;
            string name = (cbSE.SelectedItem as SportEquipment).Name;
            int count = (int)nudSE.Value;
            if (count <= 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng >0","Thông báo");
                return;
            }
            try
            {
                TempDAO.Instance.InsertTemp(idSE, name, count);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadTemp();
            //LoanSlipDAO.Instance.InsertLoan();
            //LoanSlipInfoDAO.Instance.InsertLoanInfo(LoanSlipDAO.Instance.GetMaxID(), idSE, count);
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            LoanSlipDAO.Instance.InsertLoan();
            foreach (DataGridViewRow row in dgvTemp.Rows)
            {
                if(dgvTemp.Rows.Count > 0)
                {
                    if (row.Cells[0].Value != null)
                    {
                        LoanSlipInfoDAO.Instance.InsertLoanInfo(LoanSlipDAO.Instance.GetMaxID(), (int)row.Cells[0].Value, (int)row.Cells[2].Value);
                        SportEquipmentDAO.Instance.UpdateSEbyAmount((int)row.Cells[0].Value, (int)row.Cells[2].Value);
                        MessageBox.Show("Tạo phiếu thành công!", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("");
                    return ;
                }
            }
            LoadSEList();
            DeleteTemp();

        }

        private void cbSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            SportEquipment selected = cb.SelectedItem as SportEquipment;
            id = selected.Id;
            LoadNUD(id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbID.Text);
            string sql = "delete from temp where id="+id;
            dgvTemp.DataSource = DataProvider.Instance.ExcuteQuery(sql);
            LoadTemp();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbID.Text);
            if (TempDAO.Instance.GetAmount(id) < SportEquipmentDAO.Instance.GetAmountByIdSE(id))
            {
                string sql = "update temp set amount=amount+1 where id=" + id;
                dgvTemp.DataSource = DataProvider.Instance.ExcuteQuery(sql);
                LoadTemp();
            }
            else MessageBox.Show("Số lượng đã tối đa");
        }

        private void btnDown_Click(object sender, EventArgs e)
        { 
            int id = Convert.ToInt32(tbID.Text);
            if (TempDAO.Instance.GetAmount(id) > 1)
            {
                string sql = "update temp set amount=amount-1 where id=" + id;
                dgvTemp.DataSource = DataProvider.Instance.ExcuteQuery(sql);
                LoadTemp();
            }
            else MessageBox.Show("Số lượng bằng 1 không thể giảm, hãy thử xóa");
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDLoan.Text);
            string query = $"UPDATE LoanSlip set DateCheckOut = getdate(), status = 1 where id = {id} ";
            DataProvider.Instance.ExcuteNonQuery(query);
            MessageBox.Show("Trả thành công!", "Thông báo");
            LoadLoan();
        }
    }
}

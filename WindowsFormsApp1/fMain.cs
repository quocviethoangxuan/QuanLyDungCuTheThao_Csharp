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
    public partial class fMain : Form
    {
        BindingSource ListSE = new BindingSource();
        BindingSource ListCSE = new BindingSource();
        void LoadSEList()
        {
            string query = "Select id as [Mã dụng cụ], name as [Tên dụng cụ], idCategory as [Mã loại], amount as [Số lượng], price as [Giá]   From SportEquipment ";
            ListSE.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void LoadCategory(ComboBox cb)
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cb.DataSource = listCategory;
            cb.DisplayMember = "name";
        }
        void LoadCSEList()
        {
            string query = "Select id as [Mã loại], name as [Tên loại] From Category";
            ListCSE.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void SearchSE(string str)
        {
            string query = $"Select id as [Mã dụng cụ], name as [Tên dụng cụ], idCategory as [Mã loại], amount as [Số lượng], price as [Giá]   From SportEquipment Where name like N'%{str}%' ";
            ListSE.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void LoadReport()
        {
            dgvReport.DataSource = LoanSlipDAO.Instance.GetLoanSlip();
        }
        void AddBinding()
        {
            txbIDSE.DataBindings.Add(new Binding("Text", dgvSE.DataSource, "Mã dụng cụ"));
            txbNameSE.DataBindings.Add(new Binding("Text", dgvSE.DataSource, "Tên dụng cụ", true, DataSourceUpdateMode.Never));
            txbPrice.DataBindings.Add(new Binding("Text", dgvSE.DataSource, "Giá", true, DataSourceUpdateMode.Never));
            txbAmount.DataBindings.Add(new Binding("Text", dgvSE.DataSource, "Số lượng", true, DataSourceUpdateMode.Never));
            //
            txbIDCSE.DataBindings.Add(new Binding("Text", dgvCSE.DataSource, "Mã loại"));
            txbNameCSE.DataBindings.Add(new Binding("Text", dgvCSE.DataSource, "Tên loại", true, DataSourceUpdateMode.Never));
        }
        public fMain()
        {
            InitializeComponent();
            dgvSE.DataSource = ListSE;
            dgvCSE.DataSource = ListCSE;
            LoadSEList();
            LoadCSEList();
            LoadCategory(cbCSE);
            AddBinding();
        }

        private void txbIDSE_TextChanged(object sender, EventArgs e)
        {
            if (dgvSE.SelectedCells[0].OwningRow.Cells["Mã loại"].Value != null)
            {
                if (dgvSE.SelectedCells.Count > 0)
                {
                    int id = (int)dgvSE.SelectedCells[0].OwningRow.Cells["Mã loại"].Value;
                    Category category = CategoryDAO.Instance.GetCategoryById(id);
                    cbCSE.SelectedItem = category;
                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbCSE.Items)
                    {
                        if (item.Id == category.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbCSE.SelectedIndex = index;
                }
            }
        }

        private void btnAddSE_Click(object sender, EventArgs e)
        {
            string name = txbNameSE.Text;
            int idCategory = (cbCSE.SelectedItem as Category).Id;
            int price = Convert.ToInt32(txbPrice.Text);
            int amount = Convert.ToInt32(txbAmount.Text);
            SportEquipmentDAO.Instance.AddSE(name, idCategory, price, amount);
            MessageBox.Show("Đã thêm dụng cụ mới");
            LoadSEList();
        }

        private void btnConfigSE_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDSE.Text);
            string name = txbNameSE.Text;
            int idCategory = (cbCSE.SelectedItem as Category).Id;
            int price = Convert.ToInt32(txbPrice.Text);
            int amount = Convert.ToInt32(txbAmount.Text);
            SportEquipmentDAO.Instance.UpdateSE(id ,name, idCategory, price, amount);
            MessageBox.Show("Đã chỉnh sửa dụng cụ");
            LoadSEList();
        }

        private void btnDeleteSE_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDSE.Text);
            if (MessageBox.Show("Bạn có muốn xóa dụng cụ này?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            else
            {
                string sql = "delete from SportEquipment where id =" + id;
                DataProvider.Instance.ExcuteNonQuery(sql);
            }
            LoadSEList();
        }

        private void btnSearchSE_Click(object sender, EventArgs e)
        {
            SearchSE(txbSearchSE.Text);
        }

        private void btnAddCSE_Click(object sender, EventArgs e)
        {
            string name = txbNameCSE.Text;
            if (CategoryDAO.Instance.GetCategoryByName(name) == null)
            {
                CategoryDAO.Instance.AddCate(name);
                MessageBox.Show("Đã thêm loại dụng cụ mới");
                LoadCSEList();
            }
            else MessageBox.Show("Đã tồn tại loại dụng cụ");
        }

        private void btnConfigCSE_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDCSE.Text);
            string name = txbNameCSE.Text;
            CategoryDAO.Instance.UpdateCate(id, name);
            MessageBox.Show("Đã chỉnh sửa dụng cụ");
            LoadCSEList();
        }

        private void btnDeleteCSE_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDCSE.Text);
            if (MessageBox.Show("Bạn có muốn xóa loại dụng cụ này?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            else
            {
                string sql = "delete from Category where id =" + id;
                DataProvider.Instance.ExcuteNonQuery(sql);
            }
            LoadCSEList();
        }

        private void btnSee_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvReport.SelectedCells[0].OwningRow.Cells["Mã"].Value != null)
            {
                if (dgvReport.SelectedCells.Count > 0)
                {
                    int id = (int)dgvReport.SelectedCells[0].OwningRow.Cells["Mã"].Value;
                    string sql = $"select s.name as [Tên dụng vụ], lf.count as [Số lượng] from LoanSlipInfo as lf join SportEquipment as s on lf.idSE = s.id where lf.idLoanSlip = {id}";
                    dgvReportInfo.DataSource = DataProvider.Instance.ExcuteQuery(sql);
                }
            }
        }
    }
}

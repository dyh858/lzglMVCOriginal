using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Models;

namespace LzglForm
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("确定要退出吗？", "退出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rt == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user=this.txtUser.Text;
            string pwd=this.txtPassword.Text;
            if (new SysAdminManager().Login(user,pwd))
            {
                FrmMain frmMain = new FrmMain();
                frmMain.Show();
                this.Hide();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

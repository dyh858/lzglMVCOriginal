using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LzglForm
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //初始化字符串
            string str =this.txtSource.Text;
            //定义正则表达式规则
            //Regex reg = new Regex("^\\d{2}$");
            //返回一个结果集
            if (new Regex("^\\d{17}[0-9X]{1}$").Match(str).Success)
            {
                this.rtxtResult.AppendText(str);
            }
            /*
            MatchCollection result = reg.Matches(str);
            //遍历每个结果
             foreach (Match m in result)                 
            {
                //输出结果
                this.rtxtResult.AppendText(m.ToString() + Environment.NewLine); 
             } 
             */
        }
    }
}

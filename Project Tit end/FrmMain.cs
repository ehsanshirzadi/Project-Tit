using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace Project_Tit_end
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                var resul = fBrowser.ShowDialog();
                if (resul == DialogResult.OK) txtBrowser.Text = fBrowser.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "خطای رخ داده است ");
            }
        }
        private string MakeReg(string e)
        {
            try
            {
                string reg = "";
                var sp = e.ToLower().Split(' ').ToList();
                foreach (var item in sp)
                {
                    if (item == "or")
                    {
                        reg = reg + "|";
                    }
                    else if (item == "and")
                    {
                        reg = reg + "\\s*";
                    }
                    else if (item == "") continue;
                    else
                    {
                        reg = reg + "(" + item + ")";
                    }
                }
                return reg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "خطای رخ داده است ");
                return "";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lbFiles.Items.Clear();
                if (string.IsNullOrEmpty(txtBrowser.Text)) return;
                var fi = Directory.GetFiles(txtBrowser.Text, "*.txt").ToList();
                if (fi.Count == 0)
                {
                    MessageBox.Show("آدرس مورد نظر حاوی فایل متنی نیست");
                    return;
                }
                pbStart.Value = 0;
                pbStart.Maximum = fi.Count;
                if (string.IsNullOrWhiteSpace(txtSearch.Text)) MessageBox.Show("عبارت مورد جستجو را وارد کنید");
                foreach (var item in fi)
                {
                    var fii = File.ReadAllText(item).ToLower();
                    var myreg = MakeReg(txtSearch.Text);
                    var mymatch = DoMatch(fii, myreg);
                    lbFiles.Items.Add(item + "  " + mymatch);
                    pbStart.Value += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "خطای رخ داده است ");
            }
        }
        private bool DoMatch(string mainText, string reg)
        {
            try
            {
                if (string.IsNullOrEmpty(reg)) return false;
                if (Regex.IsMatch(mainText, reg))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "خطای رخ داده است ");
                return false;
            }
        }
    }
}

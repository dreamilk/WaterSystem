using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WaterSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private Utils.User user = null;
        private Boolean result = false;
        private String name = "";
        private String password = "";

        public void SetResult(Boolean b)
        {
            this.result = b;
        }

        public void SetUser(Utils.User u)
        {
            this.user = u;
        }

        public Utils.User GetUser()
        {
            return user;
        }

        public Boolean getResult()
        {
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            password = textBox2.Text;
            if (MyTools.IsUser(name, password))
            {
                result = true;
                user = new Utils.User(name, password, MyTools.getPermission(name, password));
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }
        }

        private void button_cancle_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register(this);
            this.Hide();
            register.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("请与管理员进行联系！ QQ:1763443263");
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://hae.hust.edu.cn/");
        }
    }
}

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
    public partial class Register : Form
    {
        public Register(Login w)
        {
            InitializeComponent();
            this.w = w;
        }

        private Login w = null;
        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            w.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsCorrect())
            {
                if (!MyTools.HaveUser(textBox1.Text))
                {
                    if(MyTools.Register(textBox1.Text, textBox2.Text)){
                        w.SetUser(new Utils.User(textBox1.Text,textBox2.Text,0));
                        this.Close();
                        w.SetResult(true);
                        w.Close();

                    }
                    else
                    {
                        MessageBox.Show("注册失败");
                    }
                   
                }
                else
                {
                    MessageBox.Show("该用户已经存在，请重新注册");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                
            }
            else
            {
                MessageBox.Show("两次输入密码不一致,请重新设置密码\n或者您输入的密码过长");
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private Boolean IsCorrect()
        {
            if (textBox2.Text.Equals(textBox3.Text)&&textBox2.Text.Length<10)
            {
                return true;
            }
            return false;
        }
    }
}

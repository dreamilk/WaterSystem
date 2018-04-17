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
    public partial class Authority : Form
    {

        private List<Utils.User> list;

        public Authority()
        {
            InitializeComponent();        
        }

        private void Authority_Load(object sender, EventArgs e)
        {

            list = MyTools.GetUserList();

            for (int i = 0; i < list.Count; i++)
            {
                comboBox1.Items.Add(list[i].getName());
            }

            //第一次加载显示第一个用户
            comboBox1.SelectedIndex = 0;
            setInformation(0);
          
        }

        //得到权限  
        private String getAuthority(int i)
        {
            if (list[i].getPermission() == 0)
            {
                
                return "普通用户";
             
            }
            else
            {
               
                return "管理员";
            }
        }

        private void setInformation(int i)
        {
            username.Text = list[i].getName();
            password.Text = list[i].getPassword();
            right.Text = getAuthority(i);
            textBox1.Text = username.Text;
            textBox2.Text = password.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setInformation(comboBox1.SelectedIndex);
        }

        //重新加载当前布局
        private void reloadFrom(object sender, EventArgs e)
        {
            list = null;
            comboBox1.Items.Clear();
            Authority_Load(sender, e);
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (MyTools.changeUser(username.Text, textBox2.Text))
            {
                MessageBox.Show("修改成功");
                reloadFrom(sender, e);
            }
            else
            {
                MessageBox.Show("修改失败");
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (MyTools.HaveUser(textBox1.Text))
            {
                MessageBox.Show("用户已经存在");
            }
            else
            {
                if (MyTools.Register(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("添加普通用户成功");
                    reloadFrom(sender, e);
                }
                else
                {
                    MessageBox.Show("添加普通用户失败");
                }
            }
        }

        private void delect_Click(object sender, EventArgs e)
        {
            //判断是否为管理员，不能删除管理员
            if (list[comboBox1.SelectedIndex].getPermission() != 1)
            {
                if (MyTools.delectUser(username.Text.Trim()))
                {
                    MessageBox.Show("删除用户成功");
                    reloadFrom(sender, e);
                }
            }
            else
            {
                MessageBox.Show("不能删除管理员");
            }
        }
    }
}

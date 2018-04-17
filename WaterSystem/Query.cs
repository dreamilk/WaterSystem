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
    public partial class Query : Form
    {
        public Query()
        {
            InitializeComponent();
        }

        private void Query_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            //设置查询条件2不可输入
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox3.Enabled = false;
        }

        private SqlDataSet MySqlDataSet = null;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("表一历年水库来水量"))
            {
                MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM      表一历年水库来水量");
                this.dataGridView1.DataSource = MySqlDataSet.GetBindingSource();
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("表二水库来水量预测表"))
            {
                MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM     表二水库来水量预测表");
                this.dataGridView1.DataSource = MySqlDataSet.GetBindingSource();
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("表三灌溉面积统计表"))
            {
                MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM      表三灌溉面积统计表");
                this.dataGridView1.DataSource = MySqlDataSet.GetBindingSource();
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("表四灌溉制度表"))
            {
                MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM      表四灌溉制度表");
                this.dataGridView1.DataSource = MySqlDataSet.GetBindingSource();
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("表五农作物需水总量表"))
            {
                MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM      表五农作物需水总量表");
                this.dataGridView1.DataSource = MySqlDataSet.GetBindingSource();
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("表六计算结果统计表"))
            {
                MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM      表六计算结果统计表");
                this.dataGridView1.DataSource = MySqlDataSet.GetBindingSource();
            }

            foreach(DataGridViewColumn c in dataGridView1.Columns)
            {
                comboBox2.Items.Add(c.Name);
                comboBox3.Items.Add(c.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s10 = comboBox2.GetItemText(comboBox2.SelectedItem);
            String s11 = textBox1.Text;
            String s20 = comboBox3.GetItemText(comboBox3.SelectedItem);
            String s21 = textBox2.Text;
            if (s11!="")
            {
                if (s21 == "")
                {
                    MySqlDataSet = MyTools.queryData(comboBox1.GetItemText(comboBox1.SelectedItem), s10, s11);
                    this.dataGridView1.DataSource = MySqlDataSet.GetBindingSource();
                }
                else
                {
                    MySqlDataSet = MyTools.queryData(comboBox1.GetItemText(comboBox1.SelectedItem), s10, s11, s20, s21);
                    this.dataGridView1.DataSource = MySqlDataSet.GetBindingSource();
                }
            }
            else
            {
                MessageBox.Show("请选择查询条件");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //条件2可以选择
            comboBox3.Enabled = true;
            textBox2.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }
    }
}

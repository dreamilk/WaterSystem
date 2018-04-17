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
    public partial class Main : Form
    {
        public Main(Utils.User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private Utils.User user = null;

        private void Main_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            this.tabControl1.SelectTab(0);
            timer1.Start();

            if (user.getPermission() == 1)
            {
                this.Text = this.Text + "   | 管理员："+user.getName();
                toolStripStatusLabelmessage.Text = "欢迎管理员用户";
            }
            else
            {
                this.Text = this.Text + "   | 普通用户：" + user.getName();
                toolStripStatusLabelmessage.Text = "普通用户操作有限";

                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
            }

            toolStripStatusLabelresult.Text = "登陆成功";

            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.button8,"选择一行删除数据");
            toolTip1.SetToolTip(this.button7,"末端添加一行数据");
            toolTip1.SetToolTip(this.button6,"保存已经修改内容");
            toolTip1.SetToolTip(this.button2, "计算之前注意保存修改");
            toolTip1.SetToolTip(this.button1, "计算之前注意保存修改");
            toolTip1.SetToolTip(this.button3, "计算之前注意保存修改");
        }

        private SqlDataSet MySqlDataSet = new SqlDataSet(null, null, null);

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("表一历年水库来水量"))
            {
                MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM      表一历年水库来水量");
                this.dataGridView1.DataSource = MySqlDataSet.GetBindingSource();
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("表二水库来水量预测表"))
            {
                MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM     表二水库来水量预测表");
                this.dataGridView2.DataSource = MySqlDataSet.GetBindingSource();
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
                this.dataGridView2.DataSource = MySqlDataSet.GetBindingSource();
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals("表六计算结果统计表"))
            {
                MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM      表六计算结果统计表");
                this.dataGridView2.DataSource = MySqlDataSet.GetBindingSource();
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "当前时间：" + DateTime.Now.ToString("HH:mm:ss");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("表一历年水库来水量");
                comboBox1.Items.Add("表三灌溉面积统计表");
                comboBox1.Items.Add("表四灌溉制度表");
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("表二水库来水量预测表");
                comboBox1.Items.Add("表五农作物需水总量表");
                comboBox1.Items.Add("表六计算结果统计表");
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelmessage.Text = "保存修改内容";
            MyTools.SaveData(MySqlDataSet.GetSqlDataAdapter(),MySqlDataSet.GetDataTable());
            toolStripStatusLabelresult.Text = "保存修改成功";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Authority a = new Authority();
            a.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //AddData form = new AddData();
            //form.ShowDialog();
            toolStripStatusLabelmessage.Text = "添加一行数据";
            MySqlDataSet.GetBindingSource().AddNew();
            toolStripStatusLabelresult.Text = "添加数据成功";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelmessage.Text = "请选择一行数据";
            if (dataGridView1.SelectedRows.Count == 0)
            {

                MessageBox.Show("请选择一行数据");
                
            }
            else
            {
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(r);
                    }
                }
                toolStripStatusLabelresult.Text = "删除数据成功";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Query query = new Query();
            query.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyTools.calculateWaterNeed1();
            MyTools.calculateWaterNeed2();
            tabControl1.SelectedIndex = 1;
            comboBox1.SelectedIndex = 1;
            MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM      表五农作物需水总量表");
            this.dataGridView2.DataSource = MySqlDataSet.GetBindingSource();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //计算步骤
            MyTools.calculatePlan1();
            MyTools.calculatePlan2();
            MyTools.calculatePlan3();
            MyTools.calculatePlan4();
            MyTools.calculatePlan5();
            tabControl1.SelectedIndex = 1;
            comboBox1.SelectedIndex = 2;
            MySqlDataSet = MyTools.getSqlDataSet("SELECT  * FROM      表六计算结果统计表");
            this.dataGridView2.DataSource = MySqlDataSet.GetBindingSource();
        }
    }
}

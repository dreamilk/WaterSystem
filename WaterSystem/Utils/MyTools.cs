using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace WaterSystem
{
    class MyTools
    {
        //数据库连接字符串
        public static String connStr = Properties.Settings.Default.WaterConnectionString;

        //登陆判断是否是用户
        public static Boolean IsUser(String name, String password)
        {
            try {
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT   姓名, 密码 FROM      用户 WHERE   (姓名 = '" + name + "') AND (密码 = '" + password + "')", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                Boolean isOk = reader.HasRows;
                conn.Close();
                reader.Close();
                return isOk;
            } catch 
            {
                MessageBox.Show("数据库连接字符串错误,无法连接到数据库\n" +
                    "请检查数据库，或联系开发人员QQ:1763443263");
                return false;
            }
        }

        //得到 用户的权限
        public static int getPermission(String name, String password)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT   姓名, 权限 FROM      用户 WHERE   (姓名 = '" + name + "') ", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int permission = 0;
            while (reader.Read())
            {
                permission = reader.GetInt32(reader.GetOrdinal("权限"));
            }
            conn.Close();
            reader.Close();
            return permission;
        }

        //获取数据封装
        public static SqlDataSet getSqlDataSet(String sql)
        {
            DataTable table = new DataTable();
            BindingSource bs = new BindingSource();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connStr);
            adapter.Fill(table);
            bs.DataSource = table;
            return new SqlDataSet(adapter, table, bs);
        }

        //保存数据
        public static void SaveData(SqlDataAdapter adapter, DataTable table)
        {
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.Update(table);
        }

        //用户是否存在
        public static Boolean HaveUser(String name)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT   姓名, 权限 FROM      用户 WHERE   (姓名 = '" + name + "') ", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            return false;
        }

        //注册用户
        public static Boolean Register(String name, String password)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[用户] ([姓名], [密码], [权限]) VALUES (N'" + name + "', N'" + password + "', 0)", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            conn.Close();
            return true;
        }

        //获取用户列表
        public static List<Utils.User> GetUserList()
        {
            List<Utils.User> list = new List<Utils.User>();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT   姓名,密码,权限 FROM      用户 ", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                String name = reader.GetString(reader.GetOrdinal("姓名"));
                String password = reader.GetString(reader.GetOrdinal("密码"));
                int permission = reader.GetInt32(reader.GetOrdinal("权限"));
                list.Add(new Utils.User(name, password, permission));
            }
            return list;
        }

        //管理用户，删除用户
        public static Boolean delectUser(String name)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM 用户 WHERE 姓名 = '" + name + "' ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //管理用户，修改用户
        public static Boolean changeUser(String name, String password)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            //SqlCommand cmd = new SqlCommand("DELETE FROM 用户 WHERE 姓名 = '" + name + "' ", conn);
            SqlCommand cmd = new SqlCommand("UPDATE 用户 SET 密码 = '" + password + "' WHERE 姓名 = '" + name + "' ", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //查询数据1
        public static SqlDataSet queryData(String tablename, String s10, String s11)
        {
            DataTable table = new DataTable();
            BindingSource bs = new BindingSource();
            String sql;
            SqlDataAdapter adapter = null;
            try
            {
                sql = "select * from " + tablename + " where [" + s10 + "] = " + s11;
                adapter = new SqlDataAdapter(sql, connStr);
                adapter.Fill(table);
                bs.DataSource = table;
                return new SqlDataSet(adapter, table, bs);
            }
            catch (Exception e)
            {
                if (e is System.Data.SqlClient.SqlException)
                {
                    sql = "select * from " + tablename + " where [" + s10 + "] = '" + s11 + "'";
                    adapter = new SqlDataAdapter(sql, connStr);
                    adapter.Fill(table);
                    bs.DataSource = table;
                    return new SqlDataSet(adapter, table, bs);
                }
                return new SqlDataSet(adapter, table, bs);
            }
        }

        //查询数据2
        //数据类型 float string
        //         string float
        //         string string
        //         float float
        public static SqlDataSet queryData(String tablename, String s10, String s11, String s20, String s21)
        {
            DataTable table = new DataTable();
            BindingSource bs = new BindingSource();
            String sql;
            SqlDataAdapter adapter = null;
            try
            {
                // f f
                sql = "select * from " + tablename + " where [" + s10 + "] = " + s11 + " and [" + s20 + "] = " + s21;
                adapter = new SqlDataAdapter(sql, connStr);
                adapter.Fill(table);
                bs.DataSource = table;
            }
            catch
            {
                try
                {
                    // f s
                    sql = "select * from " + tablename + " where [" + s10 + "] = " + s11 + " and [" + s20 + "] = '" + s21+"'";
                    adapter = new SqlDataAdapter(sql, connStr);
                    adapter.Fill(table);
                    bs.DataSource = table;
                }
                catch
                {
                    try
                    {
                        // s s
                        sql = "select * from " + tablename + " where [" + s10 + "] = '" + s11 + "' and [" + s20 + "] = '" + s21+"'";
                        adapter = new SqlDataAdapter(sql, connStr);
                        adapter.Fill(table);
                        bs.DataSource = table;
                    }
                    catch
                    {
                        try
                        {
                            // s f
                            sql = "select * from " + tablename + " where [" + s10 + "] = '" + s11 + "' and [" + s20 + "] = " + s21;
                            adapter = new SqlDataAdapter(sql, connStr);
                            adapter.Fill(table);
                            bs.DataSource = table;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return new SqlDataSet(adapter, table, bs);

        }

        //主页面算法部分
        //计算轮次需水量1
        public static bool calculateWaterNeed1()
        {

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update 表五农作物需水总量表 set[本轮灌溉面积（亩）] = 干地 + 林草地 + 夏禾作物 + 秋禾作物 + 经济作物", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //主页面算法部分
        //计算轮次需水量2
        public static bool calculateWaterNeed2()
        {

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update 表五农作物需水总量表 set[作物净需水量（万立米）] = [本轮灌溉面积（亩）]*[单位面积灌溉水量（立米/亩）]/10000", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //主页面算法部分
        //计算调水计划1
        public static bool calculatePlan1()
        {
            String s= "update 表六计算结果统计表 set 表六计算结果统计表.[作物需水量净水量（万m3]= (select [作物净需水量（万立米）] from 表五农作物需水总量表 where 表五农作物需水总量表.灌水轮次=表六计算结果统计表.灌水轮次)";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //主页面算法部分
        //计算调水计划2
        public static bool calculatePlan2()
        {
            String s = "update 表六计算结果统计表 set 表六计算结果统计表.[作物需水量毛水量（万m3]=表六计算结果统计表.[作物需水量净水量（万m3]/0.63";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //主页面算法部分
        //计算调水计划3
        public static bool calculatePlan3()
        {
            String s = "update 表六计算结果统计表 set 表六计算结果统计表.[作物需水量渠首平均供水流量m3/s]=表六计算结果统计表.[作物需水量毛水量（万m3]/8.64/表六计算结果统计表.灌水天数";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //主页面算法部分
        //计算调水计划4
        public static bool calculatePlan4()
        {
            String s = "update 表六计算结果统计表 set 表六计算结果统计表.水库出库水量万m3=表六计算结果统计表.[作物需水量毛水量（万m3]/0.8";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //主页面算法部分
        //计算调水计划5
        public static bool calculatePlan5()
        {
            String s = "update 表六计算结果统计表 set 表六计算结果统计表.[水库出库平均流量m3/s]=表六计算结果统计表.[作物需水量渠首平均供水流量m3/s]/8.64/表六计算结果统计表.灌水天数";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }
    }
}

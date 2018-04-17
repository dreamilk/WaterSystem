using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WaterSystem
{
    public class SqlDataSet
    {
        public SqlDataSet(SqlDataAdapter adapter, DataTable table, BindingSource bindingSource)
        {
            this.adapter = adapter;
            this.table = table;
            this.bindingSource = bindingSource;
        }

        private SqlDataAdapter adapter;
        private DataTable table;
        private BindingSource bindingSource;

        public SqlDataAdapter GetSqlDataAdapter()
        {
            return adapter;
        }

        public DataTable GetDataTable()
        {
            return table;
        }

        public BindingSource GetBindingSource()
        {
            return bindingSource;
        }

    }
}

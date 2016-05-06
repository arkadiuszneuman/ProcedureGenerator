using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Linq;

namespace ProcedureGenerator
{
    public partial class MainFrm : DevExpress.XtraEditors.XtraForm
    {
        private List<string> vrcTables = new List<string>();
        private DateTime vrcDate = new DateTime(2015, 3, 24);
        private string vrcConnectionString;

        public MainFrm()
        {

            InitializeComponent();
            lookUpEdit1.Properties.DataSource = vrcTables;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                vrcTables.Clear();

                vrcConnectionString = uC_DatabaseSelector1.GetConnectionString();
                SqlConnection vrlConnection = new SqlConnection(vrcConnectionString);
                SqlCommand vrlCommand = new SqlCommand("sys.sp_tables");
                vrlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter vrlDocumentIdPar = new SqlParameter("@table_type", SqlDbType.VarChar);
                vrlDocumentIdPar.Direction = ParameterDirection.Input;
                vrlCommand.Parameters.Add(vrlDocumentIdPar);

                vrlCommand.Connection = vrlConnection;

                vrlConnection.Open();

                vrlDocumentIdPar.Value = "'table'";

                using (SqlDataReader vrlReader = vrlCommand.ExecuteReader())
                {
                    while (vrlReader.Read())
                    {
                        vrcTables.Add(vrlReader["TABLE_NAME"].ToString());
                    }
                    vrlReader.Close();
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void FillMemo()
        {
            var vrlColumns = GetColumns();
            this.frtxtCode.Text = CreateProcedure(vrlColumns.ToArray());
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            FillMemo();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMemo();
        }

        private string CreateProcedure(Cl_Column[] vrpColumns)
        {
            StringBuilder vrlString = new StringBuilder();

            if (vrpColumns.Length > 0 && !string.IsNullOrEmpty(lookUpEdit1.Text))
            {
                Cl_InsideCreator vrlCreator = new Cl_InsideCreator(uC_DatabaseSelector1.CurrentDatabaseName, this.lookUpEdit1.Text, this.frbitAlter.Checked);

                if (radioGroup1.SelectedIndex == 0)
                {
                    return vrlCreator.GetSelect(vrpColumns);
                }
                else if (radioGroup1.SelectedIndex == 1)
                {
                    return vrlCreator.GetUpdate(vrpColumns);
                }
                else if (radioGroup1.SelectedIndex == 2)
                {
                    return vrlCreator.GetSave(vrpColumns);
                }
                else if (radioGroup1.SelectedIndex == 3)
                {
                    return vrlCreator.GetDelete(vrpColumns[0]);
                }
            }

            return vrlString.ToString();
        }

        private List<Cl_Column> GetColumns()
        {
            var vrlColumns = new List<Cl_Column>();

            try
            {
                string vrlConnectionString = uC_DatabaseSelector1.GetConnectionString();
                SqlConnection vrlConnection = new SqlConnection(vrlConnectionString);
                vrlConnection.Open();
                SqlCommand thisCommand = vrlConnection.CreateCommand();
                thisCommand.CommandText = string.Format("SELECT OBJECT_SCHEMA_NAME(T.[object_id],DB_ID()) AS [Schema], " +
                    "T.[name] AS [table_name], AC.[name] AS [column_name], " +
                    "TY.[name] AS system_data_type, AC.[max_length], " +
                    "AC.[precision], AC.[scale], AC.[is_nullable], AC.[is_ansi_padded] " +
                    "FROM sys.[tables] AS T " +
                    "INNER JOIN sys.[all_columns] AC ON T.[object_id] = AC.[object_id] " +
                    "INNER JOIN sys.[types] TY ON AC.[system_type_id] = TY.[system_type_id] AND AC.[user_type_id] = TY.[user_type_id] " +
                    "WHERE T.[is_ms_shipped] = 0 AND T.[name] = '{0}' " +
                    "ORDER BY T.[name], AC.[column_id]", lookUpEdit1.Text);
                using (SqlDataReader vrlReader = thisCommand.ExecuteReader())
                {
                    while (vrlReader.Read())
                    {
                        Cl_Column vrlColumn = new Cl_Column()
                        {
                            Name = vrlReader["column_name"].ToString(),
                            DataType = vrlReader["system_data_type"].ToString(),
                            Lenght = Convert.ToInt32(vrlReader["max_length"]),
                            Precision = Convert.ToInt32(vrlReader["precision"]),
                            Scale = Convert.ToInt32(vrlReader["scale"]),
                        };

                        vrlColumns.Add(vrlColumn);
                    }
                    vrlReader.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return vrlColumns;
        }

        private void frbitAlter_CheckedChanged(object sender, EventArgs e)
        {
            FillMemo();
        }

        private void btnScript_Click(object sender, EventArgs e)
        {
            try
            {
                Cl_InsideCreator vrlCreator = new Cl_InsideCreator(uC_DatabaseSelector1.CurrentDatabaseName, this.lookUpEdit1.Text, this.frbitAlter.Checked);
                vrlCreator.ExecuteScript(vrcConnectionString, this.frtxtCode.Text);
            }
            catch (Exception vrlEx)
            {
                XtraMessageBox.Show(vrlEx.Message);
            }
        }

        private void btnAllScripts_Click(object sender, EventArgs e)
        {
            try
            {
                Cl_InsideCreator vrlCreator = new Cl_InsideCreator(uC_DatabaseSelector1.CurrentDatabaseName, this.lookUpEdit1.Text, this.frbitAlter.Checked);
                var vrlColumns = GetColumns().ToArray();

                vrlCreator.ExecuteScript(vrcConnectionString, vrlCreator.GetSelect(vrlColumns));
                vrlCreator.ExecuteScript(vrcConnectionString, vrlCreator.GetUpdate(vrlColumns));
                vrlCreator.ExecuteScript(vrcConnectionString, vrlCreator.GetSave(vrlColumns));
                vrlCreator.ExecuteScript(vrcConnectionString, vrlCreator.GetDelete(vrlColumns[0]));
            }
            catch (Exception vrlEx)
            {
                XtraMessageBox.Show(vrlEx.Message);
            }
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            uC_DatabaseSelector1.CurrentServerName = @".\sql2014";
            uC_DatabaseSelector1.IsSQLAuthentication = false;
        }
    }
}
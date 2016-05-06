using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProcedureGenerator
{
    public class Cl_InsideCreator
    {
        private const string cnsUserIdInsert = "_us_IdIns";
        private const string cnsUserNameInsert = "_UsNameIns";
        private const string cnsDateInsert = "_DateIns";
        private const string cnsUserIdUpdate = "_us_IdUpd";
        private const string cnsUserNameUpdate = "_UsNameUpd";
        private const string cnsDateUpdate = "_DateUpd";
        private const string cnsTimestamp = "_Timestamp";
        private string vrcTableName;
        private string vrcDatabaseName;
        private bool vrcIsAlter;

        public Cl_InsideCreator(string vrpDatabaseName, string vrpTableName, bool vrpIsAlter)
        {
            this.vrcTableName = vrpTableName;
            this.vrcDatabaseName = vrpDatabaseName;
            this.vrcIsAlter = vrpIsAlter;
        }

        private string GetProcedureString()
        {
            if (vrcIsAlter)
            {
                return "ALTER PROCEDURE [dbo].[{0}]\r\n";
            }
            else
            {
                return "CREATE PROCEDURE [dbo].[{0}]\r\n";
            }
        }

        private string GetHeader()
        {
            return "USE [" + vrcDatabaseName + "]\r\nGO\r\n\r\n";
        }

        public string GetSelect(Cl_Column[] vrpColumns)
        {
            StringBuilder vrlString = new StringBuilder(GetHeader());

            string vrlProcedureName = vrcTableName.ReplaceTablePrefix();

            vrlString.Append(string.Format(GetProcedureString(), vrlProcedureName + "Load"));
            vrlString.Append(GetColumnAsParameter(vrpColumns[0]) + "\r\n");
            vrlString.Append("AS\r\n");
            vrlString.Append("BEGIN\r\n\r\n");
            vrlString.Append("\tSET NOCOUNT ON;\r\n\r\n\t");
            vrlString.Append("SELECT\t");

            foreach (var vrlColumn in vrpColumns.Where(c => !c.Name.Contains(cnsTimestamp)))
            {
                vrlString.Append(string.Format("[{0}],\r\n\t\t", vrlColumn.Name));
            }

            vrlString.Remove(vrlString.Length - 5, 5);
            vrlString.Append("\r\n\t");
            vrlString.Append(string.Format("FROM [{0}]\r\n\t", vrcTableName));
            vrlString.Append(string.Format("WHERE ([{0}] = @{0} OR @{0} = 0)\r\n", vrpColumns[0].Name));
            vrlString.Append("END");

            return vrlString.ToString();
        }

        public string GetSave(Cl_Column[] vrpColumns)
        {
            StringBuilder vrlString = new StringBuilder(GetHeader());
            string vrlProcedureName = vrcTableName.ReplaceTablePrefix();
            var vrlColumns = vrpColumns.Where(c => !c.Name.EndsWith(cnsUserIdUpdate) && !c.Name.EndsWith(cnsUserNameUpdate) && !c.Name.EndsWith(cnsDateUpdate) && c != vrpColumns[0]);

            vrlString.Append(string.Format(GetProcedureString(), vrlProcedureName + "Save"));
            vrlString.Append(String.Join(",\r\n", vrlColumns.Where(c => !c.Name.EndsWith(cnsTimestamp) && !c.Name.EndsWith(cnsDateInsert))
                .Select(c => GetColumnAsParameter(c)).ToArray()));

            vrlString.Append("\r\nAS\r\n");
            vrlString.Append("BEGIN\r\n\r\n");
            vrlString.Append("\tSET NOCOUNT ON;\r\n\r\n");
            vrlString.Append("\tDECLARE @date datetime\r\n");
            vrlString.Append("\tSET @date = GETDATE()\r\n");
            vrlString.Append(string.Format("\tINSERT INTO [{0}]\r\n\t\t(", vrcTableName));

            vrlString.Append(String.Join(",\r\n\t\t", vrlColumns.Select(c => "[" + c.Name + "]").ToArray()));

            vrlString.Append(")\r\n\t");

            vrlString.Append("VALUES\r\n\t(");

            foreach (var vrlColumn in vrlColumns)
            {
                if (vrlColumn != vrpColumns[0])
                {
                    if (vrlColumn.Name.EndsWith(cnsTimestamp) || vrlColumn.Name.EndsWith(cnsDateInsert))
                    {
                        vrlString.Append(string.Format("@date,\r\n\t\t"));
                    }
                    else
                    {
                        vrlString.Append(string.Format("@{0},\r\n\t\t", vrlColumn.Name));
                    }
                }
            }

            vrlString.Remove(vrlString.Length - 5, 5);
            vrlString.Append(")\r\n\r\n\t");
            vrlString.Append(string.Format("SELECT SCOPE_IDENTITY() as ID, @date as DateInsert\r\n"));

            vrlString.Append("END");

            return vrlString.ToString();
        }

        public string GetUpdate(Cl_Column[] vrpColumns)
        {
            StringBuilder vrlString = new StringBuilder(GetHeader());

            var vrlColumns = vrpColumns.Where(c => !c.Name.EndsWith(cnsUserIdInsert) && !c.Name.EndsWith(cnsUserNameInsert) && !c.Name.EndsWith(cnsDateInsert));

            string vrlProcedureName = vrcTableName.ReplaceTablePrefix();

            vrlString.Append(string.Format(GetProcedureString(), vrlProcedureName + "Update"));
            vrlString.Append(String.Join(",\r\n", vrlColumns.Where(c => !c.Name.EndsWith(cnsTimestamp) && !c.Name.EndsWith(cnsDateUpdate))
                .Select(c => GetColumnAsParameter(c)).ToArray()));

            vrlString.Append("\r\nAS\r\n");
            vrlString.Append("BEGIN\r\n\r\n");
            vrlString.Append("\tSET NOCOUNT ON;\r\n\r\n");
            vrlString.Append("\tDECLARE @date datetime\r\n");
            vrlString.Append("\tSET @date = GETDATE()\r\n");

            vrlString.Append(string.Format("\t\tUPDATE\t{0}\r\n\t\t", vrcTableName));
            vrlString.Append("SET\t");

            foreach (var vrlColumn in vrlColumns)
            {
                if (vrlColumn != vrpColumns[0])
                {
                    if (vrlColumn.Name.EndsWith(cnsTimestamp) || vrlColumn.Name.EndsWith(cnsDateUpdate))
                    {
                        vrlString.Append(string.Format("[{0}] = @date,\r\n\t\t\t", vrlColumn.Name));
                    }
                    else
                    {
                        vrlString.Append(string.Format("[{0}] = @{0},\r\n\t\t\t", vrlColumn.Name));
                    }
                }
            }

            vrlString.Remove(vrlString.Length - 6, 6);
            vrlString.Append(string.Format("\r\n\t\tWHERE ([{0}] = @{0})\r\n\t", vrpColumns[0].Name));

            vrlString.Append(string.Format("SELECT @date as DateUpdate\r\n"));
            vrlString.Append("END");

            return vrlString.ToString();
        }

        public string GetDelete(Cl_Column vrpIdColumn)
        {
            StringBuilder vrlString = new StringBuilder(GetHeader());

            string vrlProcedureName = vrcTableName.ReplaceTablePrefix();

            vrlString.Append(string.Format(GetProcedureString(), vrlProcedureName + "Delete"));
            vrlString.Append(GetColumnAsParameter(vrpIdColumn));

            vrlString.Append("\r\nAS\r\n");
            vrlString.Append("BEGIN\r\n\r\n");
            vrlString.Append("\tSET NOCOUNT ON;\r\n\r\n");

            vrlString.Append(string.Format("\tDELETE FROM\t{0}\r\n\t", vrcTableName));
            vrlString.Append(string.Format("WHERE\t({0} = @{0})\r\n", vrpIdColumn.Name));

            vrlString.Append("END");

            return vrlString.ToString();
        }

        public void ExecuteScript(string vrpConnectionString, string vrpScript)
        {
            if (!String.IsNullOrEmpty(vrpScript))
            {
                if (vrpScript.StartsWith("USE"))
                {
                    vrpScript = RemoveFirstLines(vrpScript, 3);
                }

                using (SqlConnection vrlConnection = new SqlConnection(vrpConnectionString))
                {
                    using (SqlCommand vrlCommand = vrlConnection.CreateCommand())
                    {
                        vrlCommand.CommandText = vrpScript;

                        vrlConnection.Open();
                        vrlCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private string RemoveFirstLines(string text, int linesCount)
        {
            var lines = Regex.Split(text, "\r\n|\r|\n").Skip(linesCount);
            return string.Join(Environment.NewLine, lines.ToArray());
        }

        private string GetColumnAsParameter(Cl_Column vrlColumn)
        {
            if (vrlColumn.DataType == "decimal")
            {
                return string.Format("@{0} {1}({2},{3})", vrlColumn.Name, vrlColumn.DataType, vrlColumn.Precision, vrlColumn.Scale);
            }
            else if (vrlColumn.DataType == "varchar" || vrlColumn.DataType == "nvarchar" || vrlColumn.DataType == "char")
            {
                if (vrlColumn.Lenght == -1)
                {
                    return string.Format("@{0} {1}(MAX)", vrlColumn.Name, vrlColumn.DataType);
                }
                else
                {
                    return string.Format("@{0} {1}({2})", vrlColumn.Name, vrlColumn.DataType, vrlColumn.Lenght);
                }
            }
            else
            {
                return string.Format("@{0} {1}", vrlColumn.Name, vrlColumn.DataType);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcedureGenerator
{
    public static class Cl_Extensions
    {
        public static string ReplaceTablePrefix(this string vrpString)
        {
            return vrpString.Replace("tb_", "sp_").Replace("zzt_", "zzs_");
        }
    }
}

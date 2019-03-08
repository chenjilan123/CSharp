using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CSharp
{
    public static class DataTableExtension
    {
        public static IEnumerable<DataRow> AsEnumerable(this DataTable table)
        {
            foreach (DataRow dr in table.Rows)
            {
                yield return dr;
            }
        }
    }
}

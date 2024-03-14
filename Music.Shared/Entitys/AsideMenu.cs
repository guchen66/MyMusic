using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Entitys
{
    public class AsideMenu
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(16)", IsNullable = true)]
        public string? Icon { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(16)", IsNullable = true)]
        public string? Content { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(16)", IsNullable = true)]
        public string? NameSpace { get; set; }
    }
}

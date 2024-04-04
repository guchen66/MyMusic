
namespace Music.Shared.Entitys
{
    public class AsideMenu: AutoIncrementEntity
    {
        [SugarColumn(ColumnDataType = "Nvarchar(16)", IsNullable = true)]
        public string? Icon { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(16)", IsNullable = true)]
        public string? Content { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(16)", IsNullable = true)]
        public string? NameSpace { get; set; }
    }

    public enum MenuEnum
    { 
        
    }
}

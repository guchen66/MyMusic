using Music.Shared.Entitys.NotMapped;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Entitys
{
    [SugarTable("LoginInputInfo")]
    public class LoginInputInfo //: AutoIncrementEntity
    {
        [SugarColumn(IsNullable = true, Length = 200)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime LoginTime { get; set; }
    }
}
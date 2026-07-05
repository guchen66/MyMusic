using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Dtos
{
    /// <summary>
    /// 注册参数
    /// </summary>
    public class SignUpDto
    {
        public string Id { get; set; }

        public string Avator { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string VerificationCode { get; set; }
    }
}
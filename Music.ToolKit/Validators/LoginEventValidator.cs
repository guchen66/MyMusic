using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.ToolKit.Validators
{
    public class LoginEventValidator : AbstractValidator<string>
    {
        public LoginEventValidator()
        {
            RuleFor(x => x).NotEmpty();
            RuleFor(x => x).Length(4, 10);
        }

        /*public LoginEventValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("请输入用户名");
            RuleFor(x => x.Password).NotEmpty().WithMessage("请输入密码");
            RuleFor(x => x.Password).Length(4, 10).WithMessage("密码长度必须为4到10个字符之间");
        }*/
    }

}

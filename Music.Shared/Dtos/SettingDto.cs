using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Dtos
{
    public class SettingDto: BaseDto
    {
		private string _setTitle;

		public string SetTitle
		{
			get => _setTitle;
			set => SetProperty(ref _setTitle, value);
		}

	}
}

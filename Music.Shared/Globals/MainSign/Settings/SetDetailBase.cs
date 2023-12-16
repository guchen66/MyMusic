using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Globals.MainSign.Settings
{
    public class SetDetailBase : BindableBase
    {
        public virtual string Type { get; set; }

        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private string _textContent;
        public string TextContent
        {
            get { return _textContent; }
            set { SetProperty(ref _textContent, value); }
        }


    }
}

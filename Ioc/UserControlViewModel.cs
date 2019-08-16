using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class UserControlViewModel : INotifyPropertyChanged
    {
        public UserControlViewModel(My my)
        {
            this.My = my;
        }

        /// <summary>
        /// Constructor for xaml design time.
        /// </summary>
        public UserControlViewModel()
        {

        }

        public readonly My My;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        protected string text = "HelloViewModel";

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    /// <summary>
    /// StructureMap view model to xaml view registration.
    /// </summary>
    public class RegisterViewModelToViewUserControlMy : RegisterViewModelToView<UserControlViewModel, UserControlMy> { }
}

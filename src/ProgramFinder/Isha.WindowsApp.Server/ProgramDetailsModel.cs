using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Isha.WindowsApp.ViewModel
{
    public class ProgramDetailModel : INotifyPropertyChanged
    {
        private string _programid;
                
        private string _icon;      
        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                if (value != _icon)
                {
                    _icon = value;
                    NotifyPropertyChanged("Icon");
                }
            }
        }

        private string _title;      
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string _date;        
        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (value != _address)
                {
                    _address = value;
                    NotifyPropertyChanged("Address");
                }
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }

        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    NotifyPropertyChanged("Language");
                }
            }
        }

        private string _eflyer_url;
        public string EflyerUrl
        {
            get
            {
                return _eflyer_url;
            }
            set
            {
                if (value != _eflyer_url)
                {
                    _eflyer_url = value;
                    NotifyPropertyChanged("EflyerUrl");
                }
            }
        }

        private string _contact_phone;
        public string ContactPhone
        {
            get
            {
                return _contact_phone;
            }
            set
            {
                if (value != _contact_phone)
                {
                    _contact_phone = value;
                    NotifyPropertyChanged("ContactPhone");
                }
            }
        }

        private string _contact_email;
        public string ContactEmail
        {
            get
            {
                return _contact_email;
            }
            set
            {
                if (value != _contact_email)
                {
                    _contact_email = value;
                    NotifyPropertyChanged("ContactEmail");
                }
            }
        }

        private string _register_url;
        public string RegisterUrl
        {
            get
            {
                return _register_url;
            }
            set
            {
                if (value != _register_url)
                {
                    _register_url = value;
                    NotifyPropertyChanged("RegisterUrl");
                }
            }
        }

        private string _preregister_url;
        public string PreRegisterUrl
        {
            get
            {
                return _preregister_url;
            }
            set
            {
                if (value != _preregister_url)
                {
                    _preregister_url = value;
                    NotifyPropertyChanged("PreRegisterUrl");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
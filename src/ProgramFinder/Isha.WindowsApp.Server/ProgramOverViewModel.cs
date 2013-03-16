using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Isha.WindowsApp.Server;

namespace Isha.WindowsApp.ViewModel
{
    public class ProgramOverviewModel : INotifyPropertyChanged
    {
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

        private string _location;        
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (value != _location)
                {
                    _location = value;
                    NotifyPropertyChanged("Location");
                }
            }
        }


        private string _title_ext;
        public string Title_ext
        {
            get
            {
                return _title_ext;
            }
            set
            {
                if (value != _title_ext)
                {
                    _title_ext = value;
                    NotifyPropertyChanged("Title_ext");
                }
            }
        }

        private string _program_url;
        public string ProgramUrl
        {
            get
            {
                return _program_url;
            }
            set
            {
                if (value != _program_url)
                {
                    _program_url = value;
                    NotifyPropertyChanged("ProgramUrl");
                }
            }
        }

        private string _program_id;
        public string ProgramId
        {
            get
            {
                return _program_id;
            }
            set
            {
                if (value != _program_id)
                {
                    _program_id = value;
                    NotifyPropertyChanged("ProgramId");
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
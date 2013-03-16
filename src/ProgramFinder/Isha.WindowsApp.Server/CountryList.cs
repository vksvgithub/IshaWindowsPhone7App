using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Isha.WindowsApp.Server
{
    public class LocationModel : INotifyPropertyChanged
    {
        private List<string> _selectedCountryCities = new List<string>();

        public List<string> SelectedCountryCities
        {
            get { return _selectedCountryCities; }
            set
            {
                if (value != _selectedCountryCities)
                {
                    _selectedCountryCities = value;
                    NotifyPropertyChanged("SelectedCountryCities");
                }
            }
        }
        private string selectedContry = string.Empty;

        public string SelectedContry
        {
            get { return selectedContry; }
            private set { selectedContry = value; }
        }

        static LocationModel()
        {
            _countries.Add("USA");
            _countries.Add("India");
            _countries.Add("USA");
            _countries.Add("UK");
            _countries.Add("Singapore");
            _countries.Add("Lebanon");
            _countries.Add("Canada");
            _countries.Add("Other Countries");
        }

        public LocationModel()
        {
        }

        public void LoadCities(string country)
        {
            selectedContry = country;
            
            Task<List<string>> cities = JSONFeeds.GetJSONDataAsync<List<string>>("http://www.ishafoundation.org/index.php?option=com_program&task=citylist&format=json");
            List<string> finalcities = cities.Result.Distinct().ToList();

            if (finalcities.Count != 0)
            {
                _selectedCountryCities = finalcities;
            }
        }

        private static List<string> _countries = new List<string>();
        public static List<string> Countries
        {
            get
            {
                return _countries;
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

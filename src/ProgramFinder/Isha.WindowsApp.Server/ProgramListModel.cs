using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Isha.WindowsApp.Server;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Isha.WindowsApp.ViewModel
{
    public class ProgramListModel : INotifyPropertyChanged
    {
        public ProgramListModel()
        {
            this.Items = new ObservableCollection<ProgramOverviewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ProgramOverviewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async void LoadData(string ProgramSearchURL)
        {
            try
            {
                // Sample data; replace with real data
                //this.Items.Add(new ItemViewModel() { LineOne = "runtime one", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
                //this.Items.Add(new ItemViewModel() { LineOne = "runtime two", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });

                Task<ProgramList> searchresult = JSONFeeds.GetProgramListAsync(ProgramSearchURL);
                await searchresult;

                if (!searchresult.IsCompleted)
                {
                    this.IsDataLoaded = false;
                    return;
                }

                ProgramList list = searchresult.Result;
                foreach (Program p in list.results)
                {
                    this.Items.Add(new ProgramOverviewModel() { Title = p.overview.title, Date = p.overview.date, Location = p.overview.location, ProgramId = p.overview.program_id, ProgramUrl = p.overview.program_url, Title_ext = p.overview.title_ext });
                }

                this.IsDataLoaded = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());                
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
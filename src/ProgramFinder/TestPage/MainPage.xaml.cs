using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestPage.Resources;
using Isha.WindowsApp.Server;
using System.Threading.Tasks;
using System.Text;

namespace TestPage
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {   
            ////get program details

            //Task<ProgramDetail[]> pdDetail = JSONFeeds.GetJSONDataAsync<ProgramDetail[]>("http://www.ishafoundation.org/index.php?option=com_program&task=details&program_id=6078&format=json");
            //ProgramDetail[] pd = pdDetail.Result;
            //TextBlock1.Text.Insert(1, pd[0].text);
            

            ////////test list of cities
            //Task<List<string>> cities = JSONFeeds.GetJSONDataAsync<List<string>>("http://www.ishafoundation.org/index.php?option=com_program&task=citylist&format=json");                        
            //List<string> finalcities = cities.Result.Distinct().ToList();
            //TextBlock1.Text.Insert(2,finalcities[0]);
            



            ////get list of programs
            Task<ProgramList> programlist = JSONFeeds.GetProgramListAsync(TextBox1.Text.TrimEnd());

            await programlist;
            ProgramList plist = programlist.Result;

            StringBuilder listofitems = new StringBuilder();
            for (int i = 3, j = 0; i < (plist.resultcount + 3); i++, j++)
            {
                listofitems.Append(plist.results[j].overview.icon);
                listofitems.Append(Environment.NewLine);
            }

            TextBlock1.Text = listofitems.ToString();
            
        }

        private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //if (!String.IsNullOrEmpty(e.Result))
            //{
            //    //System.Runtime.Serialization.Json.DataContractJsonSerializer serialier = new DataContractJsonSerializer(typeof(Program));         
            //    TextBox2.Text = e.Result;
            //}
        }


        // Event handler for updating visual progress indicator
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calculate the downloaded percentage.
            // Update the Rectangle and TextBlock objects of the visual progress indicator.
            //TextBox2.Text = e.ProgressPercentage.ToString();            
        }

        private void TextBox1_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        
    }
}
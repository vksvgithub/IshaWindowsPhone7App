using IshaHome.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Isha.WindowsApp.Server;
using Isha.WindowsApp.ViewModel;
using System.Threading.Tasks;

// The Grid App template is documented at http://go.microsoft.com/fwlink/?LinkId=234226

namespace IshaHome
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

        internal static string ProgramSearchLocationURL = "http://www.ishafoundation.org/index.php?option=com_program&task=citylist&format=json";
        internal static string ProgramSearchURL = "http://www.ishafoundation.org/index.php?option=com_program&Itemid=250&Submit=Filter&cat=0&country=0&date=0&end=&event=0&is_sadhguru=1&lang=en&program=6&rejuvenation=0&start=&task=filter&format=json";
        internal static string ProgramDetailsURL = "http://www.ishafoundation.org/index.php?option=com_program&task=details&format=json";


        private static ProgramListModel requestedProgramList = null;

        private static ProgramDetailModel selectedProgram = new ProgramDetailModel();

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The ProgramListModel object.</returns>
        public static ProgramListModel RequestedProgramList
        {
            get
            {
                // Delay creation of the view model until necessary
                if (requestedProgramList == null)
                    requestedProgramList = new ProgramListModel();

                return requestedProgramList;
            }
        }


        public static ProgramDetailModel SelectedProgramDetails
        {
            get
            {
                if (selectedProgram == null)
                {
                    selectedProgram = new ProgramDetailModel();
                }

                return selectedProgram;
            }
        }

        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            ResourceDictionary dic = Resources;
            // Ensure that application state is restored appropriately
            if (!App.RequestedProgramList.IsDataLoaded)
            {
                App.RequestedProgramList.LoadData(ProgramSearchURL);
            }

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(ProgramSearchResult), "AllGroups"))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public static async void LoadProgramDetails(string programid)
        {
            //if (!String.IsNullOrEmpty(_programid) && String.Compare(_programid, programid) == 0)
            //{                
            //    return;
            //}
            //TODO: build a caching and not to call network if already loaded, also notify the View


            string urltocall = ProgramDetailsURL + "&program_id=" + programid;

            Task<ProgramDetail[]> searchresult = JSONFeeds.GetJSONDataAsync<ProgramDetail[]>(urltocall);
            await searchresult;

            if (!searchresult.IsCompleted)
            {
                return;
            }

            ProgramDetail[] details = searchresult.Result;

            if (details.Length != 0)
            {
                ProgramDetail detail = details[0];

                selectedProgram.Address = detail.address;
                selectedProgram.ContactEmail = detail.contact_email;
                selectedProgram.ContactPhone = detail.contact_phone;
                selectedProgram.Date = detail.date;
                selectedProgram.EflyerUrl = detail.eflyer_url;
                selectedProgram.Icon = detail.icon;
                selectedProgram.Language = detail.title_ext;
                selectedProgram.PreRegisterUrl = detail.preregister_url;
                selectedProgram.RegisterUrl = detail.register_url;
                selectedProgram.Text = detail.text;
                selectedProgram.Title = detail.title;
            }
        }
    }
}

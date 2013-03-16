using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Isha.WindowsApp.Server
{    
    public class ProgramOverview
    {

        //  "icon":"http:\/\/www.ishafoundation.org\/components\/com_program\/images\/icon\/specialevent.jpg",
        //"title":"Suryakund Consecration",
        //"title_ext":"",
        //"date":"21 - 22 Dec 2012",
        //"location":"Isha Yoga Center, Coimbatore, India",
        //"program_url":"http:\/\/www.ishayoga.org\/index.php?option=com_program&task=details&program_id=6339",
        //"program_id":"6339"
                
        public string icon {get; set;}

        public string title { get; set; }

        public string title_ext { get; set; }

        public string date { get; set; }

        public string location { get; set; }

        public string program_url { get; set; }

        public string program_id { get; set; }      
    } 


    public class Program
    {
        public int overallIndex { get; set; }
        public ProgramOverview overview { get; set; }

        public Program()
        {
            overview = new ProgramOverview();
        }
    }


    public class ProgramDetail
    {                
        public string icon {get; set;}

        public string title { get; set; }

        public string title_ext { get; set; }

        public string date { get; set; }

        public string text { get; set; }

        public string address { get; set; }

        public string eflyer_url { get; set; }

        public string contact_phone { get; set; }

        public string contact_email { get; set; }

        public string register_url { get; set; }

        public string preregister_url { get; set; }        

      //   "icon":"http:\/\/www.ishafoundation.org\/components\/com_program\/images\/icon\/iy.jpg",
      //"title":"Sadhguruvudan Isha Yoga",
      //"title_ext":"in Tamil",
      //"date":"22 - 24 Jun 2012",
      //"text":"Registrations ClosedThis program is conducted by Sadhguru.\n",
      //"address":"Tagore Art College ground,\nPondicherry, India",
      //"eflyer_url":"http:\/\/www.ishayoga.org\/programs\/2012\/IE-Sadhguru-Pondicherry-22Jun2012.isa",
      //"contact_phone":"83000 16000",
      //"contact_email":"pondicherry@ishayoga.org",
      //"register_url":"",
      //"preregister_url":""

    }      
}


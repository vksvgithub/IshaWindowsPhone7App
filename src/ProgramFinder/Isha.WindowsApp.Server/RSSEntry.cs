using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isha.WindowsApp.Server
{
    /// <summary>
    /// Class that holds details from a single RSS entry.
    /// </summary>
    public class RSSEntry
    {
        private readonly string description;
        private readonly string link;
        private readonly string title;
        private readonly DateTime pubDateUtc;

        /// <summary>
        /// Creates an instance of the RSSEntry class.
        /// </summary>
        /// <param name="title">The title of the article</param>
        /// <param name="description">Description of the article</param>
        /// <param name="link">Hyperlink Url for the article</param>
        /// <param name="pubDateUtc">Publication date of the blog entry this RSS item represents, in UTC</param>
        public RSSEntry(string title, string description, string link, DateTime pubDateUtc)
        {
            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            if (link == null)
            {
                throw new ArgumentNullException("link");
            }

            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            this.title = title;
            this.link = link;
            this.description = description;
            this.pubDateUtc = pubDateUtc;
        }

        /// <summary>
        /// Gets the description of this RSS entry.
        /// </summary>
        public string Description
        {
            get { return this.description; }
        }

        /// <summary>
        /// Gets the Url for this RSS entry.
        /// </summary>
        public string Link
        {
            get { return this.link; }
        }

        /// <summary>
        /// Gets the title for this RSS entry.
        /// </summary>
        public string Title
        {
            get { return this.title; }
        }
    }
}


// portions of rss code sample by Steve Lautenschlager at CambiaResearch.com

namespace MyPad
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Text;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.ServiceModel.Syndication;

    partial class EntriesListDialog : Form
    {
        public EntriesListDialog ()
        {
            InitializeComponent();
            try
            {
            FillItems ();
            }
            catch (Exception ex)
            {
                Trace.WriteLine (ex.ToString ());
                // supress
            }
        }

        void CreateNewEntry_Click (object sender, EventArgs e)
        {
            var dlg = new EntryCreateOrEditDialog ();
            if (dlg.ShowDialog () == DialogResult.OK)
            {
                // create a new entry
                // create new entry for feed
                SyndicationItem item = new SyndicationItem();

                var urlOfArticle = new Uri(dlg.URL);
                // set the entry id to the URL for the item
                string url = urlOfArticle.AbsoluteUri;  // TODO: create the GetArticleUrl method
                item.Id = url;

                // Add the URL for the item as a link
                var link = new SyndicationLink(urlOfArticle);
                item.Links.Add(link);

                // Fill some properties for the item
                item.Title = new TextSyndicationContent(dlg.TITLE);
                item.Summary = new TextSyndicationContent(dlg.MSG);
                var dateTime = DateTime.Now;
                item.LastUpdatedTime = dateTime;
                item.PublishDate = dateTime;
            }
        }

        void EditExistingEntry_Click (object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            var dlg = new EntryCreateOrEditDialog ();
            // prepare dialog fields
            var item = listView1.SelectedItems[0];
            var entry = (SyndicationItem)item.Tag;
            dlg.URL = entry.Id;
            dlg.TITLE = entry.Title.Text;
            dlg.MSG = entry.Summary.Text;
            if (dlg.ShowDialog () == DialogResult.OK)
            {
                // change selected entry
                entry.BaseUri = new Uri(dlg.URL);
                entry.Content = new TextSyndicationContent(dlg.MSG);
            }
        }

        void DeleteEntry_Click (object sender, EventArgs e)
        {
            var itemsToDelete = new  List<ListViewItem> ();
            // message box
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    itemsToDelete.Add (item);
                }
            }
            foreach (var item in itemsToDelete)
            {
                listView1.Items.Remove (item);
            }
        }
        readonly string baseUrl = "http://vsyachepuz.github.io/blog";
        void FillItems()
        {
            var cfg = Globals.LoadConfiguration ();
            string mainFile = cfg.AppSettings.Settings ["AtomFeedLocation"].Value;

            SyndicationFeed feed;
            try
            {
                    using (var reader = XmlReader.Create (mainFile))
                    {
                        feed = SyndicationFeed.Load (reader);
                        reader.Close ();
                    }
            }
            catch (Exception ex)
            {
                var msg = string.Format ("Error, while reading ATOM-file {0}", mainFile);
                throw new ApplicationException (msg, ex);
                // ReadElementContentAs() methods cannot be called on an element that has child elements.
                // Line 19, position 40.
/*
                The summary xml-node contains nested brackets.
                So, there should be another parser, which can process this.
                SyndicationFeed's parser is from "System.ServiceModel.Syndication"
                
                <summary type="xhtml" xml:lang="ru"><div xmlns="http://www.w3.org/1999/xhtml">
                        <svg height="100" width="100" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
                                <a xlink:href="http://www.w3schools.com/svg/" target="_blank">
                                  <circle cx="50" cy="50" r="40" stroke="black" stroke-width="3" fill="yellow" />
                                  <circle cx="50" cy="50" r="14" stroke="black" stroke-width="3" fill="magenta" />
                                </a>
                                svg-картинки отключены. В Thunderbird выбрать View -&gt; Feed message body as -&gt; Original HTML.
                        </svg>
                </div></summary>
*/
            }

            int index = 1;
            foreach (SyndicationItem item in feed.Items)
            {
                var it = new ListViewItem ();
                it.Tag = item;
                it.Text = index.ToString (); index++;

                var published = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                published.Text = item.PublishDate.ToString();
                it.SubItems.Add (published);

                var updated = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                updated.Text = item.LastUpdatedTime.ToString();
                it.SubItems.Add (updated);

                var title = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                title.Text = item.Title.Text;
                it.SubItems.Add(title);

                var url = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                url.Text = item.Id;
                it.SubItems.Add (url);

                listView1.Items.Add (it);
            }
            if (listView1.Items.Count > 0)
            {
                listView1.Items [0].Selected = true;
            }
        }
        SyndicationFeed GetFeed()
        {
            #region -- Create Feed --
            SyndicationFeed feed = new SyndicationFeed();

            // set the feed ID to the main URL of your Website
            feed.Id = baseUrl;

            // set some properties on the feed
            feed.Title = new TextSyndicationContent("Блог VsyachePuz-а");
            /*feed.Description = new TextSyndicationContent("описание первого фида");*/
            //feed.Copyright = new TextSyndicationContent("Steve");
            feed.LastUpdatedTime = new DateTimeOffset(DateTime.Now);

            // since you are writing your own custom feed generator, you get to
            // name it!  Although this is not required.
            feed.Generator = "Руки";

            /*
            // add your site or feed logo url
            string imageUrl = baseUrl + "/images/mylogo.gif";
            feed.ImageUrl = new Uri(imageUrl);
            */

            // Add the URL that will link to your published feed when it's done
            SyndicationLink link = new SyndicationLink(new Uri("https://raw.githubusercontent.com/VsyachePuz/blog/gh-pages/notifications/for-irc.atom"));
            link.RelationshipType = "self";
            link.MediaType = "text/html";
            link.Title = "VsyachePuz's blog feed";
            feed.Links.Add(link);

            // Add your site link
            link = new SyndicationLink(new Uri(baseUrl));
            link.MediaType = "text/html";
            link.Title = "Site link";
            feed.Links.Add(link);
            #endregion

            List<SyndicationItem> items = new List<SyndicationItem>();

            int maxArticles = 15;
            for (int i=0; i < listView1.Items.Count; i++)
            {
                var it = listView1.Items [i];
                Uri urlOfArticle = new Uri(GetColumnValue (it, "URL"));
                string titleOfArticle = GetColumnValue(it, "Title");
                DateTime published = DateTime.Parse(GetColumnValue(it, "Published"));
                DateTime updated = DateTime.Parse(GetColumnValue(it, "Updated"));

                // create new entry for feed
                SyndicationItem item = new SyndicationItem();

                // set the entry id to the URL for the item
                string url = urlOfArticle.AbsoluteUri;  // TODO: create the GetArticleUrl method
                item.Id = url;

                // Add the URL for the item as a link
                link = new SyndicationLink(urlOfArticle);
                item.Links.Add(link);

                // Fill some properties for the item
                item.Title = new TextSyndicationContent(titleOfArticle);
                item.LastUpdatedTime = published;
                item.PublishDate = updated;

                /*
                // Fill the item content            
                string html = String.Empty; // TODO: create the GetFeedEntryHtml method
                TextSyndicationContent content 
                = new TextSyndicationContent(html, TextSyndicationContentKind.Html);
                item.Content = content;
                */

                // Finally add this item to the item collection
                items.Add(item);

                // Stop after adding the max desired number of items
                if (items.Count >= maxArticles)
                {
                    break;
                }
            }

            feed.Items = items;
            return feed;
        }
        string GetColumnValue(ListViewItem item, string columnTag)
        {
            ListView lv = item.ListView;
            int index = 0;
            foreach (ColumnHeader col in lv.Columns)
            {
                if (string.Compare (col.Tag as String, columnTag) == 0)
                {
                    return item.SubItems [index].Text;
                }
                index ++;
            }
            return String.Empty;
        }
        void SaveButton_Click (object sender, EventArgs e)
        {
            try
            {
                var cfg = Globals.LoadConfiguration ();
                string mainFile = cfg.AppSettings.Settings ["AtomFeedLocation"].Value;

                var feed = GetFeed ();
                Globals.EnsureDirectoryExists(mainFile);
                using (FileStream fs = new FileStream (mainFile, FileMode.Create))
            {
                    using (StreamWriter w = new StreamWriter (fs, Encoding.UTF8))
                {
                        XmlWriterSettings xs = new XmlWriterSettings ();
                        xs.Indent = true;
                        using (XmlWriter xw = XmlWriter.Create (w, xs))
                    {
                            xw.WriteStartDocument ();
                            Atom10FeedFormatter formatter = new Atom10FeedFormatter (feed);
                            //Rss20FeedFormatter formatter = new Rss20FeedFormatter(feed);
                            formatter.WriteTo (xw);
                            xw.Close ();
                    }
                }
            }
                this.DialogResult = DialogResult.OK;
            } catch (Exception ex)
            {
                Trace.WriteLine (ex.ToString ());
            }
        }
    }
}

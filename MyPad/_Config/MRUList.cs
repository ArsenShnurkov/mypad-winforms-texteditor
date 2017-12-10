using System;
using System.Configuration;
// https://stackoverflow.com/questions/2718095/custom-app-config-section-with-a-simple-list-of-add-elements
/*
<configuration>
    <configSections>
        <section name="MRUList" type="MyPad.MRUListConfigurationSection, MyPad" />
    </configSections>
    <MRUList>
        <add filename="/path1/filename1"/>
        <add filename="/path2/filename2"/>
    </MRUList>
</configuration>
*/
namespace MyPad
{
    public class MRUListConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty ("", IsRequired = true, IsDefaultCollection = true)]
        public MRUListInstanceCollection Instances {
            get { return (MRUListInstanceCollection)this [""]; }
            set { this [""] = value; }
        }
        public bool Contains (string filename)
        {
            MRUListInstanceCollection collection = (MRUListInstanceCollection)this [""];
            return collection.Contains (filename);
        }
        public void RemoveAt (int index)
        {
            MRUListInstanceCollection collection = (MRUListInstanceCollection)this [""];
            collection.RemoveAt (index);
        }
        public void Remove (string filename)
        {
            MRUListInstanceCollection collection = (MRUListInstanceCollection)this [""];
            collection.Remove (filename);
        }
        public void InsertAt (int index, string filename)
        {
            MRUListInstanceCollection collection = (MRUListInstanceCollection)this [""];
            collection.InsertAt (index, filename);
        }
    }

    public class MRUListInstanceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement ()
        {
            return new MRUListElement ();
        }

        protected override object GetElementKey (ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((MRUListElement)element).FileName;
        }
        public bool Contains (string filename)
        {
            object [] keys = base.BaseGetAllKeys ();
            for (int i = 0; i < keys.Length; ++i) {
                if (string.Compare (keys [i] as string, filename) == 0) {
                    return true;
                }
            }
            return false;
        }
        public void RemoveAt (int index)
        {
            base.BaseRemoveAt (index);
        }
        public void Remove (string filename)
        {
            base.BaseRemove (filename);
        }
        public void InsertAt (int index, string filename)
        {
            MRUListElement item = CreateNewElement () as MRUListElement;
            item.FileName = filename;
            base.BaseAdd (0, item);
        }
    }

    public class MRUListElement : ConfigurationElement
    {
        //Make sure to set IsKey=true for property exposed as the GetElementKey above
        [ConfigurationProperty ("filename", IsKey = true, IsRequired = true)]
        public string FileName {
            get { return (string)base ["filename"]; }
            set { base ["filename"] = value; }
        }
    }
}

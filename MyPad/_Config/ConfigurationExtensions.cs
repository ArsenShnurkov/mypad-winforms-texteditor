using System;
using System.Configuration;
using System.Globalization;

namespace MyPad
{
    public static class ConfigurationExtensions
    {
        public static EditorConfigurationSection GetEditorConfiguration (this Configuration cfg)
        {
            EditorConfigurationSection section = cfg.GetSection ("EditorConfiguration") as EditorConfigurationSection;
            if (section == null) {
                section = new EditorConfigurationSection ();
                cfg.Sections.Add ("EditorConfiguration", section);
            }
            //section.SectionInformation.ForceSave = true;
            return section;
        }
        public static MRUListConfigurationSection GetMRUList (this Configuration cfg)
        {
            MRUListConfigurationSection section = cfg.GetSection ("MRUList") as MRUListConfigurationSection;
            if (section == null) {
                section = new MRUListConfigurationSection ();
                cfg.Sections.Add ("MRUList", section);
            }
            //section.SectionInformation.ForceSave = true;
            return section;
        }
        public static void SaveAll (this Configuration cfg)
        {
            cfg.Save (ConfigurationSaveMode.Full);
        }
    }
}

using System;
using System.Configuration;
using System.Globalization;
// https://stackoverflow.com/questions/2718095/custom-app-config-section-with-a-simple-list-of-add-elements
/*
<configuration>
    <configSections>
        <section name="EditorConfiguration" type="MyPad.EditorConfigurationSection, MyPad" />
    </configSections>
    <EditorConfiguration>
    
    </EditorConfiguration>
</configuration>
*/
namespace MyPad
{
    public class EditorConfigurationSection : ConfigurationSection
    {
        [ConfigurationPropertyAttribute (nameof (AllowCaretBeyondEOL))]
        public BooleanElement AllowCaretBeyondEOL {
            get {
                var res = ((BooleanElement)(this [nameof (AllowCaretBeyondEOL)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (ConvertTabsToSpaces))]
        public BooleanElement ConvertTabsToSpaces {
            get {
                var res = ((BooleanElement)(this [nameof (ConvertTabsToSpaces)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (EnableFolding))]
        public BooleanElement EnableFolding {
            get {
                var res = ((BooleanElement)(this [nameof (EnableFolding)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowEOLMarkers))]
        public BooleanElement ShowEOLMarkers {
            get {
                var res = ((BooleanElement)(this [nameof (ShowEOLMarkers)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowHRuler))]
        public BooleanElement ShowHRuler {
            get {
                var res = ((BooleanElement)(this [nameof (ShowHRuler)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowInvalidLines))]
        public BooleanElement ShowInvalidLines {
            get {
                var res = ((BooleanElement)(this [nameof (ShowInvalidLines)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowLineNumbers))]
        public BooleanElement ShowLineNumbers {
            get {
                var res = ((BooleanElement)(this [nameof (ShowLineNumbers)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowMatchingBrackets))]
        public BooleanElement ShowMatchingBrackets {
            get {
                var res = ((BooleanElement)(this [nameof (ShowMatchingBrackets)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowSpaces))]
        public BooleanElement ShowSpaces {
            get {
                var res = ((BooleanElement)(this [nameof (ShowSpaces)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowTabs))]
        public BooleanElement ShowTabs {
            get {
                var res = ((BooleanElement)(this [nameof (ShowTabs)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowVRuler))]
        public BooleanElement ShowVRuler {
            get {
                var res = ((BooleanElement)(this [nameof (ShowVRuler)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (HighlightCurrentLine))]
        public BooleanElement HighlightCurrentLine {
            get {
                var res = ((BooleanElement)(this [nameof (HighlightCurrentLine)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (AutoInsertBrackets))]
        public BooleanElement AutoInsertBrackets {
            get {
                var res = ((BooleanElement)(this [nameof (AutoInsertBrackets)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (TrailingWhitespace))]
        public BooleanElement TrailingWhitespace {
            get {
                var res = ((BooleanElement)(this [nameof (TrailingWhitespace)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (IndentStyle))]
        public StringElement IndentStyle {
            get {
                var res = ((StringElement)(this [nameof (IndentStyle)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (FontName))]
        public StringElement FontName {
            get {
                var res = ((StringElement)(this [nameof (FontName)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (FontSize))]
        public FloatElement FontSize {
            get {
                var res = ((FloatElement)(this [nameof (FontSize)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (MainWindowX))]
        public IntElement MainWindowX {
            get {
                var res = ((IntElement)(this [nameof (MainWindowX)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (MainWindowY))]
        public IntElement MainWindowY {
            get {
                var res = ((IntElement)(this [nameof (MainWindowY)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (MainWindowWidth))]
        public IntElement MainWindowWidth {
            get {
                var res = ((IntElement)(this [nameof (MainWindowWidth)]));
                return res;
            }
        }
        [ConfigurationPropertyAttribute (nameof (MainWindowHeight))]
        public IntElement MainWindowHeight {
            get {
                var res = ((IntElement)(this [nameof (MainWindowHeight)]));
                return res;
            }
        }
    }

    public class StringElement : ConfigurationElement
    {
        [System.Configuration.ConfigurationPropertyAttribute ("value", IsRequired = true)]
        public string Value {
            get {
                var res = this ["value"] as string;
                return res;
            }
            set {
                this ["value"] = value;
            }
        }
    }

    public class BooleanElement : ConfigurationElement
    {
        [System.Configuration.ConfigurationPropertyAttribute ("value", IsRequired = true)]
        public bool Value {
            get {
                bool res = (bool)this ["value"];
                return res;
            }
            set {
                this ["value"] = value;
            }
        }
    }

    public class FloatElement : ConfigurationElement
    {
        [System.Configuration.ConfigurationPropertyAttribute ("value", IsRequired = true)]
        public float Value {
            get {
                float res = (float)this ["value"];
                return res;
            }
            set {
                this ["value"] = value;
            }
        }
    }
    public class IntElement : ConfigurationElement
    {
        [System.Configuration.ConfigurationPropertyAttribute ("value", IsRequired = true)]
        public int Value {
            get {
                int res = (int)this ["value"];
                return res;
            }
            set {
                this ["value"] = value;
            }
        }
    }
}

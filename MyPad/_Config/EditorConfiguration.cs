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
                return ((BooleanElement)(this [nameof (AllowCaretBeyondEOL)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (ConvertTabsToSpaces))]
        public BooleanElement ConvertTabsToSpaces {
            get {
                return ((BooleanElement)(this [nameof (ConvertTabsToSpaces)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (EnableFolding))]
        public BooleanElement EnableFolding {
            get {
                return ((BooleanElement)(this [nameof (EnableFolding)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowEOLMarkers))]
        public BooleanElement ShowEOLMarkers {
            get {
                return ((BooleanElement)(this [nameof (ShowEOLMarkers)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowHRuler))]
        public BooleanElement ShowHRuler {
            get {
                return ((BooleanElement)(this [nameof (ShowHRuler)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowInvalidLines))]
        public BooleanElement ShowInvalidLines {
            get {
                return ((BooleanElement)(this [nameof (ShowInvalidLines)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowLineNumbers))]
        public BooleanElement ShowLineNumbers {
            get {
                return ((BooleanElement)(this [nameof (ShowLineNumbers)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowMatchingBrackets))]
        public BooleanElement ShowMatchingBrackets {
            get {
                return ((BooleanElement)(this [nameof (ShowMatchingBrackets)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowSpaces))]
        public BooleanElement ShowSpaces {
            get {
                return ((BooleanElement)(this [nameof (ShowSpaces)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowTabs))]
        public BooleanElement ShowTabs {
            get {
                return ((BooleanElement)(this [nameof (ShowTabs)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (ShowVRuler))]
        public BooleanElement ShowVRuler {
            get {
                return ((BooleanElement)(this [nameof (ShowVRuler)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (HighlightCurrentLine))]
        public BooleanElement HighlightCurrentLine {
            get {
                return ((BooleanElement)(this [nameof (HighlightCurrentLine)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (AutoInsertBrackets))]
        public BooleanElement AutoInsertBrackets {
            get {
                return ((BooleanElement)(this [nameof (AutoInsertBrackets)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (IndentStyle))]
        public StringElement IndentStyle {
            get {
                return ((StringElement)(this [nameof (IndentStyle)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (FontName))]
        public StringElement FontName {
            get {
                return ((StringElement)(this [nameof (FontName)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (FontSize))]
        public FloatElement FontSize {
            get {
                return ((FloatElement)(this [nameof (FontSize)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (MainWindowX))]
        public IntElement MainWindowX {
            get {
                return ((IntElement)(this [nameof (MainWindowX)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (MainWindowY))]
        public IntElement MainWindowY {
            get {
                return ((IntElement)(this [nameof (MainWindowY)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (MainWindowWidth))]
        public IntElement MainWindowWidth {
            get {
                return ((IntElement)(this [nameof (MainWindowWidth)]));
            }
        }
        [ConfigurationPropertyAttribute (nameof (MainWindowHeight))]
        public IntElement MainWindowHeight {
            get {
                return ((IntElement)(this [nameof (MainWindowHeight)]));
            }
        }
    }

    public class StringElement : ConfigurationElement
    {
        [System.Configuration.ConfigurationPropertyAttribute ("value", IsRequired = true)]
        public string Value {
            get {
                return this ["value"] as string;
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
                bool result = (bool)this ["value"];
                return result;
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
                float result = (float)this ["value"];
                return result;
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
                                int result = (int)this ["value"];
                return result;
            }
            set {
                this ["value"] = value;
            }
        }
    }
}


This script folder contains scripts to be used with the editor.

Each script is written in Lua and must be in the following format.

Folder - UniqueName
    XML File - ScriptInstaller.xml
    Lua File - Script.lua (Can be named whatever you want)

The ScriptInstaller.xml file contains the following tags.

<Script> - Defines a script and holds the script information.
    <Name> - The name of your script (eg. Test)
    <Type> - The type of script (MenuScript or TextScript)
    <File> - The relative path of the script (eg. Test.lua)
    <Description> - A small description about the script
    <Enabled> - Sets the script to enabled (True or False)

See the Test Script to see how the ScriptInstaller.xml file is setup.

Your script can contain any function included in Lua as well as the following
that are added.

Object TextEditor - The current TextEditorControl
Function GetEditor() - Gets the current TextEditorControl
Function InsertString(Object editor, int offset, string text) - Inserts a string into the editor at the offset.
Function RemoteString(Object editor, int offset, int length) - Removes a span of text from the editor.
Function Replace(Object editor, string text, string replacement) - Replaces a string in the editor.
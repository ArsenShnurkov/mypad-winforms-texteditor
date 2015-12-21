using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

// this attribute will be used to determine the folder name in SpecialFolder.ApplicationData
// (i.e. /home/${user}/.config/${AssemblyProduct}/${AssemblyTitle}.config )
[assembly: AssemblyProduct("MyPad")]

// MSDN says that it specifies a description for an assembly and that
// the assembly title is a friendly name which can include spaces.
// Visual Studio asks for the assembly name in the properties window of the project along with the default namespace.
// http://stackoverflow.com/questions/23144872/assemblytitle-attribute-in-the-net-framework
// "I don't see how AssemblyTitle differs from AssemblyProduct"
// [AssemblyTitle] is a pretty big deal,
// it is directly visible when you right-click on the assembly and use Properties + Details.
[assembly: AssemblyTitle("MyPad")]

[assembly: AssemblyDescription("Simple lightweight text editor written in C# (based on ICSharpCode.TextEditor control)")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]

[assembly: AssemblyCopyright("MIT X11, https://en.wikipedia.org/wiki/MIT_License")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("a3389656-9234-427d-8934-e96d1a4b0d40")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.3.0")]
[assembly: AssemblyFileVersion("1.0.1.0")]

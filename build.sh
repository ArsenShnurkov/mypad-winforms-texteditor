#!/bin/bash

function bin_dir ( ) {
	echo "Bin"
}

function usedebug_tostring ( ) {
	echo "Debug"
}

function csharp_sources() {
	local DIR_NAME=$1
	for f in "${DIR_NAME}"/*; do
		if [ -d $f ];
		then
			csharp_sources "$f"
		else
			case "$f" in
			*.cs ) 
			        # it's source code file
				echo -n "$f "
			        ;;
			*)
			        # it's not
			        ;;
			esac
		fi
	done
}

function bin_dir ( ) {
	echo "bin/$(usedebug_tostring)"
}

function output_arguments ( ) {
	local OUTPUT_TYPE="library"
	if [ "$2" == "exe" ];  then OUTPUT_TYPE="exe"; fi
	local OUTPUT_NAME="$(bin_dir)/$1.$2"
	echo  "/target:${OUTPUT_TYPE}" "/out:${OUTPUT_NAME}"
}

function dump_command() {
	for word in $@
	do
	    echo $word
	done
}

function references1() {
	echo -n " " /reference:System.dll
	echo -n " " /reference:System.Core.dll
	echo -n " " /reference:System.Drawing.dll
	echo -n " " /reference:System.Windows.Forms.dll
	echo -n " " /reference:System.Xml.dll
	echo -n " " /reference:System.Runtime.Remoting.dll
	echo -n " " /reference:System.Web.dll
	echo -n " " /reference:System.Configuration.dll
	echo -n " " /reference:System.ServiceModel.dll
}


function references2() {
	echo -n " " /reference:/usr/share/mono/assemblies/icsharpcode-texteditor/ICSharpCode.TextEditor.dll
	echo -n " " /reference:/usr/share/mono/assemblies/ndepend-path-1/NDepend.Path.dll
	echo -n " " /reference:$(bin_dir)/MyPad.Plugins.dll
	echo -n " " /reference:System.dll
	echo -n " " /reference:System.Core.dll
	echo -n " " /reference:System.Drawing.dll
	echo -n " " /reference:System.Windows.Forms.dll
	echo -n " " /reference:System.Xml.dll
	echo -n " " /reference:System.Runtime.Remoting.dll
	echo -n " " /reference:System.Web.dll
	echo -n " " /reference:System.Configuration.dll
	echo -n " " /reference:System.ServiceModel.dll
}

PROJECT1="/usr/bin/csc $(references1)  $(csharp_sources MyPad.Plugins) $(output_arguments MyPad.Plugins dll)"
dump_command ${PROJECT1}
eval ${PROJECT1}

PROJECT2="/usr/bin/csc $(references2)  $(csharp_sources MyPad) $(output_arguments MyPad.Plugins exe)"
dump_command ${PROJECT2}
eval ${PROJECT2}

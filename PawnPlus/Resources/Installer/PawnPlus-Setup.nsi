;PawnPlus Installer

;--------------------------------
;Includes

	!include "MUI2.nsh"
	!include "WordFunc.nsh"
	
	!define MUI_ICON "..\Icons\Installer-Icon.ico"
	
	!define MUI_WELCOMEFINISHPAGE_BITMAP "..\Images\PawnPlus_Install_Dialog.bmp" 
	!define MUI_HEADERIMAGE
	!define MUI_HEADERIMAGE_RIGHT
	!define MUI_HEADERIMAGE_BITMAP "..\Images\PawnPlus_Install_Banner.bmp"  
	
	!define CurrentVersion 0.4.8.1
	!insertmacro VersionCompare
;--------------------------------
;General

	Name "PawnPlus"
	OutFile "PawnPlus-Setup-0.4.8.1.exe"
	
	InstallDir "$PROGRAMFILES\PawnPlus"
	
	InstallDirRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" "Install Location"

	RequestExecutionLevel admin
	
;--------------------------------
;Interface Settings

	!define MUI_ABORTWARNING

	!define MUI_LANGDLL_ALLLANGUAGES
	
	;Finish option to create desktop icon if it is checked.
	!define MUI_FINISHPAGE_SHOWREADME ""
	!define MUI_FINISHPAGE_SHOWREADME_CHECKED
	!define MUI_FINISHPAGE_SHOWREADME_TEXT "Create Desktop Shortcut"
	!define MUI_FINISHPAGE_SHOWREADME_FUNCTION CreateDesktopShortcut
		
;--------------------------------
;Language Selection Dialog Settings

	!define MUI_LANGDLL_REGISTRY_ROOT "HKCU" 
	!define MUI_LANGDLL_REGISTRY_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" 
	!define MUI_LANGDLL_REGISTRY_VALUENAME "Installer Language"

;--------------------------------
;Pages

	!insertmacro MUI_PAGE_WELCOME
	!insertmacro MUI_PAGE_LICENSE "..\..\..\LICENSE"
	!insertmacro MUI_PAGE_DIRECTORY
	!insertmacro MUI_PAGE_INSTFILES
	!insertmacro MUI_PAGE_FINISH
	
	!insertmacro MUI_UNPAGE_CONFIRM
	!insertmacro MUI_UNPAGE_INSTFILES
;--------------------------------
;Languages

	!insertmacro MUI_LANGUAGE "English"
	!insertmacro MUI_LANGUAGE "French"
	!insertmacro MUI_LANGUAGE "German"
	!insertmacro MUI_LANGUAGE "Spanish"
	!insertmacro MUI_LANGUAGE "SpanishInternational"
	!insertmacro MUI_LANGUAGE "SimpChinese"
	!insertmacro MUI_LANGUAGE "TradChinese"
	!insertmacro MUI_LANGUAGE "Japanese"
	!insertmacro MUI_LANGUAGE "Korean"
	!insertmacro MUI_LANGUAGE "Italian"
	!insertmacro MUI_LANGUAGE "Dutch"
	!insertmacro MUI_LANGUAGE "Danish"
	!insertmacro MUI_LANGUAGE "Swedish"
	!insertmacro MUI_LANGUAGE "Norwegian"
	!insertmacro MUI_LANGUAGE "NorwegianNynorsk"
	!insertmacro MUI_LANGUAGE "Finnish"
	!insertmacro MUI_LANGUAGE "Greek"
	!insertmacro MUI_LANGUAGE "Russian"
	!insertmacro MUI_LANGUAGE "Portuguese"
	!insertmacro MUI_LANGUAGE "PortugueseBR"
	!insertmacro MUI_LANGUAGE "Polish"
	!insertmacro MUI_LANGUAGE "Ukrainian"
	!insertmacro MUI_LANGUAGE "Czech"
	!insertmacro MUI_LANGUAGE "Slovak"
	!insertmacro MUI_LANGUAGE "Croatian"
	!insertmacro MUI_LANGUAGE "Bulgarian"
	!insertmacro MUI_LANGUAGE "Hungarian"
	!insertmacro MUI_LANGUAGE "Thai"
	!insertmacro MUI_LANGUAGE "Romanian"
	!insertmacro MUI_LANGUAGE "Latvian"
	!insertmacro MUI_LANGUAGE "Macedonian"
	!insertmacro MUI_LANGUAGE "Estonian"
	!insertmacro MUI_LANGUAGE "Turkish"
	!insertmacro MUI_LANGUAGE "Lithuanian"
	!insertmacro MUI_LANGUAGE "Slovenian"
	!insertmacro MUI_LANGUAGE "Serbian"
	!insertmacro MUI_LANGUAGE "SerbianLatin"
	!insertmacro MUI_LANGUAGE "Arabic"
	!insertmacro MUI_LANGUAGE "Farsi"
	!insertmacro MUI_LANGUAGE "Hebrew"
	!insertmacro MUI_LANGUAGE "Indonesian"
	!insertmacro MUI_LANGUAGE "Mongolian"
	!insertmacro MUI_LANGUAGE "Luxembourgish"
	!insertmacro MUI_LANGUAGE "Albanian"
	!insertmacro MUI_LANGUAGE "Breton"
	!insertmacro MUI_LANGUAGE "Belarusian"
	!insertmacro MUI_LANGUAGE "Icelandic"
	!insertmacro MUI_LANGUAGE "Malay"
	!insertmacro MUI_LANGUAGE "Bosnian"
	!insertmacro MUI_LANGUAGE "Kurdish"
	!insertmacro MUI_LANGUAGE "Irish"
	!insertmacro MUI_LANGUAGE "Uzbek"
	!insertmacro MUI_LANGUAGE "Galician"
	!insertmacro MUI_LANGUAGE "Afrikaans"
	!insertmacro MUI_LANGUAGE "Catalan"
	!insertmacro MUI_LANGUAGE "Esperanto"
	!insertmacro MUI_LANGUAGE "Basque"
	!insertmacro MUI_LANGUAGE "Welsh"

;--------------------------------
;Version Information

	VIProductVersion "${CurrentVersion}"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "PawnPlus"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "Comments" ""
	VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" ""
	VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "PawnPlus 2014"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" ""
	VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "${CurrentVersion}"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductVersion" "${CurrentVersion}"
	
;--------------------------------
;Reserve Files

	!insertmacro MUI_RESERVEFILE_LANGDLL

;--------------------------------
;Installer Sections

Var UNINSTALL_OLD_VERSION

Section "PawnPlus Section" PawnPlusSection
	StrCmp $UNINSTALL_OLD_VERSION "" core.files
	ExecWait '$UNINSTALL_OLD_VERSION'

	core.files:
		
		SetOutPath "$INSTDIR"
	
		File /r "..\..\bin\Release\*.*"
 
		CreateDirectory "$SMPROGRAMS\PawnPlus"
		CreateShortCut "$SMPROGRAMS\PawnPlus\Uninstall.lnk" "$INSTDIR\Uninstall.exe" "" "$INSTDIR\Uninstall.exe" 0
		CreateShortCut "$SMPROGRAMS\PawnPlus\PawnPlus.lnk" "$INSTDIR\PawnPlus.exe" "" "$INSTDIR\PawnPlus.exe" 0
		
		CreateDirectory "$APPDATA\PawnPlus"

		WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" "DisplayName" "PawnPlus"
		WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" "DisplayVersion" "${CurrentVersion}"
		WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" "DisplayIcon" "$INSTDIR\PawnPlus.exe"
		WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" "Publisher" "PawnPlus"
		WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" "UninstallString" "$INSTDIR\Uninstall.exe"
	
		WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" "Install Location" $INSTDIR
	
		WriteUninstaller "$INSTDIR\Uninstall.exe"
		
		AccessControl::GrantOnFile "$INSTDIR" "ListDirectory + GenericRead + GenericExecute + GenericWrite + GenericExecute"
		AccessControl::GrantOnFile "$INSTDIR" "(Users)" "FullAccess"
		AccessControl::GrantOnFile "$INSTDIR" "(BU)" "FullAccess"
		AccessControl::EnableFileInheritance "$INSTDIR"

SectionEnd

;--------------------------------
;Installer Functions

Function .onInit

	!insertmacro MUI_LANGDLL_DISPLAY
	
	ClearErrors
	ReadRegStr $0 HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" "DisplayVersion"
	IfErrors init.uninst
	${VersionCompare} $0 ${CurrentVersion} $1
	IntCmp $1 2 init.uninst
    MessageBox MB_YESNO|MB_ICONQUESTION "PawnPlus seems to be already installed on your system, current version is $0.$\nWould you like to proceed with the installation of version ${CurrentVersion}?" \
        IDYES init.uninst
    Quit

	init.uninst:
		ClearErrors
		ReadRegStr $0 HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus" "Install Location"
		IfErrors init.done
		StrCpy $UNINSTALL_OLD_VERSION '"$0\uninstall.exe" /S _?=$0'
	init.done:

FunctionEnd
;--------------------------------
;Uninstaller Section

Section "Uninstall"

	RMDir /r "$APPDATA\PawnPlus\"

	Delete "$INSTDIR\Uninstall.exe"
	
	RMDir /r "$INSTDIR\"

	Delete "$DESKTOP\PawnPlus.lnk"
	Delete "$SMPROGRAMS\PawnPlus\*.*"
	
	DeleteRegKey HKCU "Software\PawnPlus"
	DeleteRegKey HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\PawnPlus"

SectionEnd


;--------------------------------
;Uninstaller Functions

Function un.onInit

	!insertmacro MUI_UNGETLANGUAGE
	
FunctionEnd

Function CreateDesktopShortcut

	CreateShortCut "$DESKTOP\PawnPlus.lnk" "$INSTDIR\PawnPlus.exe" ""
	
FunctionEnd

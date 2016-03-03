; Niv_en.iss
; Configuration file for Inno Setup which is an installer generator.

[Setup]
AppName=Niv
AppVersion=0.3.2
AppPublisher=Jarvis Niu
AppSupportURL=http://jarvisniu.com/niv
DefaultDirName={pf}\Niv
DefaultGroupName=Niv
UninstallDisplayIcon={app}\Niv.exe
UninstallDisplayName=Niv
Compression=lzma2
SolidCompression=yes
OutputDir=".\"
OutputBaseFilename="Niv_0.3.2_en_setup"

[Files]
Source: "Niv.exe"; DestDir: "{app}"

[Icons]
Name: "{group}\Niv"; Filename: "{app}\Niv.exe"
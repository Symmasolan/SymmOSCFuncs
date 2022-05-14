@echo off
set VRCFOLDER="C:\Program Files (x86)\Steam\steamapps\common\VRChat\"

:: DO NOT CHANGE ANYTHING BELOW THIS LINE
:: Unless you know what you're doing!

:: IF VRChat isn't running, then start VRChat.
tasklist /fi "ImageName eq VRChat.exe" /fo csv 2>NUL | find /I "VRChat.exe">NUL
if "%ERRORLEVEL%"=="0" (
  :: Program is running. Skip starting it a second time
  echo VRChat is already running.
  goto oscstart
)
echo Launching VRChat!
start "" "%VRCFOLDER%launch.exe"
echo Waiting 15 seconds for VRChat startup...
echo [Press any key to skip the pause]
timeout /t 15

:oscstart
:: Start the OSC program
echo Launching Symm OSC Functions!
start "" "%~dp0SymmOSCFuncs.exe"
timeout 3
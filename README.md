# Symmasolan's OSC Functions
### What is this?
An OSC program for VRChat and Unity that uses [WeatherAPI](https://www.weatherapi.com/) to get the current phase and illumination of the moon, and return it as useable values.

------
# Setup
This readme assumes that you're using the most current release.

## Getting your own WeatherAPI api key
Go to [www.weatherapi.com](https://www.weatherapi.com/).
Create a new account and get a **FREE** plan API key. You won't need any higher level than free.

Once you log in, there will be an API key visible.
**DO NOT SHARE THIS API KEY WITH ANYBODY!** API keys are sensitive information. Treat them the same as you would a password.
Be warned, there are multiple places on the website where your API key is not visually obfuscated in any way while logged in!

Create a new file under `./Secrets/` named `weatherapi.key`.
Copy and paste your API key into this file.

## Editing the Config to set your weather location
Open the `SymmOSCFuncs.dll.config` file with any text editor of your choice.
Change the `"London"` value to a location of your choosing
  - This `Location` value is not stored, sent, or used anywhere other than with WeatherAPI for standard API actions.
  - Any valid ZIP code number or state/province name will work!
  - Here are a few examples of valid options:
    - City/Province:
      - `<add key="Location" value="London" />`
      - `<add key="Location" value="New York City" />`
      - `<add key="Location" value="Nova Scotia" />`
    - ZIP Code:
      - `<add key="Location" value="12345" />`
      - `<add key="Location" value="12345-6789" />`

## (Optional) Editing `start.bat`
You only need to take this step if you're choosing to use the `start.bat` file to start both VRChat and this program at the same time.

Right click -> Edit `start.bat`
Change the second line's value (VRCFOLDER=) to your local VRChat install folder.
  - This is usually `C:\Program Files (x86)\Steam\steamapps\common\VRChat\`
  - Make sure your folder location is surrounded in quotations ("")
  - Make sure you ONLY use backslashes in your folder path! (\\)

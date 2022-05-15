# Symmasolan's OSC Functions
### What is this?
A standalone OSC program for VRChat that uses [WeatherAPI](https://www.weatherapi.com/) to retrieve the current phase and illumination of the moon, returning it as useable values via OSC.

------
# Setup
This readme assumes that you're using the most current [release](https://github.com/Symmasolan/SymmOSCFuncs/releases/latest) of the OSC program itself, as well as the `SymmOSCFuncs.unitypackage` (also at [Releases!](https://github.com/Symmasolan/SymmOSCFuncs/releases/latest)).

This will also assume basic knowledge/understanding of Unity Animators.

## Setting up your Unity project
In the latest VRChat supported version of Unity, import the following:
- Latest VRChat Avatar SDK
- (Optional) Poiyomi 7+
  - The included `.unitypackage` in [Releases](https://github.com/Symmasolan/SymmOSCFuncs/releases/latest) will have Poiyomi 8 materials as an example, but any animatable shader of your choosing will work similarly to the example animations.

## Setting up your VRChat Animation Layers with MoonOSC
This will depend on what you plan to manipulate using this program's output, but for the majority of situations you will be placing the `FX_MoonOSC` Animation Controller into your FX layer.

## Getting your own WeatherAPI api key
1. Go to [www.weatherapi.com](https://www.weatherapi.com/) and create a new account and get a **FREE** plan API key. You won't need any higher level than free.
2. Once you log in, there will be an API key visible.
  - **DO NOT SHARE THIS API KEY WITH ANYBODY.** API keys are sensitive information. Treat them the same as you would a password.
  - Be warned, there are multiple places on the website where your API key is not visually obfuscated in any way while logged in!
3. Create a new folder named `Secrets`. Then, create a new file under `/Secrets` named `weatherapi.key`.
4. Copy and paste your WeatherAPI key into the `weatherapi.key` file.

## Editing the Config to set your weather location
1. Open the `SymmOSCFuncs.dll.config` file with any text editor of your choice.

2. Change the `"London"` value to a location of your choosing
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


1. Right click -> Edit `start.bat`

2. Change the second line's value (VRCFOLDER=) to your local VRChat install folder.
  - This is usually `C:\Program Files (x86)\Steam\steamapps\common\VRChat\`
  - Make sure your folder location is surrounded in quotations ("")
  - Make sure you ONLY use backslashes in your folder path! (\\)

------
## Todo
- Explain where to put individual animations in each layer for the `FX_MoonOSC` animation controller.

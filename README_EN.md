# Project Overview

This is a Rogue Visual Novel developed with Unity.Tip:It's just a project used to practice.

# Develop Environment

Unity 2022.3.62f1c1
Visual Studio 2022

# File Directory

```text
Assets/
├─ Scenes/             Main menu, game, and sample scenes
├─ Scripts/            Game C# source code
│  ├─ Audio/            Audio playback and management
│  ├─ Bootstrap/        Scene startup and system binding
│  ├─ Display/          Display system
│  ├─ InputSystem/      Input commands and input states
│  ├─ MainMenu/         Main menu, gallery, settings, and save panel
│  ├─ Rogue/            Maps, nodes, story steps, saves, and state machines
│  ├─ Setting/          Settings configuration and persistence
│  └─ Utility/          Event bus, asset loaders, and shared code
├─ AssetBundles/       Source assets for maps, prefabs, and audio
├─ Resources/          Runtime resources and test sprites
├─ StreamingAssets/    AssetBundle files shipped with the project
├─ TextMesh Pro/       TextMesh Pro assets
└─ Tree_Textures/      Texture assets
Packages/              Unity package configuration
ProjectSettings/       Unity project and build settings
```


# How to Open

Open File "\Assets\Scenes\MainMenu.unity" in Unity Editor and startup it.
Notes: The directories "E:unityNotmeAssetsResourcesTestSprites", "E:unityNotmeAssetsAssetBundlesAudio", and "E:unityNotmeAssetsAssetBundlesSprites" are all uploaded, this project may have missing resource references. Thanks for your understanding.

# Content Stats

| Item | Count |
| --- | ---: |
| Game scripts (`Assets/Scripts/**/*.cs`) | 210 files |
| C# source lines (including blank lines and comments) | 10,314 |
| Nonblank C# lines | 9,580 |
| Scene files | 3 total; 2 enabled in Build Settings (`MainMenu`, `Game`) |
| Type declarations | 242: 190 classes, 3 structs, 32 interfaces, 17 enums |

# What has been done

- Added the basic main menu, game scene, gallery, and settings flows, with code to enter the game and return to the menu.
- Implemented Rogue map configuration loading and construction, layer and node progression, story steps, and choice branches.
- Added playback code for dialogue, characters, backgrounds, and BGM, plus audio, display, and input systems.
- Added a manual save panel, JSON save read/write, save slot display, and basic data validation. Restoring full progress from a save remains limited, as noted below.
- Added settings loading, editing, and saving, along with error handling for some initialization and runtime failures.

# Known issue

- Loading a save finds and builds its map, but does not yet restore the saved current node or its completion state into the new map progression. A run cannot be resumed completely.
- The issue log records that an input buffered while leaving an end node can move the camera toward the previous node after a new map loads. This still needs verification in the current version.
- Gallery input still uses a different, older input path from the other main menu buttons.
- Some loaders, players, and state base classes still contain `NotImplementedException` placeholders; calling those paths may interrupt execution.
- All game scripts compile into the default Assembly-CSharp assembly; there are no asmdef compilation boundaries.
- There are no automated tests, so some edge cases require manual verification.

# Other Feeling

Hello,here's XI(Hsin or Eleven, as you like).I would be honored if you can see it.This might take up some of your time because this is some meaningless content, so if you don't want to read it, please just ignore this part.
Origianlly,this project was prepared for 21days-Gamejam.However,it don't as a entry because of the misjudgement of my ability an time.What's worse is that the completion date has been pushed from July 2026 all the way to October 2026.
Originally,I thought I could prepare Examination and this project(include code,screenwrite,art and other) at the same time,but now,It's obviously imposible.Just the code alone took me a few months.
There are a mount of question in this project.Example:chaotic namespace,chaotic file organization,no assembly devision, no GameManager and an architecture that's not particularly great.Everyone is a drawback for program.(Please stop making fun of this coding,guys.This code,was all writen by me,line by line.Meme.jpg)
I even feel that I'm getting worse and worse at coding.Every time I'm about to finish, the feeling of opening the editor and not being able to write anything for an hour hits me extra hard.
Then,I want to have a little talk about the relatation between game and developer.In my view,It's necessary to tast the game for developer,Obviously.But I feel like this sentence can only be directed at planners or the person in the team who comes up with ideas or takes responsibility. For others (at least programmers, or at least from my personal perspective), I don't think this should be something they have to do. Please allow me to express this somewhat extreme opinion. At least for me, I wouldn't go through something that makes writing it incredibly painful. I can say this because the development process is obviously not enjoyable.I'm sorry.It's my problem.Maybe I shouldn't talk about it in here.It's just my rant but I really,really want to say it out.

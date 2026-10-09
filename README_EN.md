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
├─ AssetBundles/       Map configs and prefabs; Audio/Sprites are local assets
├─ Resources/          Runtime resources; TestSprites contains local test assets
├─ StreamingAssets/    AssetBundle files shipped with the project
├─ TextMesh Pro/       TextMesh Pro assets
└─ Tree_Textures/      Texture assets
Packages/              Unity package configuration
ProjectSettings/       Unity project and build settings
```


# How to Open

Open the project root in Unity Hub, then open `Assets/Scenes/MainMenu.unity` in the Unity Editor and run it.

`Assets/Resources/TestSprites/`, `Assets/AssetBundles/Audio/`, and `Assets/AssetBundles/Sprites/` contain local assets that are not tracked by Git. A fresh checkout may have missing references if a scene uses those assets.

# Content Stats

| Item | Count |
| --- | ---: |
| Game scripts (`Assets/Scripts/**/*.cs`) | 210 files |
| C# source lines (including blank lines and comments) | 9,927 |
| Nonblank C# lines | 9,199 |
| Scene files | 3 total; 2 enabled in Build Settings (`MainMenu`, `Game`) |
| Type declarations | 241: 189 classes, 3 structs, 32 interfaces, 17 enums |

Counts reflect the current working tree under `Assets/Scripts/`. Type declarations were counted from source keywords.

# What has been done

- Added the basic main menu, game scene, gallery, and settings flows, with entry points for scene switching and returning to the menu.
- Implemented Rogue map configuration loading and construction, layer and node progression, story steps, and choice branches.
- Added playback code for dialogue, characters, backgrounds, and BGM, plus audio, display, and input systems.
- Added a manual save panel, JSON save read/write, and save slot display. Loading a save currently has a blocking issue, as noted below.
- Added settings loading, editing, and saving.

# Known issue

- The map ID lookup used when loading a save reads a cache that is never populated, so saved maps cannot currently be loaded. Even if a map loads, the saved current node and its completion state are not restored, so a run cannot be resumed completely.
- The issue log records that an input buffered while leaving an end node can move the camera toward the previous node after a new map loads. This still needs verification in the current version.
- Some loaders, players, and state base classes still contain `NotImplementedException` placeholders; calling those paths may interrupt execution.
- All game scripts compile into the default Assembly-CSharp assembly; there are no asmdef compilation boundaries.
- There are no automated tests for game code, so some edge cases require manual verification.

# Other Feeling

```text
Hello,here's XI(Hsin or Eleven, as you like).I would be honored if you can see it.This might take up some of your time because this is some meaningless
content, so if you don't want to read it, please just ignore this part.

Originally,this project was prepared for 21-day Gamejam.However,it was not submitted as a entry because of the misjudgement of my ability and time.What's worse is
that the completion date has been pushed from July 2026 all the way to October 2026.

Originally,I thought I could prepare for exams and work on this project(include code,screenwrite,art and other) at the same time,but now,It's obviously
impossible.Just the code alone took me a few months.

There are many problems in this project.Example:chaotic namespace,chaotic file organization,no assembly definitions, no GameManager and an
architecture that's not particularly great.Each of this is a drawback for the project.(Please stop making fun of this coding,guys.This code,was all written by me,
line by line.Meme.jpg)

I even feel that I'm getting worse and worse at coding.Every time I'm about to finish, the feeling of opening the editor and not being able to write
anything for an hour hits me extra hard.

Then,I want to have a little talk about the relatationship between games and developers.In my view,It's necessary to play their game for developer,Obviously.But
I feel like this sentence can only be directed at planners or the person in the team who comes up with ideas or takes responsibility. For others (at
least programmers, or at least from my personal perspective), I don't think this should be something they have to do. Please allow me to express this
somewhat extreme opinion. At least for me, I wouldn't want to play a game that was painful for me to develop. I can say this because the
development process is obviously not enjoyable.I'm sorry.It's my problem.Maybe I shouldn't talk about it in here.It's just my rant but I really,really
want to say it out.

Secondly, the most important thing about a game has got to be that it’s fun. It has to have gameplay that can attract players, whether it’s a
single-player game or an online game that keeps running. I think this applies across the board. This is my weakness—I don’t have the skills to be an
excellent gameplay designer or planner: I just don’t know how to design a fun game. Naturally, this part wasn’t implemented in the project either.
```

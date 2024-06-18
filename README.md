# Setup
Add this into the `Game` class (Assembly-CSharp.dll)
```
static Game()
{
        Activator.CreateInstance(Assembly.LoadFrom(Path.Combine(Application.dataPath, "../Blizzard/Assemblies/Blizzard.Entry.dll")).GetType("Blizzard.Entry.Entry"));
}
```

Setup Path Var called "SNOWTOPIA" to be the game head folder (for dependencies)

Restart PC


Compiled DLLS go in %SNOWTOPIA%/Blizzard/Assemblies

Content go in %SNOWTOPIA%/Blizzard/Content

Currently add this to the Content Folder [blizzard_content](https://discord.com/channels/932741876174454914/934204225154588722/1252649344318836877)

Now you can open the VS project

# Setup
Add this into the `Game` class (Assembly-CSharp.dll)
```
static Game()
{
        Activator.CreateInstance(Assembly.LoadFrom(Path.Combine(Application.dataPath, "../Blizzard/Assemblies/Blizzard.Standalone.dll")).GetType("Blizzard.Standalone.BlizzardStandalone"));
}
```

1. Setup Path Var called "SNOWTOPIA" to be the game head folder (for dependencies)
2. Restart PC
3. Have a version of blizzard installed (latest)
4. Now you can open the VS project

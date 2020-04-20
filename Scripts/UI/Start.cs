using Godot;
using System;

public class Start : Control
{
    [Export] public Resource StartScene = null;

    private PackedScene scene = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        scene = ResourceLoader.Load<PackedScene>(StartScene.ResourcePath);
    }

    public void OnStartPressed()
    {
        if (StartScene != null)
        {
            GetTree().ChangeSceneTo(scene);
        }
    }


    public void OnSettingsPressed()
    {
        GD.PushError("Not implemented yet!");
    }

    public void OnCreditsPressed()
    {
        GD.PushError("Not implemented yet!");
    }


    public void OnExitPressed()
    {
        GetTree().Quit();
    }
}

using Godot;
using System;

public class EndScene : AnimatedSprite
{
    [Export] public string SceneToLoad = "Menu";
    [Export] public float RunningTime = 5f;
    private float elapsed = 0f;

    public override void _Ready()
    {
        Play("default");
    }

    public override void _Process(float delta)
    {

        if (elapsed > RunningTime)
            GetTree().ChangeScene($"res://Scenes/{SceneToLoad}.tscn");

        if (Input.IsActionPressed("Cancel"))
            GetTree().ChangeScene($"res://Scenes/{SceneToLoad}.tscn");

        elapsed += delta;
    }
}


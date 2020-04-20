using Godot;
using System;

public class EndScene : AnimatedSprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export] public string SceneToLoad = "Menu";
    [Export] public float RunningTime = 5f;
    private float elapsed = 0f;

    public override void _Ready()
    {
        Play("default");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        if (elapsed > RunningTime)
            GetTree().ChangeScene($"res://Scenes/{SceneToLoad}.tscn");

        if (Input.IsActionPressed("Cancel"))
            GetTree().ChangeScene($"res://Scenes/{SceneToLoad}.tscn");

        elapsed += delta;
    }
}


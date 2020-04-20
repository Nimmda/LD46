using Godot;
using System;

public class Steam : AnimatedSprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private float elapsed = 0f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Playing = true;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        elapsed += delta;

        if (elapsed > 1f)
            QueueFree();
    }
}

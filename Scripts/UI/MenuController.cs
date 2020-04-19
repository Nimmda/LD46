using Godot;
using System;

public class MenuController : Node2D
{
    public override void _Ready()
    {

    }
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GetTree().Paused = !GetTree().Paused;
        }
    }
}

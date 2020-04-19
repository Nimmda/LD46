using Godot;
using System;

public class Game : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        VisualServer.SetDefaultClearColor(new Color(0f, 0f, 0f, 1));
    }


}

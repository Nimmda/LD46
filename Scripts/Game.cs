using Godot;
using System;

public class Game : Node2D
{

    public override void _Ready()
    {
        VisualServer.SetDefaultClearColor(new Color(0f, 0f, 0f, 1));
    }

}

using Godot;
using System;

public class EndScene : AnimatedSprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private bool isPlaying = false;
    public override void _Ready()
    {


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (!isPlaying)
        {
            Frame = 0;
            Playing = true;
            isPlaying = true;
        }
        else
        {
            if (Frame == Animation.Length - 1)
                GetTree().ChangeScene("res://Scenes/Menu.tscn");
        }
    }
}

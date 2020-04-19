using Godot;
using System;

public class LevelStart : Node2D
{
    [Export] public Vector2 PlayerPosition;
    public override void _Ready()
    {
        var game = GetTree().Root.GetNode<Game>("Game");
        var player = game.GetNode<Player>("Player");

        if (PlayerPosition != Vector2.Zero)
            player.Position = PlayerPosition;
        else
            player.Position = (FindNode("Entry") as Node2D).Position;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

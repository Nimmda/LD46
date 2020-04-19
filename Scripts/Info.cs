using Godot;
using System;

public class Info : Label
{
    private Player player = null;


    public override void _Ready()
    {
        player = GetNode<Player>("/root/Game/Player");
    }

    public override void _Process(float delta)
    {
        Text = $"Life: {player.healtPoints}";
    }
}

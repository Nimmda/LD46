using Godot;
using System;

public class WaterPool : Area2D
{
    private Player player = null;
    private bool playerInWater = false;
    private float elapsed = 0f;

    private PackedScene steam = null;

    public override void _Ready()
    {
        steam = ResourceLoader.Load<PackedScene>("res://Scenes/Steam.tscn");
    }

    public void OnWaterPoolBodyEntered(Node body)
    {
        if (!(body is Player))
            return;

        player = (body as Player);

        playerInWater = true;
        elapsed = 1f;

        // toogle gravity
        player.ToggleWaterGravity();
    }

    public void OnWaterPoolBodyExited(Node body)
    {
        if (!(body is Player))
            return;

        playerInWater = false;
        elapsed = 0f;

        player.ToggleWaterGravity();

        player = null;
    }


    // every second 3 damage
    public override void _Process(float delta)
    {
        if (!playerInWater)
            return;


        if (elapsed > 1f)
        {

            player.AddHealth(-10);

            elapsed = 0f;

            var steamScene = steam.Instance() as Steam;

            steamScene.Position = player.Position + player.Scale * 16;

            GetTree().Root.AddChild(steamScene);

        }
        elapsed += delta;
    }
}

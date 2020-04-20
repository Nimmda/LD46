using Godot;
using System;

public class DropHandler : Node2D
{
    [Export] public Vector2 spawnTimeRange = new Vector2(1f, 2f);

    private float elapsed = 1f;
    private float nextSpawn = 0f;

    private PackedScene packed = null;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        nextSpawn = (float)GD.RandRange(spawnTimeRange.x, spawnTimeRange.y);

        packed = ResourceLoader.Load<PackedScene>("res://Scenes/Drop.tscn");
    }


    public override void _Process(float delta)
    {
        if (elapsed > nextSpawn)
        {
            var drop = packed.Instance();
            AddChild(drop);
            elapsed = 0f;
            nextSpawn = (float)GD.RandRange(spawnTimeRange.x, spawnTimeRange.y);

        }

        elapsed += delta;
    }
}

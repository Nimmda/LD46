using Godot;
using System;

public class Drop : Area2D
{
    [Export] public int dropGravity = 120;
    [Export] public int damage = 20;
    [Export] public float destroyCoordY = 300f;

    public override void _Ready()
    {

    }

    public void OnDropBodyEntered(Node body)
    {
        if (!(body is Player))
            return;

        (body as Player).AddHealth(-damage);
        Disconnect("body_entered", this, "OnDropBodyEntered");
        QueueFree();
    }

    public override void _PhysicsProcess(float delta)
    {
        var pos = Position;
        pos.y += dropGravity * delta;
        Position = pos;

        if (Position.y > destroyCoordY)
        {
            Disconnect("body_entered", this, "OnDropBodyEntered");
            QueueFree();
        }
    }
}

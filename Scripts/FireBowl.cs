using Godot;
using System;

public class FireBowl : Area2D
{
    private AnimatedSprite sprite = null;
    private Player player = null;
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public void OnTorchBodyEntered(Node body)
    {
        if (!(body is Player))
            return;

        this.Modulate = new Color(0.69f, 0.59f, 0.23f, 1);
        this.player = (body as Player);
    }

    public void OnTorchBodyExited(Node body)
    {
        if (!(body is Player))
            return;

        this.Modulate = new Color(1, 1, 1, 1);
        player = null;

    }

    public override void _Process(float delta)
    {
        if (player == null)
            return;

        player.AddHealth(1);
    }


}

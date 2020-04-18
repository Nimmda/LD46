using Godot;
using System;

public class Health : AnimatedSprite
{
    private Player player = null;


    public override void _Ready()
    {
        player = GetNode<Player>("/root/Game/Player");
    }

    public override void _Process(float delta)
    {
        var health = player.healtPoints;
        if (health > 80)
            Frame = 0;

        if (health > 60 && health <= 80)
            Frame = 1;

        if (health > 40 && health <= 60)
            Frame = 2;

        if (health > 20 && health <= 40)
            Frame = 3;

        if (health > 0 && health <= 20)
            Frame = 4;

        if (health < 1)
            Visible = false;
        else
            Visible = true;
    }
}

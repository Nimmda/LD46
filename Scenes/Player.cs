using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public int RunSpeed = 100;
    [Export] public int JumpSpeed = -400;
    [Export] public int Gravity = 1200;
    [Export] public int healtPoints = 100;
    private AnimatedSprite sprite = null;

    Vector2 velocity = new Vector2();
    bool jumping = false;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("Sprite");
    }

    public void GetInput()
    {
        velocity.x = 0;
        bool right = Input.IsActionPressed("ui_right");
        bool left = Input.IsActionPressed("ui_left");
        bool jump = Input.IsActionPressed("ui_up");

        if (jump && IsOnFloor())
        {
            jumping = true;
            velocity.y = JumpSpeed;
        }

        if (right)
        {
            velocity.x += RunSpeed;
            sprite.FlipH = false;
        }

        if (left)
        {
            velocity.x -= RunSpeed;
            sprite.FlipH = true;
        }

        if (Mathf.Abs(velocity.x) > 0.01f && !jumping)
        {
            sprite.Animation = "move";

        }
        else
        {
            sprite.Animation = "idle";

        }
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        velocity.y += Gravity * delta;
        if (jumping && IsOnFloor())
            jumping = false;
        velocity = MoveAndSlide(velocity, new Vector2(0, -1));
    }
}

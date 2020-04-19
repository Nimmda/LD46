using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public int RunSpeed = 100;
    [Export] public int JumpSpeed = -400;
    [Export] public int Gravity = 1200;
    [Export] public int healtPoints = 100;
    [Export] public int healthDecrease = 5;
    private AnimatedSprite sprite = null;
    private bool isDecreasingHealth = false;
    private string currentAnim = "idle";
    private Camera2D camera = null;

    Vector2 velocity = new Vector2();
    bool jumping = false;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("Sprite");
        camera = GetNode<Camera2D>("Camera2D");

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
            sprite.FlipH = true;
        }

        if (left)
        {
            velocity.x -= RunSpeed;
            sprite.FlipH = false;
        }

        if (left || right)
        {
            currentAnim = "move";
        }
        else
            currentAnim = "idle";

        if (jumping)
        {
            currentAnim = "jumping";
        }

        if (sprite.Animation != currentAnim)
            sprite.Animation = currentAnim;
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        velocity.y += Gravity * delta;

        velocity = MoveAndSlide(velocity, new Vector2(0, -1));

        if (jumping && IsOnFloor())
        {
            jumping = false;
        }


        if (!isDecreasingHealth)
        {
            isDecreasingHealth = true;
            DecreaseHealth();
        }

        SetScale();
    }

    private async void DecreaseHealth()
    {
        var timer = new Timer();
        timer.Autostart = false;
        timer.WaitTime = 1f;

        GetTree().Root.AddChild(timer);
        timer.Start();

        await ToSignal(timer, "timeout");
        // decrease health after 1 second
        healtPoints -= healthDecrease;
        isDecreasingHealth = false;
    }

    public void AddHealth(int amount)
    {
        healtPoints = Mathf.Min(amount + healtPoints, 100);
    }

    private void SetScale()
    {
        var scaling = (healtPoints / 100.0f);
        Scale = new Vector2(scaling, scaling) + new Vector2(0.5f, 0.5f);

        // set camera
        //camera.Zoom = scaling * new Vector2(0.4f, 0.4f);

    }

}

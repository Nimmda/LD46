using Godot;
using System.Collections.Generic;

public class Player : KinematicBody2D
{
    [Export] public int RunSpeed = 100;
    [Export] public int JumpSpeed = -400;
    [Export] public int Gravity = 1200;
    [Export] public int WaterGravity = 200;
    [Export] public int healtPoints = 100;
    [Export] public int healthDecrease = 5;
    private bool inWater = false;
    private AnimatedSprite sprite = null;
    private bool isDecreasingHealth = false;
    private string currentAnim = "idle";
    private Camera2D camera = null;
    public bool dead = false;

    private string[] effectNames = new string[] { "jump", "idle", "die", "absorb", "damage" };

    // Sound
    private Node nodeEffects = null;
    private Dictionary<string, List<AudioStreamPlayer2D>> effects = new Dictionary<string, List<AudioStreamPlayer2D>>();

    private bool isDefeatSet = false;

    Vector2 velocity = new Vector2();
    bool jumping = false;
    private bool playerProcessing = true;
    private float elapsed;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("Sprite");
        camera = GetNode<Camera2D>("Camera2D");
        nodeEffects = GetNode<Node>("Effects");

        // Get all Sounds

        foreach (AudioStreamPlayer2D audio in nodeEffects.GetChildren())
        {
            foreach (var name in effectNames)
            {
                if (audio.Name.BeginsWith(name))
                {
                    if (!effects.ContainsKey(name))
                        effects.Add(name, new List<AudioStreamPlayer2D>());

                    effects[name].Add(audio);
                }

            }

        }
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
            PlaySound("jump");
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

    }

    public override void _PhysicsProcess(float delta)
    {
        if (dead)
            return;


        GetInput();

        var currentGravity = Gravity;

        if (inWater)
            currentGravity = WaterGravity;


        velocity.y += currentGravity * delta;

        velocity = MoveAndSlide(velocity, new Vector2(0, -1));

        if (jumping && IsOnFloor())
        {
            jumping = false;
        }

    }

    public override void _Process(float delta)
    {
        if (healtPoints < 0f)
        {
            if (!dead)
            {
                PlaySound("die");
                sprite.Animation = "death";
            }
            dead = true;

            if (sprite.Frame == sprite.Frames.GetFrameCount("death") - 1)
            {
                sprite.Stop();

                if (!isDefeatSet)
                {
                    GetNode<Sprite>("GameOver").Visible = true;
                    GetNode<AudioStreamPlayer>("../AudioStreamPlayerGameMusic").Stop();
                    GetNode<AudioStreamPlayer2D>("../AudioStreamPlayer2D").Play();
                    isDefeatSet = true;
                    elapsed = 0f;
                }
                else
                {
                    if (elapsed > 2f)
                    {
                        GetTree().Root.GetNode<Game>("Game").GameOver();
                    }
                    elapsed += delta;
                }


            }
            else
            {             // set scaling with frame id
                var scaling = 1.2f * (1 + sprite.Frame);
                Scale = new Vector2(scaling, scaling);
                var pos = new Vector2(Position.x, Position.y - (sprite.Frame));
                Position = pos;
            }

        }
        else
        {
            SetScale();

            // set animation
            if (sprite.Animation != currentAnim)
            {
                PlaySound("idle");
                sprite.Animation = currentAnim;
            }



            if (!isDecreasingHealth)
            {
                isDecreasingHealth = true;
                DecreaseHealth();
            }

        }

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
        if (dead)
            return;

        if (amount < 0)
        {
            PlaySound("damage");
        }
        else
        {
            PlaySound("absorb");
        }


        healtPoints = Mathf.Min(amount + healtPoints, 100);
    }

    private void PlaySound(string name)
    {
        List<AudioStreamPlayer2D> sounds = null;
        if (!effects.TryGetValue(name, out sounds))
            return;

        var id = (int)GD.RandRange(0, sounds.Count);

        sounds[id].Play();


    }

    private void SetScale()
    {
        var scaling = (healtPoints / 100.0f);
        Scale = new Vector2(scaling, scaling) + new Vector2(0.5f, 0.5f);

        // set camera
        //camera.Zoom = scaling * new Vector2(0.4f, 0.4f);

    }

    public void ToggleProcessing()
    {
        playerProcessing = !playerProcessing;

        sprite.Animation = "idle";

        SetProcess(playerProcessing);
        SetProcessInput(playerProcessing);
        SetPhysicsProcess(playerProcessing);

    }

    public void ToggleWaterGravity()
    {
        inWater = !inWater;
    }
}

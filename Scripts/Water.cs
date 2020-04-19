using Godot;
using System;

public class Water : Area2D
{
    private AnimatedSprite steam = null;
    private Sprite stoneEmpty = null;
    private Texture stoneTexture = null;
    public override void _Ready()
    {
        steam = GetNode<AnimatedSprite>("SteamAnimatedSprite");
        stoneEmpty = GetNode<Sprite>("Stone/StoneSprite");
        stoneTexture = ResourceLoader.Load("res://Assets/Original/stoneBlock_16x16.png") as Texture;

        steam.Visible = false;
    }

    private Texture LoadTexture(string path)
    {
        return ResourceLoader.Load(path) as Texture;
    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public void OnTorchBodyEntered(Node body)
    {
        if (!(body is Player))
            return;

        steam.Visible = true;
        steam.Playing = true;
        (body as Player).AddHealth(-20);
        Disconnect("body_entered", this, "OnTorchBodyEntered");
        WaitForAnimationEnded();
    }

    private async void WaitForAnimationEnded()
    {

        var timer = new Timer();
        timer.WaitTime = 1.2f;
        timer.OneShot = true;
        timer.Autostart = true;
        AddChild(timer);

        await ToSignal(timer, "timeout");

        stoneEmpty.Texture = stoneTexture;
        steam.Visible = false;
    }
}

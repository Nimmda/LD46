using Godot;
using System;



public class Torch : Area2D
{
    private AnimatedSprite sprite = null;
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");


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
    public void OnTorchBodyEntered(Player player)
    {
        sprite.Animation = "burnOut";
        player.AddHealth(50);
        Disconnect("body_entered", this, "OnTorchBodyEntered");
    }
}

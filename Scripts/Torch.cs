using Godot;
using System;

public class Torch : Area2D
{

    private AnimatedSprite sprite = null;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }



    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public void OnTorchBodyEntered(Player player)
    {
        sprite.Animation = "burnOut";
        player.AddHealth(20);
    }
}

using Godot;
using System;

public class HintArea : Area2D
{
    [Export] public string Message = "Message Here";
    private HintController hintController = null;
    public override void _Ready()
    {
        hintController = GetNode<HintController>("../CanvasLayer");
    }
    public void OnHintBodyEntered(Node body)
    {
        if (body is Player)
        {
            Disconnect("body_entered", this, "OnHintBodyEntered");
            hintController.ShowHint(1f, Message);
        }
    }
}

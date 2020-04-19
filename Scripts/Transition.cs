using Godot;
using System;


public enum FadeType { In, Out, OutIn }
public class Transition : Node2D
{
    public FadeType FadeType = FadeType.OutIn;

    private AnimationPlayer animationPlayer = null;
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void Fade()
    {

    }

}

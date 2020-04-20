using Godot;
using System;

public class HintController : CanvasLayer
{
    private Game game = null;

    private AnimationPlayer hintAnim = null;
    public override void _Ready()
    {
        game = GetTree().Root.GetNode<Game>("Game");

        hintAnim = GetNode<AnimationPlayer>("Hint");

        ShowHint(0.5f, "IGNIS: WHOOOAA! What a ride! I better back to my cosy fireplace in the kings hall. Let's hope there is no water around. Zsssshhhh!");
    }
    public async void ShowHint(float waittime, string msg)
    {


        var txtLabel = hintAnim.GetChild(0).GetChild<RichTextLabel>(0);
        txtLabel.Text = msg;

        var timer = new Timer();
        timer.WaitTime = waittime;
        timer.Autostart = true;
        AddChild(timer);

        await ToSignal(timer, "timeout");

        game.TogglePauseMode();

        hintAnim.GetNode<Panel>("Panel").Visible = true;
        hintAnim.CurrentAnimation = "TextVisible";
        FadeOutHint();
    }

    public async void FadeOutHint()
    {
        var timer = new Timer();
        timer.WaitTime = hintAnim.CurrentAnimationLength + 2f;
        timer.Autostart = true;
        AddChild(timer);

        await ToSignal(timer, "timeout");


        var panel = hintAnim.GetNode<Panel>("Panel");

        panel.Visible = false;
        panel.GetNode<RichTextLabel>("RichTextLabel").Text = "";
        game.TogglePauseMode();

    }

}

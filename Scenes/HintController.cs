using Godot;
using System;

public class HintController : CanvasLayer
{

    private AnimationPlayer hintAnim = null;
    public override void _Ready()
    {
        hintAnim = GetNode<AnimationPlayer>("Hint");

        ShowHint(hintAnim, "TextVisible", 1.5f);
    }

    public async void ShowHint(AnimationPlayer anim, string animName, float waittime)
    {

        var timer = new Timer();
        timer.WaitTime = waittime;
        timer.Autostart = true;
        AddChild(timer);

        await ToSignal(timer, "timeout");
        hintAnim.GetNode<Panel>("Panel").Visible = true;
        hintAnim.CurrentAnimation = "TextVisible";
        FadeOutHint(anim, anim.CurrentAnimationLength + 1f);
    }

    public async void FadeOutHint(AnimationPlayer anim, float time)
    {
        var timer = new Timer();
        timer.WaitTime = time;
        timer.Autostart = true;
        AddChild(timer);

        await ToSignal(timer, "timeout");

        var timePassed = 0.0f;

        var panel = anim.GetNode<Panel>("Panel");
        var col = panel.Modulate;

        while (timePassed < 1.0f)
        {
            timePassed += GetProcessDeltaTime();


            col.a -= 2.5f * GetProcessDeltaTime();
            panel.Modulate = col;

        }
        panel.Visible = false;


    }

}

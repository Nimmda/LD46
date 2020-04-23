using Godot;
using System;

public class Settings : Control
{
    [Export]
    public bool MasterSound = true;
    [Export] public int MusicVolume = -6;

    [Export] public int EffectsVolume = -6;

    [Export] public string MusicBusName = "Music";
    [Export] public string EffectsBusName = "Effects";

    [Export] public NodePath Effects = null;
    [Export] public NodePath Music = null;

    public override void _Ready()
    {
        GetNode<Slider>(Effects).Value = EffectsVolume;
        GetNode<Slider>(Music).Value = MusicVolume;
    }
    public void OnCheckboxMasterToggled(bool buttonPressed)
    {


        AudioServer.SetBusMute(0, buttonPressed);
    }

    public void OnHSliderMusicValueChanged(float value)
    {
        var id = AudioServer.GetBusIndex(MusicBusName);
        AudioServer.SetBusVolumeDb(id, (int)value);
    }

    public void OnHSliderEffectsValueChanged(float value)
    {
        var id = AudioServer.GetBusIndex(EffectsBusName);
        AudioServer.SetBusVolumeDb(id, (int)value);
    }
    public void OnBackPressed()
    {
        this.Visible = false;
    }
}
using Godot;
using System;

public class Exit : Area2D
{
    public override void _Ready()
    {

    }

    public void OnExitDoorPlayerEntered(Player player)
    {
        // change to next Level
        GD.Print("nextlevel");
        // need to call deferred because you cannot add new area2d's
        // in a scene from a on entered scene signal
        CallDeferred("LoadLevel");
    }

    private void LoadLevel()
    {
        GetTree().Root.GetNode<Game>("Game").LoadNextLevel();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

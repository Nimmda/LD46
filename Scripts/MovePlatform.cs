using Godot;
using System;

public class MovePlatform : StaticBody2D
{


    public void OnMovePlatformBodyEntered(Node body)
    {
        if (!(body is Player))
            return;


        //  CallDeferred("ParentToPlatform", body);

    }

    private void ParentToPlatform(Node node)
    {
        GetTree().Root.GetNode<Game>("Game").RemoveChild(node);

        this.AddChild(node);
        (node as Player).Position = new Vector2(0, 0);

    }

    private void ParentToGame(Node node)
    {
        this.RemoveChild(node);
        GetTree().Root.GetNode<Game>("Game").AddChild(node);
    }

    public void OnMovePlatformBodyExited(Node body)
    {
        if (!(body is Player))
            return;

        //  CallDeferred("ParentToGame", body);
    }


}

using Godot;
using System.Collections.Generic;

public class Game : Node2D
{
    public List<string> Levels = new List<string>();

    private int currentId = -1;
    private Node currentLevel = null;
    public override void _Ready()
    {
        VisualServer.SetDefaultClearColor(new Color(0f, 0f, 0f, 1));

        // add scenes
        Levels.Add("res://Scenes/Level1.tscn");
        Levels.Add("res://Scenes/Level2.tscn");

        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        currentId++;

        if (currentId < Levels.Count)
        {
            var level = ResourceLoader.Load<PackedScene>(Levels[currentId]);

            if (currentLevel != null)
            {
                RemoveChild(currentLevel);
                (currentLevel as Node2D).QueueFree();
                currentLevel = null;
            }
            currentLevel = level.Instance();
            AddChild(currentLevel);
        }

    }
}

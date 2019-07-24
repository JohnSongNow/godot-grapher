using Godot;
using System;

public class Legend : Control
{
    public Label title;
    public VBoxContainer legendFigures;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        title = GetNode("Title") as Label;
        legendFigures = GetNode("LegendFigures") as VBoxContainer;
    }
    
    public void AddLegendFigure(Node icon, string title)
    {
        PackedScene scene = (PackedScene)ResourceLoader.Load("res://addons/godot_grapher/Scenes/Legend/LegendFigure.tscn");
        LegendFigure fig = scene.Instance() as LegendFigure;
        legendFigures.AddChild(fig);

        fig.SetIcon(icon);
        fig.SetTitle(title);

        this.SetName(title + "LegendFigure");
    }

    public void AddLegendFigure(Color color, string title)
    {
        PackedScene scene = (PackedScene)ResourceLoader.Load("res://addons/godot_grapher/Scenes/Legend/LegendFigure.tscn");
        LegendFigure fig = scene.Instance() as LegendFigure;
        legendFigures.AddChild(fig);

        fig.SetIconColour(color);
        fig.SetTitle(title);

        this.SetName(title + "LegendFigure");
    }

    public void Resize(Vector2 size)
    {
        this.SetPosition(new Vector2(size.x + 150, 0));
    }
}

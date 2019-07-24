using Godot;
using System;

public class LegendFigure : HBoxContainer
{
    public Node icon;
    public Label title;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        title = GetNode("Title") as Label;
        icon = GetNode("Icon");
    }

    public void SetTitle(string title)
    {
        this.title.Text = title;
    }

    public void SetIcon(Node icon)
    {
        this.icon.QueueFree();
        this.icon = icon;
        AddChild(icon);
    }

    /// <summary>
    /// Sets the icon to a coloured panel instaed for defult
    /// </summary>
    /// <param name="color"></param>
    public void SetIconColour(Color color)
    {
        StyleBoxFlat box = (icon.Get("custom_styles/panel") as StyleBoxFlat).Duplicate() as StyleBoxFlat;
        box.SetBgColor(color);
        icon.Set("custom_styles/panel", box);
    }
}

using Godot;
using System;

public class Notch2D : Control
{
    public Label label;
    public Panel notch;

    public override void _Ready()
    {
        label = GetNode("Label") as Label;
        notch = GetNode("Notch") as Panel;
    }

    /// <summary>
    /// Sets the colour of the notch
    /// </summary>
    public void SetColour(Color colour)
    {
        
    }

    public void Resize(Vector2 size)
    {

    }

    public void SetYAxis()
    {
        notch.SetSize(new Vector2(40, 8));
        notch.SetPosition(new Vector2(-20, 0));
        label.SetPosition(new Vector2(-label.GetSize().x, notch.GetPosition().y - label.GetSize().y / 2));
    }
}

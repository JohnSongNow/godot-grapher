using Godot;
using System;

public class Axis2D : Control
{
    public enum DIR { X, Y };

    public DIR dir;
    public Vector2 scale;

    public Label minScale;
    public Label maxScale;
    public Label title;
    public Line2D axis;
    public Control ticks;

    public override void _Ready()
    {
        title = GetNode("Label") as Label;
        axis = GetNode("Axis") as Line2D;
        minScale = GetNode("Min") as Label;
        maxScale = GetNode("Max") as Label;
    }

    /// <summary>
    /// Resizes the axis line and the label position
    /// </summary>
    public void Resize(Vector2 size)
    {
        title.Hide();
        title.Show();

        float offset = 80;
        
        // resize the axis size
        if (dir == DIR.X)
        {
            axis.SetPointPosition(0, new Vector2(0 - axis.GetWidth() / 2f, size.y));
            axis.SetPointPosition(1, new Vector2(size.x, size.y));

            title.SetPosition(new Vector2(size.x / 2 - title.GetSize().x / 2, size.y + offset / 2));
        }
        else
        {
            axis.SetPointPosition(0, new Vector2(0, size.y));
            axis.SetPointPosition(1, new Vector2(0, 0));

            title.SetPosition(new Vector2(-offset - title.GetSize().x, size.y / 2 - title.GetSize().y / 2));
        }

        minScale.Hide();
        minScale.Show();
        maxScale.Hide();
        maxScale.Show();

        // resize the min/max scale
        if (dir == DIR.X)
        {
            minScale.SetPosition(new Vector2(0, size.y));
            maxScale.SetPosition(new Vector2(size.x, size.y));
            
        }
        else
        {
            minScale.SetPosition(new Vector2(-minScale.GetSize().x - 20, size.y - minScale.GetSize().y));
            maxScale.SetPosition(new Vector2(-maxScale.GetSize().x - 20, 0));
        }
        // resize ticks
    }

    public void ResetScale()
    {
        minScale.Text = scale.x.ToString();
        maxScale.Text = scale.y.ToString();
    }

    public void SetScale(Vector2 scale)
    {
        this.scale = new Vector2(scale);
        ResetScale();
    }
}

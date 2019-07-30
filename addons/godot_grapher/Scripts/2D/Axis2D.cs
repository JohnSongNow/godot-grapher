using Godot;
using System;

public class Axis2D : Control
{
    public enum DIR { X, Y };

    // options
    public Boolean logScale = false;
    public Boolean showScales = true;

    // values
    public DIR dir;
    public Vector2 scale;

    public Label minScale;
    public Label maxScale;
    public Label title;
    public Line2D axis;
    public Control notches;

    public override void _Ready()
    {
        title = GetNode("Label") as Label;
        axis = GetNode("Axis") as Line2D;
        minScale = GetNode("Min") as Label;
        maxScale = GetNode("Max") as Label;
        notches = GetNode("Notches") as Control;
    }

    /// <summary>
    /// Resizes the axis line and the label position
    /// </summary>
    public void Resize(Vector2 size)
    {
        title.Hide();
        title.Show();

        float offset = 80;

        this.SetPosition(new Vector2(0, size.y));

        // resize the axis size
        if (dir == DIR.X)
        {
            axis.SetPointPosition(0, new Vector2(0 - axis.GetWidth() / 2f, 0));
            axis.SetPointPosition(1, new Vector2(size.x, 0));

            title.SetPosition(new Vector2(size.x / 2 - title.GetSize().x / 2, offset / 2));
        }
        else
        {
            axis.SetPointPosition(0, new Vector2(0, -size.y));
            axis.SetPointPosition(1, new Vector2(0, 0));

            title.SetPosition(new Vector2(-offset - title.GetSize().x, size.y / 2 - title.GetSize().y / 2));
        }

        if (showScales) {
            minScale.Hide();
            minScale.Show();
            maxScale.Hide();
            maxScale.Show();
        }
        // resize the min/max scale
        if (dir == DIR.X)
        {
            minScale.SetPosition(new Vector2(0, 0));
            maxScale.SetPosition(new Vector2(size.x, 0));

        }
        else
        {
            minScale.SetPosition(new Vector2(-minScale.GetSize().x - 20, 0));
            maxScale.SetPosition(new Vector2(-maxScale.GetSize().x - 20, -size.y - minScale.GetSize().y));
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

    /// <summary>
    /// Adds a notch to the axis
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Notch2D AddNotch(float value)
    {
        // checking out of bounds
        if (scale.y >= value && value >= scale.x)
        {

        }

        PackedScene scene = (PackedScene)ResourceLoader.Load("res://addons/godot_grapher/Scenes/2D/Notch2D.tscn");
        Notch2D notch = scene.Instance() as Notch2D;
        notches.AddChild(notch);
        /*
        StyleBoxFlat box = (icon.Get("custom_styles/panel") as StyleBoxFlat).Duplicate() as StyleBoxFlat;
        box.SetBgColor(color);
        icon.Set("custom_styles/panel", box);
        */
        notch.label.SetText(value.ToString());

        // adjust notch pos and size here
        if (dir == DIR.X)
        {
            if (!logScale)
            {
                notch.SetPosition(new Vector2(((value - scale.x) / (scale.y - scale.x)) * (this.axis.GetPointPosition(1).x - this.axis.GetPointPosition(0).x), 0));
            }
            else
            {
                notch.SetPosition(new Vector2(((float)Math.Log10(value - scale.x) / (float)Math.Log10(scale.y - scale.x)) * (this.axis.GetPointPosition(1).x - this.axis.GetPointPosition(0).x), 0));
            }
        }
        else
        {
            notch.SetYAxis();
            if (!logScale)
            {

            }
            else
            {
                notch.SetPosition(new Vector2(notch.GetPosition().x, ((float)Math.Log10(value - scale.x) / (float)Math.Log10(scale.y - scale.x)) * (this.axis.GetPointPosition(0).y - this.axis.GetPointPosition(1).y)));
            }
        }

        return notch;
    }

    public void SetScaleVisible(bool visible)
    {
        showScales = visible;
        minScale.SetVisible(visible);
        maxScale.SetVisible(visible);
    }
}

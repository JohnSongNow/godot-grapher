using Godot;
using System;
using System.Collections.Generic;

public class Graph2D : Control
{
    // signals
    [Signal]
    public delegate void OnValuesChanged(List<Vector2> values);

    // options
    public Boolean autoScale = true;

    // state information
    public List<Vector2> values;
    public Vector2 min;
    public Vector2 max;

    // graphical you can remove set the visiblity of the control node if you
    // dont need to graphical elements
    public Axis2D xAxis;
    public Axis2D yAxis;
    public Label title;
    public Control points;
    public Legend legend;

    public override void _Ready()
    {
        values = new List<Vector2>();
        min = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        max = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

        xAxis = GetNode("XAxis2D") as Axis2D;
        yAxis = GetNode("YAxis2D") as Axis2D;
        title = GetNode("Title") as Label;
        points = GetNode("Points") as Control;
        legend = GetNode("Legend") as Legend;

        xAxis.dir = Axis2D.DIR.X;
        yAxis.dir = Axis2D.DIR.Y;

        Resize();
        Connect("resized", this, nameof(Resize));
    }

    /// <summary>
    /// Resizes the label, axis based on the current size
    /// </summary>
    public void Resize()
    {
        title.SetPosition(new Vector2(this.GetSize().x / 2 - title.GetSize().x / 2, -title.GetSize().y));

        xAxis.Resize(this.GetSize());
        yAxis.Resize(this.GetSize());

        legend.Resize(this.GetSize());
    }

    /// <summary>
    /// Adds a list of points to the points array
    /// </summary>
    public void ClearPoints()
    {
        values.Clear();
        min = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        max = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

        EmitSignal(nameof(OnValuesChanged), values);
    }

    /// <summary>
    /// Adds a list of points to the points add
    /// </summary>
    /// <param name="newPoints"></param>
    public List<Control> AddPoints(List<Vector2> newPoints)
    {
        List<Control> plots = new List<Control>();

        values.AddRange(newPoints);

        // getting boundaries
        foreach (Vector2 point in newPoints)
        {

            if (point.x < min.x)
            {
                min.x = point.x;
            }
            else if (point.x > max.x)
            {
                max.x = point.x;
            }

            if (point.y < min.y)
            {
                min.y = point.y;
            }
            else if (point.y > max.y)
            {
                max.y = point.y;
            }
        }

        if (autoScale)
        {
            // update scale for axises
            xAxis.scale = new Vector2(min.x, max.x);
            yAxis.scale = new Vector2(min.y, max.y);
            xAxis.ResetScale();
            yAxis.ResetScale();
        }

        if (yAxis.logScale)
        {
            // plot the after the scales have been reset
            Vector2 diff = new Vector2(xAxis.scale.y - xAxis.scale.x, (float)Math.Log10(yAxis.scale.y - yAxis.scale.x));
            foreach (Vector2 pnt in newPoints)
            {
                PackedScene scene = (PackedScene)ResourceLoader.Load("res://addons/godot_grapher/Scenes/2D/Point.tscn");
                Panel point = scene.Instance() as Panel;
                this.points.AddChild(point);
                plots.Add(point);

            /*
            // if points are not inbound of the scales dont draw them
            if (pnt.x <= xAxis.scale.y && pnt.x >= xAxis.scale.x && pnt.y <= yAxis.scale.y && pnt.y >= yAxis.scale.y)
            {
            */
                //GD.Print(pnt.ToString());
                point.SetPosition(GetSize() * (new Vector2(pnt.x - xAxis.scale.x, (float)Math.Log10(pnt.y) - (float)Math.Log10(yAxis.scale.x)) / diff));
                point.SetPosition(new Vector2(point.GetPosition().x, GetSize().y - point.GetPosition().y));
            //}
            }
        }
        else
        {
            Vector2 diff = new Vector2(xAxis.scale.y - xAxis.scale.x, yAxis.scale.y - yAxis.scale.x);
            foreach (Vector2 pnt in newPoints)
            {
                PackedScene scene = (PackedScene)ResourceLoader.Load("res://addons/godot_grapher/Scenes/2D/Point.tscn");
                Panel point = scene.Instance() as Panel;
                this.points.AddChild(point);
                plots.Add(point);

                // if points are not inbound of the scales dont draw them
                if (pnt.x <= xAxis.scale.y && pnt.x >= xAxis.scale.x && pnt.y <= yAxis.scale.y && pnt.y >= yAxis.scale.y)
                {
                    point.SetPosition(GetSize() * ((pnt - min) / diff));
                    point.SetPosition(new Vector2(point.GetPosition().x, GetSize().y - point.GetPosition().y));
                }
            }
        }

        EmitSignal(nameof(OnValuesChanged));

        return plots;
    }

    /// <summary>
    /// Sets the colours of the control points to that colour,
    /// warning shares the resources and does not make a new one
    /// </summary>
    /// <param name=""></param>
    public void SetColour(List<Control> points, Color colour)
    {
        StyleBoxFlat box = (points[0].Get("custom_styles/panel") as StyleBoxFlat).Duplicate() as StyleBoxFlat;
        box.SetBgColor(colour);

        foreach (Control point in points)
        {
            point.Set("custom_styles/panel", box);
        }
    }

    /// <summary>
    /// Sets the visilbity of a point list
    /// </summary>
    /// <param name="points"></param>
    /// <param name="visible"></param>
    public void SetPointsVisible(List<Control> points, bool visible)
    {
        foreach (Control point in points)
        {
            point.SetVisible(visible);
        }
    }


    public void SetAxisScale(Vector2 xAxisScale, Vector2 yAxisScale)
    {
        xAxis.scale = xAxisScale;
        yAxis.scale = yAxisScale;
        xAxis.ResetScale();
        yAxis.ResetScale();

        Resize();
    }
}

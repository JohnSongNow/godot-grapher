using Godot;
using System;

public class Main : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // getting root
        Graph2D graph = GetNode("Graph2D") as Graph2D;
        graph.autoScale = false;
        graph.logScale = true;
        graph.SetAxisScale(new Vector2(1970, 2020), new Vector2((float)Math.Pow(10, -1), (float)Math.Pow(10, 8)));

        List<string> cores = FileToString("Example/Data/microprocessor-trend-data-master/cores.txt");
        List<string> frequency = FileToString("Example/Data/microprocessor-trend-data-master/frequency.txt");
        List<string> specint = FileToString("Example/Data/microprocessor-trend-data-master/specint.txt");
        List<string> transistors = FileToString("Example/Data/microprocessor-trend-data-master/transistors.txt");
        List<string> watts = FileToString("Example/Data/microprocessor-trend-data-master/watts.txt");

        List<Control> coreControls = graph.AddPoints(StringToPoints(cores));
        List<Control> frequencyControls = graph.AddPoints(StringToPoints(frequency));
        List<Control> transistorControls = graph.AddPoints(StringToPoints(transistors));
        List<Control> wattControls = graph.AddPoints(StringToPoints(watts));
        graph.SetColour(coreControls, Constants.Colours[0]);
        graph.SetColour(frequencyControls, Constants.Colours[1]);
        graph.SetColour(transistorControls, Constants.Colours[2]);
        graph.SetColour(wattControls, Constants.Colours[3]);
        graph.xAxis.title.Text = "Year";
        graph.yAxis.title.Text = "Intensity";
        graph.Resize();
    }

    public List<String> FileToString(string path)
    {
        List<string> result = new List<string>();
        int counter = 0;
        string line;

        // Read the file and display it line by line.  
        System.IO.StreamReader file =
            new System.IO.StreamReader(path);
        while ((line = file.ReadLine()) != null)
        {
            result.Add(line);
            counter++;
        }

        file.Close(); ;

        return result;
    }

    public List<Vector2> StringToPoints(List<string> items)
    {

        List<Vector2> result = new List<Vector2>();

        foreach (String point in items)
        {
            if (point != "####" && point != "")
            {
                result.Add(new Vector2(float.Parse(point.Split(" ")[0]), float.Parse(point.Split(" ")[1])));
            }
        }

        return result;
    }
}

# Godot-Grapher
An addon for Godot 3 in C# used to generate simple graphs.

# Introduction
Simple add-on for Godot 3 in the C# used to generate simple graphs. The add-on creates 2D graphs using control and polygon nodes. The graphs created are scatterplots and supports both normal and logarithmic scales. The tool is currently very primitive since it was made to create infographics. Due to it also being a C# tool only and Godot not having great C# plugin support as of (**3.1**) signals won't work in the editor tabs and must be connected via scripting.

# Setup
The following are required to use this plugin.
- Mono (**4.5+**)
- Godot C# (**3.0+**)

To install drag the **godot-grapher** folder inside your **addons** folder inside the root of your godot project. Once you have moved the plugin into the correct folder the plugin should be inside the plugin options in your project settings. Enable the plugin and the plugin should be installed.

![alt text](https://github.com/JohnSongNow/godot-grapher/blob/master/example/images/setup.png "Enable Plugin Here")

A more in-depth guide for installing Godot plugins can be found [here](https://docs.godotengine.org/en/3.1/tutorials/plugins/editor/import_plugins.html) on their offical website.

# Usage

To create a graph either instance the Graph2D scene, or create a Graph2D node. If you choose to create the Graph2D node you will have to individually add the axis, legend and title nodes to the root graph node. The axis, legend and title nodes are changeable within the editor, however to put points in you will need to instance a Graph2D object while scripting.

The scripting for a Graph2D object involves finding in in the scene.
```
Graph2D graph = root.GetNode("Graph2D") as Graph2D;
graph.autoScale = false;
graph.logScale = true;
# Not needed with autoscale on
graph.SetAxisScale(new Vector2(1970, 2020), new Vector2((float)Math.Pow(10, -1), (float)Math.Pow(10, 8))); 
```

Once you have the Graph2D object adding points or legendfigures can be done as below.
```
List<Control> coreControls = graph.AddPoints(StringToPoints(cores));
graph.legend.AddLegendFigure(new Color("1,1,1,1"), "Cores");
graph.Resize();
```

# Example
Below is an example using data of historic CPU trends found [here](https://github.com/karlrupp/microprocessor-trend-data/tree/master/42yrs);
![alt text](https://github.com/JohnSongNow/godot-grapher/blob/master/example/images/graph_example.png "2D Scatterplot Example")

This library is used in tandum with [Godot Video Tools](https://github.com/JohnSongNow/godot-video-tools) to create visuals and demostrations.

## License
This uses the MIT license.

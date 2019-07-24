tool
extends EditorPlugin

func _enter_tree():
    # Initialization of the plugin goes here
	var script = ResourceLoader.load("addons/godot_grapher/Scripts/2D/Graph2D.cs") as Script
	add_custom_type("Graph2D", "Control", script, preload("icon.png"))

func _exit_tree():
    # Clean-up of the plugin goes here
	remove_custom_type("Graph2D")
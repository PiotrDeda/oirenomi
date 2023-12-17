using System.Collections.Generic;
using System.Linq;
using ReactiveUI;

namespace Oirenomi.Models;

public class ProjectData : ReactiveObject
{
	Scene? _selectedScene;
	GameObject? _selectedGameObject;

	public ProjectData()
	{
		LoadSampleData(); // TODO: Debug only, remove when implemented
	}

	public string? ProjectPath { get; set; }
	public string? ProjectName { get; set; }
	public List<Scene> Scenes { get; set; } = new();

	public Scene? SelectedScene
	{
		get => _selectedScene;
		set => this.RaiseAndSetIfChanged(ref _selectedScene, value);
	}

	public GameObject? SelectedGameObject
	{
		get => _selectedGameObject;
		set => _selectedGameObject = this.RaiseAndSetIfChanged(ref _selectedGameObject, value);
	}

	public void SetProjectPathAndName(string? projectPath)
	{
		ProjectPath = projectPath;
		ProjectName = ProjectPath?.Split('\\').Last().Split('.').First();
	}

	public bool BuildProject()
	{
		if (ProjectPath is null || ProjectName is null)
			return false;
		ProjectBuilder.Build(ProjectPath, ProjectName);
		return true;
	}

	public bool LoadProject()
	{
		if (ProjectPath is null || ProjectName is null)
			return false;

		BuildProject();

		return true;
	}

	public void LoadSampleData()
	{
		if (Scenes.Count > 0)
			return;

		// = Scenes =
		Scenes = new() { new("Game Example"), new("Many Objects"), new("Empty") };

		// = Cameras =
		// == Scene 0 ==
		Scenes[0].Cameras.Add(new("Camera", "Rokuro.Graphics.Camera"));
		Scenes[0].Cameras.Add(new("UICamera", "Rokuro.Graphics.UICamera"));
		// == Scene 1 ==
		foreach (int i in Enumerable.Range(0, 100))
			Scenes[1].Cameras.Add(new($"Camera{i}", "Rokuro.Graphics.Camera"));

		// = GameObjects =
		// == Scene 0 ==
		Scenes[0].GameObjects.Add(new("Player", "tiles/player", "Rokuro.Player", 0, 0));
		Scenes[0].GameObjects[0].CustomProperties.Add(new("Health", "10"));
		Scenes[0].GameObjects[0].CustomProperties.Add(new("Damage", "1"));
		Scenes[0].GameObjects.Add(new("Enemy", "tiles/enemy", "Rokuro.Enemies.Enemy", 0, 0));
		Scenes[0].GameObjects.Add(new("Item", "tiles/item", "Rokuro.Items.Item", 0, 0));
		// == Scene 1 ==
		foreach (int i in Enumerable.Range(0, 100))
			Scenes[1].GameObjects.Add(new($"GameObject{i}", "GameObject", "Rokuro.GameObject", 0, 0));
		foreach (int i in Enumerable.Range(0, 100))
			Scenes[1].GameObjects[0].CustomProperties.Add(new($"Property{i}", $"Value{i}"));
	}
}

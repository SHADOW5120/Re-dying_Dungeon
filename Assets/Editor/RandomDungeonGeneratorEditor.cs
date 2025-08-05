using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbtractDungeonGenerator), true)]

public class RandomDungeonGeneratorEditor : Editor
{
    AbtractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbtractDungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Button"))
        {
            generator.GenerateDungeon();
        }
    }
}

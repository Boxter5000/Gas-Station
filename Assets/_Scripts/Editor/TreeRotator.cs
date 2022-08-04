using UnityEngine;
using UnityEditor;

public class TreeRotator : EditorWindow
{
    public TerrainData terrainData;
    
    
    void OnGUI()
    {
        GUILayout.Label ("Tree Rotator", EditorStyles.boldLabel);
        terrainData = (TerrainData) EditorGUILayout.ObjectField(terrainData, typeof(TerrainData), true);

        if (GUILayout.Button("fixTrees"))
        {
            Rotate();
        }
    }
    
    [MenuItem("Trees/rotate")]
    public static void RotateTrees()
    {
        GetWindow(typeof(TreeRotator));
    }

    public void Rotate()
    {
        for (var index = 0; index < terrainData.treeInstances.Length; index++)
        {
            terrainData.treeInstances[index].heightScale = Random.Range(0f, 360f);
        }
    }
}

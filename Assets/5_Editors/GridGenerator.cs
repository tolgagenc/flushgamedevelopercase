using UnityEditor;
using UnityEngine;

public class GridGenerator : EditorWindow
{
    private int gridSizeX = 10;
    private int gridSizeY = 10;
    private float cellSize = 1f;
    private GameObject cellObject;

    [MenuItem("Window/Grid Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(GridGenerator), false, "Grid Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);

        gridSizeX = EditorGUILayout.IntField("Grid Size X", gridSizeX);
        gridSizeY = EditorGUILayout.IntField("Grid Size Y", gridSizeY);
        cellSize = EditorGUILayout.FloatField("Cell Size", cellSize);
        cellObject = EditorGUILayout.ObjectField("Cell Object", cellObject, typeof(GameObject)) as GameObject;

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }
    }

    private void GenerateGrid()
    {
        GameObject gridParent = new GameObject("Grid");
        
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                
                GameObject cell = cellObject == null ? GameObject.CreatePrimitive(PrimitiveType.Cube) : PrefabUtility.InstantiatePrefab(cellObject) as GameObject;
                cell.transform.position = new Vector3(x * cellSize, 0f, y * cellSize);
                cell.transform.localScale = new Vector3(cellSize, 0.1f, cellSize);
                cell.transform.parent = gridParent.transform;
            }
        }
    }
}
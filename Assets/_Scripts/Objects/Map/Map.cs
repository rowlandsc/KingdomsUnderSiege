using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Map : MonoBehaviour {

    public TerrainCollider Terrain;
    public int GridHeight = 50;
    public int GridWidth = 50;
    public float CellWidth = 1;
    public float CellHeight = 1;

    public float yOffset = 1;

    public bool ShowGrid = false;
    public bool ShowGridDebug = true;

    [HideInInspector]
    public Cell[][] _grid = { };

    public class Cell {
        public Vector3 pos;
        public Map map;
        
    }


    void Start () {
	
	}
	
	void Update () {
        
	}

    void OnDrawGizmos() {
        if (ShowGridDebug && !ShowGrid) {
            Gizmos.color = Color.red;
            for (int i=0; i<_grid.Length; i++) {
                for (int j=0; j<_grid[i].Length; j++) {
                    Cell c = _grid[i][j];
                    if (i < _grid.Length - 1) {
                        Gizmos.DrawLine(c.pos, c.pos + new Vector3(0, 0, CellHeight));
                    }
                    if (j < _grid[i].Length - 1) {
                        Gizmos.DrawLine(c.pos, c.pos + new Vector3(CellWidth, 0, 0));
                    }
                }
            }
        }
    }

    public void UpdateGrid() {
        if (Terrain) {
            Debug.Log("Updating grid...");
            ArrayUtility.Clear<Cell[]>(ref _grid);

            for (int i = 0; i < GridHeight; i++) {
                Cell[] list = { };
                for (int j = 0; j < GridWidth; j++) {
                    Vector3 xzpos = transform.position + new Vector3(CellWidth * j, 100, CellHeight * i);
                    Ray ray = new Ray(xzpos, Vector3.down);
                    RaycastHit hit;
                    Terrain.Raycast(ray, out hit, 200);

                    Cell c = new Cell();
                    c.map = this;
                    c.pos = hit.point + new Vector3(0, yOffset, 0);
                    ArrayUtility.Add<Cell>(ref list, c);
                }
                ArrayUtility.Add<Cell[]>(ref _grid, list);
            }
            Debug.Log("Done updating grid.");
        }
        else {
            Debug.Log("Can't update grid: no terrain given.");
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Map))]
public class GenerateGrid : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Map map = (Map)target;
        if (GUILayout.Button("Update Grid")) {
            map.UpdateGrid();
        }
    }
}
#endif
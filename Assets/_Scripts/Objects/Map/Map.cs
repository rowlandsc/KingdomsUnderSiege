using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Map : MonoBehaviour {

    public static Map Instance = null;
    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

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

#if UNITY_EDITOR

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
                    /*Vector3 xzpos = transform.position + new Vector3(CellWidth * j, 100, CellHeight * i);
                    Ray ray = new Ray(xzpos, Vector3.down);
                    RaycastHit hit;
                    Terrain.Raycast(ray, out hit, 200);*/

                    Cell c = new Cell();
                    c.map = this;
                    c.pos = GetMapPositionAtPoint(CellWidth * j, CellHeight * i) + new Vector3(0, yOffset, 0);
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

#endif

    public Vector3 GetMapPositionAtPoint(float x, float z) {
        float TEST_HEIGHT = 100;

        if (x < Terrain.transform.position.x)
            x = Terrain.transform.position.x;
        if (x > Terrain.transform.position.x + Terrain.terrainData.size.x - .1f)
            x = Terrain.transform.position.x + Terrain.terrainData.size.x - .1f;
        if (z < Terrain.transform.position.z)
            z = Terrain.transform.position.z;
        if (z > Terrain.transform.position.z + Terrain.terrainData.size.z - .1f)
            z = Terrain.transform.position.z + Terrain.terrainData.size.z - .1f;

        if (Terrain) {
            Vector3 xzpos = transform.position + new Vector3(x, TEST_HEIGHT, z);
            Ray ray = new Ray(xzpos, Vector3.down);
            RaycastHit hit;
            Terrain.Raycast(ray, out hit, TEST_HEIGHT * 2);

            return hit.point;
        }

        return new Vector3(x, TEST_HEIGHT, z);
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
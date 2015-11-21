using UnityEngine;
using System.Collections;
using UnityEditor;

[RequireComponent(typeof(Camera))]
public class OverseerCamera : MonoBehaviour {

    public Map GameMap;
    public float MaxZoom = 2;

    private Camera _camera;
    public float _maxHeight = 10;
    public float _minHeight = 10;
    
    void Start() {
        UpdateCameraPositions();
    }

    void Update() {
        if (transform.position.y > _maxHeight) {
            transform.position = new Vector3(transform.position.x, _maxHeight, transform.position.z);
        }
        if (transform.position.y < _minHeight) {
            transform.position = new Vector3(transform.position.x, _minHeight, transform.position.z);
        }
    }

    public void UpdateCameraPositions() {
        _camera = GetComponent<Camera>();
        if (GameMap) {
            float hAngle = _camera.fieldOfView / 2;
            float mapWidth = GameMap.Terrain.GetComponent<Terrain>().terrainData.size.x;
            float mapHeight = GameMap.Terrain.GetComponent<Terrain>().terrainData.size.z;

            _maxHeight = (mapWidth / 2) / Mathf.Tan(Mathf.Deg2Rad * hAngle);
            _minHeight = (mapWidth / MaxZoom) / Mathf.Sign(Mathf.Deg2Rad * hAngle);

            transform.position = new Vector3(GameMap.transform.position.x + mapWidth / 2, GameMap.transform.position.y + _maxHeight, GameMap.transform.position.z + mapHeight / 2); 
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(OverseerCamera))]
public class UpdateOverseerCamera : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        OverseerCamera cam = (OverseerCamera)target;
        if (GUILayout.Button("Update Camera Limits")) {
            cam.UpdateCameraPositions();
        }
    }
}
#endif

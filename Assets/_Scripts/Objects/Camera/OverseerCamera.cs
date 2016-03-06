using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class OverseerCamera : MonoBehaviour {

    public Map GameMap;
    public float MaxZoom = 8;
    public float ZoomSpeed = 8;

    public float Zoom = 1;

    public float ScrollSpeed = 50;

    public Camera Camera;
    public float _maxHeight = 0;
    public float _xMinBound = 0;
    public float _xMaxBound = 0;
    public float _zMinBound = 0;
    public float _zMaxBound = 0;

    NetworkPlayerInput _playerInput;
    
    void Start() {
		GameMap = Map.Instance;
        InitializeCamera();
        UpdateCameraBounds();
        _playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
    }

    void Update() {
        if (Mathf.Abs(InputManager.OverseerZoom) > float.Epsilon) {
            Zoom += (InputManager.OverseerZoom / Mathf.Abs(InputManager.OverseerZoom)) * ZoomSpeed * Time.deltaTime;
            if (Zoom > MaxZoom) Zoom = MaxZoom;
            if (Zoom < 1) Zoom = 1;
        }
        transform.position = new Vector3(transform.position.x, _maxHeight / Zoom, transform.position.z);

        UpdateCameraBounds();

        if (Mathf.Abs(_playerInput.OverseerHorizontalInput) > float.Epsilon) {
            float newX = transform.position.x + _playerInput.OverseerHorizontalInput * ScrollSpeed * Time.deltaTime;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        if (Mathf.Abs(_playerInput.OverseerVerticaleInput) > float.Epsilon) {
            float newZ = transform.position.z + _playerInput.OverseerVerticaleInput * ScrollSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }

        if (transform.position.x > _xMaxBound) {
            transform.position = new Vector3(_xMaxBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x < _xMinBound) {
            transform.position = new Vector3(_xMinBound, transform.position.y, transform.position.z);
        }
        if (transform.position.z > _zMaxBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zMaxBound);
        }
        if (transform.position.z < _zMinBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zMinBound);
        }
    }

    public void InitializeCamera() {
        //Camera = GetComponent<Camera>();
		Debug.Log("Inititalizing overseer camera");
        Zoom = 1;
        if (GameMap) {
            float vFOV = Camera.fieldOfView;
            float radAngle = Camera.fieldOfView * Mathf.Deg2Rad;
            float radHFOV = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * Camera.aspect);
            float hFOV = Mathf.Rad2Deg * radHFOV;
            float hAngle = hFOV / 2;

            float mapWidth = GameMap.TerrainBounds.bounds.size.x;
            float mapHeight = GameMap.TerrainBounds.bounds.size.z;

            _maxHeight = ((mapWidth / 2) / Mathf.Tan(Mathf.Deg2Rad * hAngle));

            transform.position = new Vector3(GameMap.transform.position.x + mapWidth / 2, GameMap.transform.position.y + _maxHeight, GameMap.transform.position.z + mapHeight / 2);
        }
    }

    public void UpdateCameraBounds() {
        if (GameMap) {
            float vFOV = Camera.fieldOfView;
            float radAngle = Camera.fieldOfView * Mathf.Deg2Rad;
            float radHFOV = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * Camera.aspect);
            float hFOV = Mathf.Rad2Deg * radHFOV;
            float vAngle = vFOV / 2;
            float hAngle = hFOV / 2;

            float mapWidth = GameMap.TerrainBounds.bounds.size.x;
            float mapHeight = GameMap.TerrainBounds.bounds.size.z;
            float viewWidth = (transform.position.y * Mathf.Tan(Mathf.Deg2Rad * hAngle) * 2);
            float viewHeight = (transform.position.y * Mathf.Tan(Mathf.Deg2Rad * vAngle) * 2);

            _xMinBound = GameMap.TerrainBounds.transform.position.x + viewWidth / 2;
            _xMaxBound = GameMap.TerrainBounds.transform.position.x + mapWidth - viewWidth / 2;
            _zMinBound = GameMap.TerrainBounds.transform.position.z + viewHeight / 2;
            _zMaxBound = GameMap.TerrainBounds.transform.position.z + mapHeight - viewHeight / 2;

            if (_xMaxBound < _xMinBound) {
                _xMinBound = (_xMinBound + _xMaxBound) / 2;
                _xMaxBound = _xMinBound;
            }
            if (_zMaxBound < _zMinBound) {
                _zMinBound = (_zMinBound + _zMaxBound) / 2;
                _zMaxBound = _zMinBound;
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(OverseerCamera))]
public class UpdateOverseerCamera : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        OverseerCamera cam = (OverseerCamera)target;
        if (GUILayout.Button("Intialize Camera")) {
            cam.InitializeCamera();
            cam.UpdateCameraBounds();
        }
        if (GUILayout.Button("Update Camera Bounds")) {
            cam.UpdateCameraBounds();
        }
    }
}
#endif

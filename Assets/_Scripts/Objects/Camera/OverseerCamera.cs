using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class OverseerCamera : MonoBehaviour {

    public static OverseerCamera Instance = null;

    public Map GameMap;
    public float MaxZoom = 8;
    public float ZoomSpeed = 8;

    public Camera Camera;
    public float _maxHeight = 0;
    public float _xMinBound = 0;
    public float _xMaxBound = 0;
    public float _zMinBound = 0;
    public float _zMaxBound = 0;

    public float Zoom = 1;

    public float ScrollSpeed = 50;

    public bool EdgePanEnabled = false;
    public bool EdgePanOnlyWhenOnWindow = true;
    public float EdgePanVerticalBuffer = 0.1f;
    public float EdgePanHorizontalBuffer = 0.1f;
    public float PanSpeed = 5;
    public Vector3 PanCurrentVelocity = Vector2.zero;
    public float PanFriction = .95f;

    private bool _dragPanning = false;
    private Vector3 _dragPanStartPoint = Vector3.zero;
    private Vector3 _dragPanStartPosition = Vector3.zero;
    private Vector3 _lastDragPoint = Vector3.zero;

    NetworkPlayerInput _playerInput;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Debug.LogWarning("Duplicate OverseerCamera detected");
            Destroy(this);
        }
    }
    
    void Start() {
		GameMap = Map.Instance;
        InitializeCamera();
        UpdateCameraBounds();
        _playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
    }

    void Update() {
        if (Mathf.Abs(InputManager.Instance.OverseerZoom) > float.Epsilon) {
            Zoom += (InputManager.Instance.OverseerZoom / Mathf.Abs(InputManager.Instance.OverseerZoom)) * ZoomSpeed * Time.deltaTime;
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

        if (!_dragPanning) {
            if (InputManager.Instance.CameraDragPanDown) {
                _dragPanning = true;
                _dragPanStartPoint = InputManager.Instance.OverseerClickPositionViewport;
                _dragPanStartPosition = transform.position;
                _lastDragPoint = _dragPanStartPoint;
            }
        }

        if (_dragPanning) {
            _dragPanning = !InputManager.Instance.CameraDragPanUp;

            if (!_dragPanning) {
                Vector3 currentDragPoint = InputManager.Instance.OverseerClickPositionViewport;
                PanCurrentVelocity = _lastDragPoint - currentDragPoint;
                PanCurrentVelocity.x = PanCurrentVelocity.x * Mathf.Abs(transform.position.z * Camera.aspect);
                PanCurrentVelocity.z = PanCurrentVelocity.z * Mathf.Abs(transform.position.z);

                PanCurrentVelocity = PanCurrentVelocity * (.1f + (1 - Zoom / MaxZoom) * .9f);
            }
            else {
                Vector3 currentDragPoint = InputManager.Instance.OverseerClickPositionViewport;

                Vector3 jump = _dragPanStartPoint - currentDragPoint;
                jump.x = jump.x * Mathf.Abs(transform.position.y * Camera.aspect);
                jump.z = jump.z * Mathf.Abs(transform.position.y);
                transform.position = _dragPanStartPosition + jump;
                _lastDragPoint = currentDragPoint;
            }
        }
        else {
            transform.position += new Vector3(PanCurrentVelocity.x, 0, PanCurrentVelocity.z);
            PanCurrentVelocity = PanCurrentVelocity * PanFriction;

            Vector3 pan = InputManager.Instance.CameraEdgePan;
            if (EdgePanOnlyWhenOnWindow) {
                if (Mathf.Abs(pan.x) >= 1) pan.x = 0;
                if (Mathf.Abs(pan.y) >= 1) pan.y = 0;
            }

            transform.position = new Vector3(transform.position.x + PanSpeed * Time.deltaTime * pan.x, transform.position.y, transform.position.z + PanSpeed * Time.deltaTime * pan.z);
        }

        if (transform.position.x > _xMaxBound) {
            transform.position = new Vector3(_xMaxBound, transform.position.y, transform.position.z);
            if (PanCurrentVelocity.x > 0) PanCurrentVelocity.x = 0;
        }
        if (transform.position.x < _xMinBound) {
            transform.position = new Vector3(_xMinBound, transform.position.y, transform.position.z);
            if (PanCurrentVelocity.x < 0) PanCurrentVelocity.x = 0;
        }
        if (transform.position.z > _zMaxBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zMaxBound);
            if (PanCurrentVelocity.z > 0) PanCurrentVelocity.z = 0;
        }
        if (transform.position.z < _zMinBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zMinBound);
            if (PanCurrentVelocity.z < 0) PanCurrentVelocity.z = 0;
        }
    }

    public void InitializeCamera() {
        //Camera = GetComponent<Camera>();
        Zoom = 1;
        if (GameMap) {
            float vFOV = Camera.fieldOfView;
            float radAngle = Camera.fieldOfView * Mathf.Deg2Rad;
            float radHFOV = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * Camera.aspect);
            float hFOV = Mathf.Rad2Deg * radHFOV;
            float hAngle = hFOV / 2;

            float mapWidth = GameMap.TerrainBounds.bounds.size.z;
            float mapHeight = GameMap.TerrainBounds.bounds.size.x;

            _maxHeight = ((mapWidth / 2) / Mathf.Tan(Mathf.Deg2Rad * hAngle));

            transform.position = new Vector3(GameMap.TerrainBounds.transform.position.x, GameMap.TerrainBounds.transform.position.y + _maxHeight, GameMap.TerrainBounds.transform.position.z);
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

            float mapWidth = GameMap.TerrainBounds.bounds.size.z;
            float mapHeight = GameMap.TerrainBounds.bounds.size.x;
            float viewWidth = (transform.position.y * Mathf.Tan(Mathf.Deg2Rad * hAngle) * 2);
            float viewHeight = (transform.position.y * Mathf.Tan(Mathf.Deg2Rad * vAngle) * 2);

            _xMinBound = GameMap.TerrainBounds.transform.position.x - (mapHeight / 2) + viewHeight / 2;
            _xMaxBound = GameMap.TerrainBounds.transform.position.x + (mapHeight / 2) - viewHeight / 2;
            _zMinBound = GameMap.TerrainBounds.transform.position.z - (mapWidth / 2) + viewWidth / 2;
            _zMaxBound = GameMap.TerrainBounds.transform.position.z + (mapWidth / 2) - viewWidth / 2;

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

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class MapCircleDrawer : MonoBehaviour {
    public Map GameMap;
    public float CircleThetaScale = 0.01f;
    public float CircleRadius = 0f;
    public Color CircleColor;
    public Vector3 PositionOffset = new Vector3(0, 0.5f, 0);
    public float LineWidth;
    private LineRenderer _lineRenderer;
    private int _size;

    void Awake() {
        float sizeValue = (2.0f * Mathf.PI) / CircleThetaScale;
        _size = (int)sizeValue;
        _size++;
        _lineRenderer = GetComponent<LineRenderer>();
         _lineRenderer.SetVertexCount(_size);
    }

    public void UpdateCircle() {
        _lineRenderer.SetColors(CircleColor, CircleColor);
        _lineRenderer.SetWidth(LineWidth, LineWidth);
        if (CircleRadius > 0f) {
            Vector3 pos;
            float theta = 0f;
            for (int i = 0; i < _size; i++) {
                theta += (2.0f * Mathf.PI * CircleThetaScale);
                float x = CircleRadius * Mathf.Cos(theta);
                float z = CircleRadius * Mathf.Sin(theta);
                x += gameObject.transform.position.x;
                z += gameObject.transform.position.z;
                pos = GameMap.GetMapPositionAtPoint(x, z) + PositionOffset;
                _lineRenderer.SetPosition(i, pos);
            }
        }
    }

    public bool CircleIsVisible() {
        return _lineRenderer.enabled;
    }

    public void SetCircleVisible(bool v) {
        _lineRenderer.enabled = v;
    }

}

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager Instance = null;

    void Awake() {
        if (!Instance) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public float OverseerZoom {
        get { return Input.GetAxisRaw("OverseerZoom"); }
    }
    public float OverseerScrollHorizontal {
        get {
            float input = Input.GetAxisRaw("OverseerScrollHorizontal");
            return input;

        }
    }
    public float OverseerScrollVertical {
        get {
            return Input.GetAxisRaw("OverseerScrollVertical");
        }
    }

    public Vector3 OverseerClickPositionPixel {
        get {
            return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
        }
    }

    public Vector3 OverseerClickPositionViewport {
        get {
            int screenWidth = OverseerCamera.Instance.Camera.pixelWidth;
            int screenHeight = OverseerCamera.Instance.Camera.pixelHeight;

            if (screenWidth <= 0 || screenHeight <= 0) return new Vector2(0, 0);

            Vector3 mouseScreenPos = Input.mousePosition;
            return new Vector3(mouseScreenPos.y / screenWidth, 1, 1 - mouseScreenPos.x / screenHeight);
        }
    }

    public bool OverseerSelectTower {
        get {
            return Input.GetMouseButton(0);
        }
    }

    public bool OverseerRotateTower {
        get {
            return Input.GetMouseButton(1);
        }
    }

    public Vector3 CameraEdgePan {
        get {
            int screenWidth = OverseerCamera.Instance.Camera.pixelWidth;
            int screenHeight = OverseerCamera.Instance.Camera.pixelHeight;

            if (screenWidth <= 0 || screenHeight <= 0) return new Vector2(0, 0);

            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mousePos = new Vector3(mouseScreenPos.x / screenWidth, 1, mouseScreenPos.y / screenHeight);
            float x = 0;
            float z = 0;

            if (mousePos.x < OverseerCamera.Instance.EdgePanHorizontalBuffer)
                z = (OverseerCamera.Instance.EdgePanHorizontalBuffer - mousePos.x) / OverseerCamera.Instance.EdgePanHorizontalBuffer;
            else if (mousePos.x > 1 - OverseerCamera.Instance.EdgePanHorizontalBuffer)
                z = -1 * (1 - ((1 - mousePos.x) / OverseerCamera.Instance.EdgePanHorizontalBuffer));

            if (mousePos.z < OverseerCamera.Instance.EdgePanVerticalBuffer)
                x = -1 * (OverseerCamera.Instance.EdgePanVerticalBuffer - mousePos.z) / OverseerCamera.Instance.EdgePanVerticalBuffer;
            else if (mousePos.z > 1 - OverseerCamera.Instance.EdgePanVerticalBuffer)
                x = (1 - ((1 - mousePos.z) / OverseerCamera.Instance.EdgePanVerticalBuffer));

            // Return square of distance to edge, scaled positive or negative depending on where the mouse is
            return new Vector3(x * x * (x > 0 ? 1 : -1), 0, z * z * (z > 0 ? 1 : -1));
        }
    }

    public bool CameraDragPanDown {
        get {
            return Input.GetMouseButtonDown(2);
        }
    }

    public bool CameraDragPanUp {
        get {
            return Input.GetMouseButtonUp(2);
        }
    }


    public void MouseOnScreen() {
        OverseerCamera.Instance.EdgePanEnabled = true;
    }

    public void MouseOffScreen() {
        OverseerCamera.Instance.EdgePanEnabled = false;
    }
}

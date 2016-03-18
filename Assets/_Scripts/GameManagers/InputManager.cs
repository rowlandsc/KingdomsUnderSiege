using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager Instance = null;
    public bool EdgePanEnabled = false;
    public float EdgePanVerticalBuffer = 0.1f;
    public float EdgePanHorizontalBuffer = 0.1f;

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

    public Vector3 ClickPositionPixel {
        get {
            return Input.mousePosition;
        }
    }

    public Vector3 ClickPositionWorldspace {
        get {
            return OverseerCamera.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public Vector3 ClickPositionViewport {
        get {
            return OverseerCamera.Instance.Camera.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    public Vector2 CameraEdgePan {
        get {
            int screenWidth = Camera.main.pixelWidth;
            int screenHeight = Camera.main.pixelHeight;

            if (screenWidth <= 0 || screenHeight <= 0) return new Vector2(0, 0);

            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mousePos = new Vector3(mouseScreenPos.x / screenWidth, mouseScreenPos.y / screenHeight, 1);
            float x = 0;
            float y = 0;

            Debug.Log(mousePos);

            if (mousePos.x < EdgePanHorizontalBuffer)
                x = -1 * (EdgePanHorizontalBuffer - mousePos.x) / Instance.EdgePanHorizontalBuffer;
            else if (mousePos.x > 1 - Instance.EdgePanHorizontalBuffer)
                x = (1 - ((1 - mousePos.x) / Instance.EdgePanHorizontalBuffer));

            if (mousePos.y < EdgePanVerticalBuffer)
                y = -1 * (EdgePanVerticalBuffer - mousePos.y) / Instance.EdgePanVerticalBuffer;
            else if (mousePos.y > 1 - Instance.EdgePanVerticalBuffer)
                y = (1 - ((1 - mousePos.y) / Instance.EdgePanVerticalBuffer));

            return new Vector2(x * x * (x > 0 ? 1 : -1), y * y * (y > 0 ? 1 : -1));
        }
    }

    public bool CameraDragPanDown {
        get {
            return Input.GetMouseButtonDown(1);
        }
    }

    public bool CameraDragPanUp {
        get {
            return Input.GetMouseButtonUp(1);
        }
    }


    public void MouseOnScreen() {
        EdgePanEnabled = true;
    }

    public void MouseOffScreen() {
        EdgePanEnabled = false;
    }
}

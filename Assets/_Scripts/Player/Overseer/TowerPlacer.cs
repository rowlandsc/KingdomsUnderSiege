using UnityEngine;
using System.Collections;

public class TowerPlacer : MonoBehaviour {

    public static TowerPlacer Instance;

    public bool TowerPlaceModeOn = false;
    public GameObject TowerToPlace = null;
    public GameObject TestSphere = null;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

	void Start() {
	    
	}
	
	void Update() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            TowerPlaceModeOn = !TowerPlaceModeOn;
        }

        if (TowerPlaceModeOn) {
            if (TestSphere) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = LayerMask.GetMask("Map");
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Camera.main.transform.position.y * 2, mask);
                if (hit.collider != null) {
                    //TestSphere.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
                    TestSphere.transform.position = hit.point;
                }
                Debug.DrawRay(ray.origin, ray.direction * Camera.main.transform.position.y * 2, Color.green);
            }
        }
	}
}

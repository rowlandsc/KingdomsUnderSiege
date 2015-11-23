using UnityEngine;
using System.Collections;

public class TowerPlacer : MonoBehaviour {

    public static TowerPlacer Instance;

    public GameObject TowerPlaceTesterPrefab = null;
    public GameObject TowerPlaceTester = null;
    public bool TowerPlaceModeOn = false;
    public bool TowerPlaceLocationValid = true;
    public Vector3 TowerPlaceLocation;
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

        if (TowerPlaceModeOn && Input.GetMouseButtonUp(0)) {
            TowerPlaceTester.GetComponent<TowerPlacementTester>().PlaceTower();
            TowerPlaceModeOn = false;
        }

        if (TowerPlaceModeOn) {
            //if (TestSphere) {
            if (!TowerPlaceTester) {
                TowerPlaceTester = Instantiate(TowerPlaceTesterPrefab);
                TowerPlaceTester.GetComponent<TowerPlacementTester>().Init(TowerToPlace.GetComponent<Tower>());
            }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = LayerMask.GetMask("Map");
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Camera.main.transform.position.y * 2, mask);
                if (hit.collider != null) {
                    //TestSphere.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
                    //TestSphere.transform.position = hit.point;
                    TowerPlaceLocation = hit.point;
                }
                Debug.DrawRay(ray.origin, ray.direction * Camera.main.transform.position.y * 2, Color.green);
            //}
        }
	}
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class TowerPlacer : NetworkBehaviour {

    public static TowerPlacer Instance;

    public Map GameMap = null;
    public List<Tower> TowerList;
    public GameObject TowerPlaceTesterPrefab = null;
    public GameObject TowerPlaceTester = null;
    public bool TowerPlaceModeOn = false;
    public bool TowerPlaceLocationValid = true;
    public Vector3 TowerPlaceLocation;
    public string TowerToPlaceID = "TowerArcher1";
    public GameObject TestSphere = null;

    private bool _lastTowerPlaceModeOn = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
           // Destroy(this);
        }
    }

    void Start()
    {
        this.GameMap = GameObject.Find("Map").GetComponent<Map>();
    }
	
	void Update() {
        
        if (Input.GetKeyUp(KeyCode.Space)) {
            TowerPlaceModeOn = !TowerPlaceModeOn;
        }

        if (TowerPlaceModeOn) {
            if (!TowerPlaceTester) {
                TowerPlaceTester = Instantiate(TowerPlaceTesterPrefab);
                TowerPlaceTester.GetComponent<TowerPlacementTester>().Init(GameMap, TowerToPlaceID);              
            }

            if (!_lastTowerPlaceModeOn) {
                foreach (Tower t in TowerList) {
                    t.GetComponent<MapCircleDrawer>().SetCircleVisible(true);
                }
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            string layer = LayerMask.LayerToName(GameMap.gameObject.layer);
            LayerMask mask = LayerMask.GetMask(layer);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Camera.main.transform.position.y * 4, mask);
            if (hit.collider != null) {
                TowerPlaceLocation = hit.point;
            }
            Debug.DrawRay(ray.origin, ray.direction * Camera.main.transform.position.y * 2, Color.green);

            TowerPlaceLocationValid = true;
            foreach (Tower t in TowerList) {
                float minDistance = PrefabCache.Instance.PrefabIndex[TowerToPlaceID].GetComponent<Tower>().Radius + t.Radius;
                if (Vector2.Distance(new Vector2(TowerPlaceLocation.x, TowerPlaceLocation.z), new Vector2(t.transform.position.x, t.transform.position.z)) < minDistance) {
                    TowerPlaceLocationValid = false;
                    break;
                }
            }

            if (Input.GetMouseButtonUp(0)) {
                if (TowerPlaceLocationValid) {
                    TowerPlaceTester.GetComponent<TowerPlacementTester>().PlaceTower();
                    //tower.GetComponent<MapCircleDrawer>().UpdateCircle();
                    //TowerList.Add(tower);
                    TowerPlaceModeOn = false;
                }
            }
        }
        if (!TowerPlaceModeOn) {
            if (_lastTowerPlaceModeOn) {
                foreach (Tower t in TowerList) {
                    t.GetComponent<MapCircleDrawer>().SetCircleVisible(false);
                }
            }
        }

        _lastTowerPlaceModeOn = TowerPlaceModeOn;
    }
}

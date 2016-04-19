using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class TowerUpgrader : NetworkBehaviour {

    public static TowerUpgrader Instance;

    public Map GameMap = null;
    public NetworkIdentity OverseerPlayer;
    public OverseerCamera Cam;
    public bool TowerUpgradeModeOn = false;
    public Tower SelectedTower = null;
    public TowerPlacer TowerPlacer = null;


    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            // Destroy(this);
        }
    }

    void Start() {
        this.GameMap = GameObject.Find("Map").GetComponent<Map>();
        TowerPlacer = TowerPlacer.Instance;
    }

    void Update() {

        if (!TowerPlacer.TowerPlaceModeOn) {
            if (InputManager.Instance.OverseerSelectTower) {
                Ray ray = Cam.Camera.ScreenPointToRay(Input.mousePosition);
                for (int i = 0; i < TowerPlacer.TowerList.Count; i++) {
                    Collider towerCol = TowerPlacer.TowerList[i].GetComponent<Collider>();
                    RaycastHit hit;
                    towerCol.Raycast(ray, out hit, Cam.transform.position.y * 4);
                    if (hit.collider != null) {
                        SelectedTower = TowerPlacer.TowerList[i];
                        break;
                    }
                }
            }
        }


        
    }

    void OnDrawGizmos() {
        if (!TowerPlacer.TowerPlaceModeOn) {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(Cam.Camera.ScreenPointToRay(Input.mousePosition));
        }
    }

}

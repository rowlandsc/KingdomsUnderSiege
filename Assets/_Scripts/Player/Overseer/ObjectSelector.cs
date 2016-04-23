using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ObjectSelector : NetworkBehaviour {

    public interface ISelectable {
        GameObject GameObject { get; }

        void RegisterAsSelectable();
        void UnregisterAsSelectable();

        Collider GetSelectionCollider();
    }

    public static ObjectSelector Instance;
    public static List<ISelectable> Selectables = new List<ISelectable>();

    public Map GameMap = null;
    public NetworkIdentity OverseerPlayer;
    public OverseerCamera Cam;
    [SerializeField]
    public ISelectable Selected = null;
    public GameObject SelectedObject = null;
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
            if (InputManager.Instance.OverseerSelect) {
                Ray ray = Cam.Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Cam.transform.position.y * 4);

                for (int i = 0; i < Selectables.Count; i++) {
                    Collider towerCol = Selectables[i].GetSelectionCollider();
                    if (hit.collider == towerCol) {
                        Selected = Selectables[i];
                        break;
                    }
                }
            }
        }

        SelectedObject = (Selected != null) ? Selected.GameObject : null ;
        
    }

    void OnDrawGizmos() {
        if (!TowerPlacer.TowerPlaceModeOn) {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(Cam.Camera.transform.position, Cam.Camera.ScreenPointToRay(Input.mousePosition).direction * Cam.transform.position.y * 4);
        }
    }

}

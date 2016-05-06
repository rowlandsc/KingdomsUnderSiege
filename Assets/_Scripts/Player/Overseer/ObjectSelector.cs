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

    public delegate void dNewSelection(GameObject selectedObject);
    public static event dNewSelection eOnNewSelection;

    public static ObjectSelector Instance;
    public static List<ISelectable> Selectables = new List<ISelectable>();

    public Map GameMap = null;
    public NetworkIdentity OverseerPlayer;
    public OverseerCamera Cam;
    [SerializeField]
    public ISelectable Selected = null;
    public GameObject SelectedObject = null;
    public TowerPlacer TowerPlacer = null;

    private MapCircleDrawer _mapCircleDrawer;

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
        _mapCircleDrawer = GetComponent<MapCircleDrawer>();
        _mapCircleDrawer.GameMap = Map.Instance;
        _mapCircleDrawer.SetCircleVisible(false);
    }

    void Update() {

        if (!TowerPlacer.TowerPlaceModeOn) {
            if (InputManager.Instance.OverseerSelect) {
                GameObject oldSelection = SelectedObject;
                Ray ray = Cam.Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Cam.transform.position.y * 4);

                bool found = false;
                for (int i = 0; i < Selectables.Count; i++) {
                    Collider towerCol = Selectables[i].GetSelectionCollider();
                    if (hit.collider == towerCol) {
                        Selected = Selectables[i];
                        _mapCircleDrawer.CircleRadius = Selected.GetSelectionCollider().bounds.extents.magnitude;
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    Selected = null;
                    SelectedObject = null;
                    _mapCircleDrawer.SetCircleVisible(false);
                }
                else {
                    SelectedObject = Selected.GameObject;
                }

                if (SelectedObject != oldSelection) {
                    if (eOnNewSelection != null) eOnNewSelection(SelectedObject);
                }
            }
        }

        SelectedObject = (Selected != null) ? Selected.GameObject : null ;
        
        if (SelectedObject != null) {
            _mapCircleDrawer.SetCircleVisible(true);
            _mapCircleDrawer.CirclePosition = SelectedObject.transform.position;
            _mapCircleDrawer.UpdateCircle();
        }
        else {
            _mapCircleDrawer.SetCircleVisible(false);
        }
    }

    void OnDrawGizmos() {
        if (!TowerPlacer.TowerPlaceModeOn) {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(Cam.Camera.transform.position, Cam.Camera.ScreenPointToRay(Input.mousePosition).direction * Cam.transform.position.y * 4);
        }
    }

}

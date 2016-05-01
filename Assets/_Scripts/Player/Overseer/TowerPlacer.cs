using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class TowerPlacer : NetworkBehaviour {

    public static TowerPlacer Instance;

    public Map GameMap = null;
    public OverseerCamera Cam;
    public List<Tower> TowerList;
    public GameObject TowerPlaceTesterPrefab = null;
    public GameObject TowerPlaceTester = null;
    public bool TowerPlaceModeOn = false;
    public bool TowerPlaceLocationValid = true;
    public Vector3 TowerPlaceLocation;
    public string TowerToPlaceID = "TowerArcher1";
    public string ArcherTowerID = "TowerArcher1";
    public string MortarTowerID = "TowerMortar1";
    public string MageTowerID = "TowerMagic1";
    public NetworkIdentity OverseerPlayer;

    private Transform _towerParent = null;
    public Transform _overseerTowerMarker = null;
    public Transform _heroTowerMarker = null;
    private bool _lastTowerPlaceModeOn = false;
    private bool _canPlaceTowers = true;


    private float archerTowerCost = 0;
    private float mortarTowerCost = 0;
    private float mageTowerCost = 0;
    private NetworkPlayerStats overseerStats;

    public bool CanPlaceTowers {
        get {
            return _canPlaceTowers;
        }
    }
    public bool CanPlaceArcherTower {
        get {
            int overseerGold = overseerStats.Gold;
            return (CanPlaceTowers && (overseerGold >= archerTowerCost));
        }
    }
    public bool CanPlaceMortarTower {
        get {
            int overseerGold = overseerStats.Gold;
            return (CanPlaceTowers && (overseerGold >= mortarTowerCost));
        }
    }
    public bool CanPlaceMageTower {
        get {
            int overseerGold = overseerStats.Gold;
            return (CanPlaceTowers && (overseerGold >= mageTowerCost));
        }
    }
    public bool CanPlaceCurrentTower {
        get {
            if (TowerToPlaceID == ArcherTowerID) return CanPlaceArcherTower;
            if (TowerToPlaceID == MortarTowerID) return CanPlaceMortarTower;
            if (TowerToPlaceID == MageTowerID) return CanPlaceMageTower;
            return true;
        }
    }


    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
           // Destroy(this);
        }
    }
    

    void OnEnable()
    {
        RoundManager.AddListener("RoundEnded", SetCanPlaceTowers);
        RoundManager.AddListener("PreroundEnded", SetCannotPlaceTowers);
    }

    void OnDisable()
    {
        RoundManager.RemoveListener("RoundEnded", SetCanPlaceTowers);
        RoundManager.RemoveListener("PreroundEnded", SetCannotPlaceTowers);
    }

    void Start()
    {
        this.GameMap = GameObject.Find("Map").GetComponent<Map>();
        _towerParent = GameObject.Find("Towers").transform;
        _overseerTowerMarker = _towerParent.FindChild("OverseerBaseMarker");
        _heroTowerMarker = _towerParent.FindChild("HeroBaseMarker");

        overseerStats = KUSNetworkManager.LocalPlayer.GetComponent<NetworkPlayerStats>();
        archerTowerCost = PrefabCache.Instance.PrefabIndex[ArcherTowerID].GetComponent<ProfileSystem>().Worth;
        mortarTowerCost = PrefabCache.Instance.PrefabIndex[MortarTowerID].GetComponent<ProfileSystem>().Worth;
        mageTowerCost = PrefabCache.Instance.PrefabIndex[MageTowerID].GetComponent<ProfileSystem>().Worth;
    }
	
	void Update() {
        
        //if (Input.GetKeyUp(KeyCode.Space) && this._canPlaceTowers) {
        //    TowerPlaceModeOn = !TowerPlaceModeOn;
        //}

        if (TowerPlaceModeOn) {
            if (!CanPlaceCurrentTower) {
                StopPlacingTower();
                return;
            }

            if (!_lastTowerPlaceModeOn) {
                foreach (Tower t in TowerList) {
                    t.GetComponent<MapCircleDrawer>().SetCircleVisible(true);
                }
            }

            if (!TowerPlaceTester) {
                TowerPlaceTester = Instantiate(TowerPlaceTesterPrefab);
                TowerPlaceTester.GetComponent<TowerPlacementTester>().Init(GameMap, TowerToPlaceID);
                return;              
            }

            Ray ray = Cam.Camera.ScreenPointToRay(Input.mousePosition);
            string layer = "MapPlayArea";
            LayerMask mask = LayerMask.GetMask(layer);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Cam.transform.position.y * 4, mask);
            if (hit.collider != null) {
                TowerPlaceLocation = hit.point;
            }
            Debug.DrawRay(ray.origin, ray.direction * Cam.transform.position.y * 2, Color.green);

            TowerPlaceLocationValid = true;
            foreach (Tower t in TowerList) {
                float minDistance = PrefabCache.Instance.PrefabIndex[TowerToPlaceID].GetComponent<Tower>().Radius + t.Radius;
                if (Vector2.Distance(new Vector2(TowerPlaceLocation.x, TowerPlaceLocation.z), new Vector2(t.transform.position.x, t.transform.position.z)) < minDistance) {
                    TowerPlaceLocationValid = false;
                    break;
                }
            }
            if (TowerPlaceLocationValid) {
                LayerMask baseMask = LayerMask.GetMask("BaseWalls");
                RaycastHit hitOverseerBase, hitHeroBase;
                Ray overseerBaseRay = new Ray(_overseerTowerMarker.transform.position, TowerPlaceLocation - _overseerTowerMarker.transform.position);
                Ray heroBaseRay = new Ray(_heroTowerMarker.transform.position, TowerPlaceLocation - _heroTowerMarker.transform.position);
                Physics.Raycast(overseerBaseRay, out hitOverseerBase, 1000, baseMask);
                if (hitOverseerBase.collider == null) TowerPlaceLocationValid = false;

                if (TowerPlaceLocationValid) {
                    Physics.Raycast(heroBaseRay, out hitHeroBase, 1000, baseMask);
                    if (hitHeroBase.collider == null) TowerPlaceLocationValid = false;
                }
            }


            if (Input.GetMouseButtonUp(0)) {
                if (hit.collider != null && TowerPlaceLocationValid) {
                    TowerPlaceTester.GetComponent<TowerPlacementTester>().PlaceTower(OverseerPlayer);
                    //tower.GetComponent<MapCircleDrawer>().UpdateCircle();
                    //TowerList.Add(tower);
                    TowerPlaceModeOn = false;
                    OverseerPlayer.GetComponent<NetworkPlayerStats>().AddTowerPlaced();
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

    public void StartPlacingTower(string towerID) {
        TowerPlaceModeOn = true;
        TowerToPlaceID = towerID;
    }

    public void StopPlacingTower() {
        TowerPlaceModeOn = false;
        TowerToPlaceID = "";
    }

    void SetCanPlaceTowers()
    {
        this._canPlaceTowers = true;
    }

    void SetCannotPlaceTowers()
    {
        this._canPlaceTowers = false;
        this.TowerPlaceModeOn = false;
    }

    void OnDrawGizmos() {
        if (TowerPlaceModeOn) {
            Gizmos.color = new Color(1, 1, 1, 1);
            Gizmos.DrawLine(_overseerTowerMarker.transform.position, TowerPlaceLocation);
            Gizmos.DrawLine(_heroTowerMarker.transform.position, TowerPlaceLocation);
        }
    }
}

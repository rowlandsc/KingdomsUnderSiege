using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TowerPlacementTester : MonoBehaviour {

    public Map GameMap = null;
    public Tower TowerToPlace = null;
    public Material ValidMaterial = null;
    public Material InvalidMaterial = null;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private MapCircleDrawer _mapCircleDrawer;

    public void Init(Map map, Tower tower) {
        GameMap = map;

        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _mapCircleDrawer = GetComponent<MapCircleDrawer>();

        TowerToPlace = tower;
        _meshFilter.mesh = TowerToPlace.GetComponent<MeshFilter>().sharedMesh;
        Material[] matlist = TowerToPlace.GetComponent<MeshRenderer>().sharedMaterials;
        for (int i=0; i<matlist.Length; i++) {
            matlist.SetValue(ValidMaterial, i);
        }
        _meshRenderer.sharedMaterials = matlist;

        _mapCircleDrawer.CircleRadius = TowerToPlace.Radius;
        _mapCircleDrawer.GameMap = GameMap;

        transform.localScale = TowerToPlace.transform.localScale;
    }

    void Update() {
        if (TowerPlacer.Instance.TowerPlaceModeOn) {
            transform.position = TowerPlacer.Instance.TowerPlaceLocation;
            if (TowerPlacer.Instance.TowerPlaceLocationValid) {
                Material[] matlist = _meshRenderer.sharedMaterials;
                for (int i = 0; i < matlist.Length; i++) {
                    matlist.SetValue(ValidMaterial, i);
                }
                _meshRenderer.sharedMaterials = matlist;
            }
            else {
                Material[] matlist = _meshRenderer.sharedMaterials;
                for (int i = 0; i < matlist.Length; i++) {
                    matlist.SetValue(InvalidMaterial, i);
                }
                _meshRenderer.sharedMaterials = matlist;
            }
            if (!_mapCircleDrawer.CircleIsVisible()) {
                _mapCircleDrawer.SetCircleVisible(true);
            }
            _mapCircleDrawer.UpdateCircle();
        }
        else {
            Destroy(gameObject);
        }
    }

    public Tower PlaceTower() {
        Tower tower = GameObject.Instantiate<Tower>(TowerToPlace);
        tower.transform.position = transform.position;
        tower.transform.rotation = transform.rotation;
        tower.GetComponent<MapCircleDrawer>().GameMap = GameMap;
        tower.GetComponent<MapCircleDrawer>().CircleRadius = tower.Radius;
        return tower;
    }
}

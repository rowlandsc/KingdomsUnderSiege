using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TowerPlacementTester : MonoBehaviour {

    public Tower TowerToPlace = null;
    public Material ValidMaterial = null;
    public Material InvalidMaterial = null;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    public void Init(Tower tower) {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();

        TowerToPlace = tower;
        _meshFilter.mesh = TowerToPlace.GetComponent<MeshFilter>().sharedMesh;
        _meshRenderer.material = ValidMaterial;

        transform.localScale = TowerToPlace.transform.localScale;
    }

    void Update() {
        if (TowerPlacer.Instance.TowerPlaceModeOn) {
            transform.position = TowerPlacer.Instance.TowerPlaceLocation;
            if (TowerPlacer.Instance.TowerPlaceLocationValid) {
                _meshRenderer.material = ValidMaterial;
            }
            else {
                _meshRenderer.material = InvalidMaterial;
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    public void PlaceTower() {
        GameObject.Instantiate(TowerToPlace, transform.position, transform.rotation);
        
    }
}

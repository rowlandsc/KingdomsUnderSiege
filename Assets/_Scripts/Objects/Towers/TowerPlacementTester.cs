using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TowerPlacementTester : NetworkBehaviour
{

    public Map GameMap = null;
    public string TowerToPlaceID = "TowerArcher1";
    public Material ValidMaterial = null;
    public Material InvalidMaterial = null;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private MapCircleDrawer _mapCircleDrawer;

    private Tower _networkSpawnedTower;

    public void Init(Map map, string towerID)
    {
        GameMap = map;

        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _mapCircleDrawer = GetComponent<MapCircleDrawer>();

        TowerToPlaceID = towerID;
        _meshFilter.mesh = PrefabCache.Instance.PrefabIndex[TowerToPlaceID].GetComponent<MeshFilter>().sharedMesh;
        Material[] matlist = PrefabCache.Instance.PrefabIndex[TowerToPlaceID].GetComponent<MeshRenderer>().sharedMaterials;
        for (int i = 0; i < matlist.Length; i++)
        {
            matlist.SetValue(ValidMaterial, i);
        }
        _meshRenderer.sharedMaterials = matlist;

        _mapCircleDrawer.CircleRadius = PrefabCache.Instance.PrefabIndex[TowerToPlaceID].GetComponent<Tower>().Radius;
        _mapCircleDrawer.GameMap = GameMap;

        transform.localScale = PrefabCache.Instance.PrefabIndex[TowerToPlaceID].transform.localScale;
    }

    void Update()
    {
        if (TowerPlacer.Instance.TowerPlaceModeOn)
        {
            transform.position = TowerPlacer.Instance.TowerPlaceLocation;
            if (TowerPlacer.Instance.TowerPlaceLocationValid)
            {
                Material[] matlist = _meshRenderer.sharedMaterials;
                for (int i = 0; i < matlist.Length; i++)
                {
                    matlist.SetValue(ValidMaterial, i);
                }
                _meshRenderer.sharedMaterials = matlist;
            }
            else {
                Material[] matlist = _meshRenderer.sharedMaterials;
                for (int i = 0; i < matlist.Length; i++)
                {
                    matlist.SetValue(InvalidMaterial, i);
                }
                _meshRenderer.sharedMaterials = matlist;
            }
            if (!_mapCircleDrawer.CircleIsVisible())
            {
                _mapCircleDrawer.SetCircleVisible(true);
            }
            _mapCircleDrawer.UpdateCircle();
        }
        else {
            Destroy(gameObject);
        }
    }


    public void PlaceTower()
    {
        KUSNetworkManager.HostPlayer.CmdPlaceTower(TowerToPlaceID, transform.position);
    }

    /*[Command]
    public void CmdPlaceTower()
    {
        Debug.Log("Server Command Called");
        Tower tower = GameObject.Instantiate<Tower>(TowerToPlace);
        tower.transform.position = transform.position;
        tower.transform.rotation = transform.rotation;
        tower.GetComponent<MapCircleDrawer>().GameMap = GameMap;
        tower.GetComponent<MapCircleDrawer>().CircleRadius = tower.Radius;
        NetworkServer.Spawn(tower.gameObject);

        _networkSpawnedTower = tower;
    }*/
}

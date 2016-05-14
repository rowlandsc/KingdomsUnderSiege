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
    public float RotateSpeed = 180;

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
        GameObject meshPrefab = PrefabCache.Instance.PrefabIndex[TowerToPlaceID + "1"];
        /*_meshFilter.mesh = GetComponent<MeshFilter>().sharedMesh;
        Material[] matlist = PrefabCache.Instance.PrefabIndex[TowerToPlaceID].GetComponent<MeshRenderer>().sharedMaterials;
        for (int i = 0; i < matlist.Length; i++)
        {
            matlist.SetValue(ValidMaterial, i);
        }
        _meshRenderer.sharedMaterials = matlist;*/

        GenerateMeshFromPrefab(meshPrefab.transform, transform);

        _mapCircleDrawer.CircleRadius = PrefabCache.Instance.PrefabIndex[TowerToPlaceID].GetComponent<Tower>().Radius;
        _mapCircleDrawer.GameMap = GameMap;

        transform.localScale = PrefabCache.Instance.PrefabIndex[TowerToPlaceID].transform.localScale;
    }

    public void GenerateMeshFromPrefab(Transform prefabToCopy, Transform corObj) {
        MeshRenderer mr = prefabToCopy.gameObject.GetComponent<MeshRenderer>();
        MeshFilter mf = prefabToCopy.gameObject.GetComponent<MeshFilter>();
        if (mr != null && mf != null) {
            MeshFilter thisMf = corObj.GetComponent<MeshFilter>();
            if (thisMf == null) thisMf = corObj.gameObject.AddComponent<MeshFilter>();
            MeshRenderer thisMr = corObj.GetComponent<MeshRenderer>();
            if (thisMr == null) thisMr = corObj.gameObject.AddComponent<MeshRenderer>();

            thisMf.mesh = mf.sharedMesh;

            Material[] matlist = mr.sharedMaterials;
            for (int i = 0; i < matlist.Length; i++) {
                matlist.SetValue(ValidMaterial, i);
            }
            thisMr.sharedMaterials = matlist;
        }

        for (int i=0; i<prefabToCopy.childCount; i++) {
            Transform prefabChild = prefabToCopy.GetChild(i);
            GameObject newChild = new GameObject(prefabChild.name);
            newChild.transform.SetParent(corObj);
            newChild.transform.localPosition = prefabChild.localPosition;
            newChild.transform.localScale = prefabChild.localScale;
            GenerateMeshFromPrefab(prefabChild, newChild.transform);
        }
    }

    void Update()
    {
        if (InputManager.Instance.OverseerRotateTower) {
            transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
        }

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
            _mapCircleDrawer.CirclePosition = transform.position;
            _mapCircleDrawer.UpdateCircle();
        }
        else {
            Destroy(gameObject);
        }
    }


    public void PlaceTower(NetworkIdentity player)
    {
        KUSNetworkManager.HostPlayer.CmdPlaceTower(player, TowerToPlaceID, transform.position, transform.rotation);
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

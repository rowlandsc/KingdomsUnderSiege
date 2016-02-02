using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayerObject : NetworkBehaviour {
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    public void CmdPlaceTower(GameObject TowerToPlace, GameObject GameMap) { 
   
        Debug.Log("Server Command Called");
        Tower tower = GameObject.Instantiate(TowerToPlace).GetComponent<Tower>();
        tower.transform.position = transform.position;
        tower.transform.rotation = transform.rotation;
        tower.GetComponent<MapCircleDrawer>().GameMap = GameMap.GetComponent<Map>();
        tower.GetComponent<MapCircleDrawer>().CircleRadius = tower.Radius;
        NetworkServer.Spawn(tower.gameObject);
    }
}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayerObject : NetworkBehaviour {
    
    public enum PlayerType
    {
        HERO,
        OVERSEER
    }

    public enum PlayerClass
    {
        KNIGHT,
        ARCHER,
        MAGE,
        OVERSEER
    }

    public PlayerType Type;
    public PlayerClass Class;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    public void CmdPlaceTower(string prefabID, Vector3 position) { 
   
        Debug.Log("Server Command Called " + prefabID + " " + position);
        Tower tower = GameObject.Instantiate(PrefabCache.Instance.PrefabIndex[prefabID]).GetComponent<Tower>();
        tower.transform.position = position;
        //tower.transform.rotation = transform.rotation;
        tower.GetComponent<MapCircleDrawer>().GameMap = Map.Instance;
        tower.GetComponent<MapCircleDrawer>().CircleRadius = tower.Radius;
        NetworkServer.Spawn(tower.gameObject);
    }
}

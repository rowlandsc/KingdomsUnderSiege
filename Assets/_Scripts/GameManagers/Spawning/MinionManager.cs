using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MinionManager : NetworkBehaviour {

    public static MinionManager Instance = null;
    
    public ArrayList ActiveMinions;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

	// Use this for initialization
	void Start () {
        ActiveMinions = new ArrayList();
    }

    public static void AddActiveMinion(GameObject minion)
    {
        MinionManager.Instance.ActiveMinions.Add(minion);
    }

    public static void RemoveActiveMinion(GameObject minion)
    {
        MinionManager.Instance.ActiveMinions.Remove(minion);
    }
}

using UnityEngine;
using System.Collections;

public class OverseerEndStructure : MonoBehaviour {

    public GameObject[] OverseerMinionSpawners;
    public bool Destroyable = false;
    private BoxCollider _collider;

	// Use this for initialization
	void Start () {
        this._collider = GetComponent<BoxCollider>();
        this._collider.enabled = false;
        this.OverseerMinionSpawners = new GameObject[2];
        setSpawners();
	}

    void OnEnable()
    {
        OverseerSpawnerDeath.spawnerDied += bothDestroyed;
        OverseerSpawnerDeath.spawnerRespawned += oneRespawn;
    }

    void OnDisable()
    {
        OverseerSpawnerDeath.spawnerDied -= bothDestroyed;
        OverseerSpawnerDeath.spawnerRespawned -= oneRespawn;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setCanBeDestroyed()
    {
        this.Destroyable = true;
        this._collider.enabled = true;
    }

    public void setCannotBeDestroyed()
    {
        this.Destroyable = false;
        this._collider.enabled = false;
    }

    public void setSpawners()
    {
        GameObject[] overseerObjects = GameObject.FindGameObjectsWithTag("OverseerPlayer");
        int added = 0;

        for(int i = 0; i < overseerObjects.Length; i++)
        {
            if(overseerObjects[i].name == "OverseerMinionSpawn(Clone)")
            {
                this.OverseerMinionSpawners[added] = overseerObjects[i];
                added++;
            }
            if(added > 1)
            {
                break;
            }
        }
    }

    public void bothDestroyed()
    {
        if(this.OverseerMinionSpawners[0].GetComponent<OverseerSpawnerDeath>().isDead &&
            this.OverseerMinionSpawners[1].GetComponent<OverseerSpawnerDeath>().isDead)
        {
            this.setCanBeDestroyed();
        }
    }

    public void oneRespawn()
    {
        this.setCannotBeDestroyed();
    }
}

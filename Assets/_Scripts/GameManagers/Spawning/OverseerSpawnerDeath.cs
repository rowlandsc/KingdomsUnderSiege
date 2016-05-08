using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class OverseerSpawnerDeath : NetworkBehaviour, IKillable {

    public bool isDead = false;
    public int RespawnTime = 100;


    public delegate void Died();
    public static event Died spawnerDied;
    public static event Died spawnerRespawned;

	public void OnDeath()
    {
        int goldAmount = GetComponent<ProfileSystem>().Worth;
        if (!isDead)
        {
            if (KUSNetworkManager.KnightPlayer != null)
            {
                NetworkPlayerStats.AddGold(KUSNetworkManager.KnightPlayer, goldAmount);
            }
            if (KUSNetworkManager.MagePlayer != null)
            {
                NetworkPlayerStats.AddGold(KUSNetworkManager.MagePlayer, goldAmount);
            }
            if (KUSNetworkManager.ArcherPlayer != null)
            {
                NetworkPlayerStats.AddGold(KUSNetworkManager.ArcherPlayer, goldAmount);
            }

            Debug.Log("[IMPORTANT]: Switch to destroyed model");

            this.isDead = true;
            GetComponent<ProfileSystem>().baseHealthPoints = 1000f;
            spawnerDied();
            StartCoroutine(respawnTimer());
        }
    }

    public IEnumerator respawnTimer()
    {
        yield return new WaitForSeconds(RespawnTime);
        this.isDead = false;
        GetComponent<ProfileSystem>().baseHealthPoints = GetComponent<ProfileSystem>().MaxHealthPoints;
        spawnerRespawned();
    }
    
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class OverseerSpawnerDeath : NetworkBehaviour, IKillable {
    
	public void OnDeath()
    {
            int goldAmount = GetComponent<ProfileSystem>().Worth;

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

            Destroy(this.gameObject);
    }    
}

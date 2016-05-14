using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class HeroSpawnerDeath : NetworkBehaviour, IKillable {

    public void OnDeath()
    {
        NetworkPlayerObject player = KUSNetworkManager.OverseerPlayer;
        player.GetComponent<NetworkPlayerStats>().AddGold(GetComponent<ProfileSystem>().Worth);

        Destroy(this.gameObject);
    }
}

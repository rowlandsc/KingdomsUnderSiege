using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using DG.Tweening;

public class HeroObject : NetworkBehaviour, IKillable, ObjectSelector.ISelectable {
    private GameObject Mage_birthplace;
    private GameObject Knight_birthplace;
    private GameObject Arch_birthplace;
    private GameObject deathPoint;
    private ProfileSystem _ps;

    string herorespwn_words = "";
    float herorespwn_timer = 10f;

    // Use this for initialization
    void Start () {
        Mage_birthplace = GameObject.Find("MageSummonPoint");
        Knight_birthplace = GameObject.Find("KnightSummonPoint");
        Arch_birthplace = GameObject.Find("ArchSummonPoint");
        deathPoint = GameObject.Find("DeathPoint");
        _ps = GetComponent<ProfileSystem>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDeath()
    {
        this.gameObject.transform.DOMove(deathPoint.transform.position, 0.5f, false);
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        _ps.baseHealthPoints = _ps.MaxHealthPoints;
        NetworkPlayerOwner playerOwner = this.GetComponent<NetworkPlayerOwner>();
        NetworkPlayerStats stats = playerOwner.Owner.GetComponent<NetworkPlayerStats>();
        stats.AddDeath();
        if (_ps.Killer != NetworkInstanceId.Invalid)
        {
            if (isServer)
            {
                GameObject player = NetworkServer.FindLocalObject(_ps.Killer);
                NetworkPlayerStats playerStats = player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>();
                playerStats.AddGold((int)_ps.Worth);
                playerStats.AddHeroKill();
            }
            else
            {
                GameObject player = ClientScene.FindLocalObject(_ps.Killer);
                NetworkPlayerStats playerStats = player.GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerStats>();
                playerStats.AddGold((int)_ps.Worth);
                playerStats.AddHeroKill();
            }
        }
        StartCoroutine(respawn());
    }

    public IEnumerator respawn()
    {
        herorespwn_words = "Respawn in " + ((int)Mathf.Round(herorespwn_timer)).ToString() + " seconds";
        yield return new WaitForSeconds(herorespwn_timer);

        if (this.gameObject.name == "Mage(Clone)")
        {
            this.gameObject.transform.DOMove(Mage_birthplace.transform.position, 0.1f, false);
        }

        else if (this.gameObject.name == "Knight(Clone)")
        {
            this.gameObject.transform.DOMove(Knight_birthplace.transform.position, 0.1f, false);
        }

        else if (this.gameObject.name == "Archer(Clone)")
        {
            this.gameObject.transform.DOMove(Arch_birthplace.transform.position, 0.1f, false);
        }

        this.gameObject.GetComponent<Rigidbody>().useGravity = true;

        _ps.baseHealthPoints = _ps.MaxHealthPoints;
        _ps.baseMagicPoints = _ps.MaxMagicPoints;

    }

    public GameObject GameObject {
        get { return gameObject; }
    }

    public void RegisterAsSelectable() {
        if (KUSNetworkManager.LocalPlayer.Class == NetworkPlayerObject.PlayerClass.OVERSEER) ObjectSelector.Selectables.Add(this);
    }
    public void UnregisterAsSelectable() {
        if (KUSNetworkManager.LocalPlayer.Class == NetworkPlayerObject.PlayerClass.OVERSEER) ObjectSelector.Selectables.Remove(this);
    }

    public Collider GetSelectionCollider() {
        return GetComponent<Collider>();
    }
}

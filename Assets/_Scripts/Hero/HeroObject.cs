using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.Text;

public class HeroObject : NetworkBehaviour, IKillable, ObjectSelector.ISelectable {
    private GameObject Mage_birthplace;
    private GameObject Knight_birthplace;
    private GameObject Arch_birthplace;
    private GameObject deathPoint;
    private ProfileSystem _ps;

    string herorespwn_words = "";
    float herorespwn_timer = 10f;
	bool herodie = false;
	private NetworkPlayerInput _playerInput;
	private Text words = null;
	private GameObject DeathScreen = null;

    // Use this for initialization
    void Start () {
        Mage_birthplace = GameObject.Find("MageSummonPoint");
        Knight_birthplace = GameObject.Find("KnightSummonPoint");
        Arch_birthplace = GameObject.Find("ArchSummonPoint");
        deathPoint = GameObject.Find("DeathPoint");
        _ps = GetComponent<ProfileSystem>();
		_playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
		words = GameObject.FindGameObjectWithTag("DeathCount").GetComponent<Text>();
		DeathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDeath()
    {
		if(_playerInput.CheckLocalPlayer != 1) {
			
		} else {
			
			this.gameObject.transform.position = deathPoint.transform.position;
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
        
    }

    public IEnumerator respawn()
    {
        DeathCount();
        StringBuilder sb = new StringBuilder();
        while(herorespwn_timer >= float.Epsilon)
        {
            sb.Append("Respawn in ");
            sb.Append(((int)Mathf.Round(herorespwn_timer)).ToString());
            sb.Append(" seconds");
            herorespwn_words = sb.ToString();
            words.text = herorespwn_words;
            herorespwn_timer -= Time.deltaTime;

            // Clear StringBuilder
            sb.Length = 0;
            sb.Capacity = 0;
            yield return null;
        }
        
		
        //yield return new WaitForSeconds(herorespwn_timer);

        if (this.gameObject.name == "Mage(Clone)")
        {
            this.gameObject.transform.position = Mage_birthplace.transform.position;
			UnDeathCount();
        }

        else if (this.gameObject.name == "Knight(Clone)")
        {
            this.gameObject.transform.position = Knight_birthplace.transform.position;
			UnDeathCount();
        }

        else if (this.gameObject.name == "Arch(Clone)")
        {
            this.gameObject.transform.position = Arch_birthplace.transform.position;
			UnDeathCount();
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

	void OnGUI() {
		if (this.herodie ==  true && _playerInput.CheckLocalPlayer == 1) {
			print("hero DE");
		}
	}

	void DeathCount() {
		if (_playerInput.CheckLocalPlayer == 1) {
			words.text = "You died, respawn in 10 sec";
			DeathScreen.GetComponent<RawImage>().enabled = true;
		}
	}

	void UnDeathCount() {
		words.text = "";
		DeathScreen.GetComponent<RawImage>().enabled = false;
	}

}

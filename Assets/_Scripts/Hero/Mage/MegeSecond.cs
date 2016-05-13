using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Networking;

public class MegeSecond : MonoBehaviour {


	private float holdtime=3f;
	public float cooldown=6f;
	public float distance=20f;
	public float mp_use=10f;

	private bool canAttack;
	private bool Startcooldown;
	public float holdingtime;
	public float cooldown_timer;
	private int bullet_shot;

	private GameObject icebullet_clone1,icebullet_clone2,icebullet_clone3,icebullet_clone4,icebullet_clone5;

    private NetworkPlayerInput _playerInput;

	//FX
	public GameObject charingAnim;
	public GameObject charingAnim2;
	public GameObject charingAnim3;


	private GameObject chargingAnim_;
	private GameObject chargingAnim2_;
	private GameObject chargingAnim3_;


	public GameObject icebullet;

	private bool anim_2_once,anim_3_once;
	private float size_attack;

	private GameObject spellPosition;
	
	// Use this for initialization
	void Start () {
		spellPosition = GameObject.Find("SpellPosition");
	
		holdingtime=0;
		bullet_shot=0;
		canAttack = true;
		Startcooldown = false;
		cooldown_timer = cooldown;

		anim_2_once=true;
		anim_3_once=true;

		size_attack= Screen.width/15;
        _playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInput.HeroMeleeChargeAttackInputDown > 0 && canAttack && this.gameObject.GetComponent<ProfileSystem>().MPenough(mp_use))
        {
            KUSNetworkManager.HostPlayer.CmdMageCharging(
                GetComponent<NetworkIdentity>(),
                "MageCharging1",
                this.gameObject.transform.position - new Vector3(0, 0.2f, 0),
                transform.rotation);
        }
        
        if (_playerInput.HeroMeleeChargeAttackInput > 0 && canAttack && this.gameObject.GetComponent<ProfileSystem>().MPenough(mp_use))
        {
            holdingtime += Time.deltaTime;

            //charing make the bullets different
            if (holdingtime >= 0)
            {
                // (shot 1 bullets)
                bullet_shot = 1;

                if (holdingtime >= holdtime * 0.5f)
                {
                    if (anim_2_once)
                    {
                        KUSNetworkManager.HostPlayer.CmdMageCharging(
                            GetComponent<NetworkIdentity>(),
                            "MageCharging2",
                            this.gameObject.transform.position - new Vector3(0, 0.2f, 0),
                            transform.rotation);
                        anim_2_once = false;
                    }

                    bullet_shot = 3;

                    if (holdingtime >= holdtime * 1.0f)
                    {
                        if (anim_3_once)
                        {
                            KUSNetworkManager.HostPlayer.CmdMageCharging(
                                GetComponent<NetworkIdentity>(),
                                "MageCharging3",
                                this.gameObject.transform.position - new Vector3(0, 0.2f, 0),
                                transform.rotation);
                            anim_3_once = false;
                        }

                        bullet_shot = 5;

                    }
                }
            }

        }

        if (_playerInput.HeroMeleeChargeAttackInputUp > 0 && canAttack)
        {
            this.gameObject.GetComponent<ProfileSystem>().useMagic(mp_use);

            anim_2_once = true;
            anim_3_once = true;

            KUSNetworkManager.HostPlayer.CmdMageSecondFxDestroy();

            holdingtime = 0;

            int layerMask = new int();
            layerMask = 1 << 12;   // 8th layer is the layer you want to ignore
            layerMask = ~layerMask;


            if (bullet_shot >= 1)
            {
                Ray ray1 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit1;
                if (Physics.Raycast(ray1, out hit1, Mathf.Infinity, layerMask))
                {
					if (hit1.transform.gameObject.tag != "HeroPlayer")
                    {
                        KUSNetworkManager.HostPlayer.CmdMageSecond(
                            GetComponent<NetworkIdentity>(),
                            spellPosition.transform.position,
                            Quaternion.LookRotation(ray1.direction),
                            hit1);
                    }
                }

                if (bullet_shot >= 3)
                {
                    Ray ray2 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2 - size_attack, Screen.height / 2, 0));
                    Ray ray3 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2 + size_attack, Screen.height / 2, 0));
                    RaycastHit hit2, hit3;

                    if (Physics.Raycast(ray2, out hit2, Mathf.Infinity, layerMask))
                    {
						if (hit2.transform.gameObject.tag != "HeroPlayer")
                        {
                            KUSNetworkManager.HostPlayer.CmdMageSecond(
                            GetComponent<NetworkIdentity>(),
                            spellPosition.transform.position,
                            Quaternion.LookRotation(ray2.direction),
                            hit2);
                        }
                    }
                    if (Physics.Raycast(ray3, out hit3, Mathf.Infinity, layerMask))
                    {
						if (hit3.transform.gameObject.tag != "HeroPlayer")
                        {
                            KUSNetworkManager.HostPlayer.CmdMageSecond(
                            GetComponent<NetworkIdentity>(),
                            spellPosition.transform.position,
                            Quaternion.LookRotation(ray3.direction),
                            hit3);
                        }
                    }

                    if (bullet_shot >= 5)
                    {
                        Ray ray4 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2 - 2 * size_attack, Screen.height / 2, 0));
                        Ray ray5 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2 + 2 * size_attack, Screen.height / 2, 0));
                        RaycastHit hit4, hit5;

                        if (Physics.Raycast(ray4, out hit4, Mathf.Infinity, layerMask))
                        {
							if (hit4.transform.gameObject.tag != "HeroPlayer")
                            {
                                KUSNetworkManager.HostPlayer.CmdMageSecond(
                                GetComponent<NetworkIdentity>(),
                                spellPosition.transform.position,
                                Quaternion.LookRotation(ray4.direction),
                                hit4);
                            }
                        }
                        if (Physics.Raycast(ray5, out hit5, Mathf.Infinity, layerMask))
                        {
                            if (hit5.transform.gameObject.tag != "HeroPlayer")
                            {
                                KUSNetworkManager.HostPlayer.CmdMageSecond(
                                GetComponent<NetworkIdentity>(),
                                spellPosition.transform.position,
                                Quaternion.LookRotation(ray5.direction),
                                hit5);
                            }

                        }
                    }
                }

                Startcooldown = true;
            }

            if (Startcooldown)
            {

                canAttack = false;
                cooldown_timer -= Time.deltaTime;

                if (cooldown_timer < 0)
                {
                    bullet_shot = 0;
                    canAttack = true;
                    Startcooldown = false;
                    cooldown_timer = cooldown;
                }
            }
        }
    }


	public float gettimer(){
		return cooldown_timer;
	}

	public float getcooldown(){
		return cooldown;
	}
}

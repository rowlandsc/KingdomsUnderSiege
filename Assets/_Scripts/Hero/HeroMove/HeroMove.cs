using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Networking;

public class HeroMove : NetworkBehaviour {



	public float movespeed=1.5f;
	public float backwardspeed=1.0f;
	public float sidespeed=1.2f;

	static public bool CanMove=true;

	private GameObject Maincamera;
	private GameObject[] NewGameObject;

	private Vector3 cureent_position;
	public bool ifMoving;
	private Vector3 curPos, LastPos;

    private NetworkPlayerInput _playerInput;

    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
		ifMoving=false;
		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        _playerInput = GetComponent<NetworkPlayerOwner>().Owner.GetComponent<NetworkPlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

		Maincamera = GameObject.FindGameObjectWithTag("MainCamera");
		curPos = this.gameObject.transform.position;

		if(Vector3.Distance(curPos - LastPos,new Vector3(0f,0f,0f)) <=0.1) {
			ifMoving=false;
		}
		else{
			ifMoving=true;
		}
		LastPos = curPos;

        if(_playerInput.HeroMoveForwardInput > float.Epsilon && CanMove){

			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

            //transform.Translate(moveDirection * Time.deltaTime* movespeed, Space.World);
            _rigidbody.position += moveDirection * Time.deltaTime * movespeed;
        }

		if(_playerInput.HeroMoveHorizontalInput < -1 * float.Epsilon && CanMove){

			Vector3 moveDirection = Maincamera.transform.right;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

            //transform.Translate(-moveDirection * Time.deltaTime* sidespeed, Space.World);
            _rigidbody.position += -moveDirection * Time.deltaTime * sidespeed;
        }
		if(_playerInput.HeroMoveHorizontalInput > float.Epsilon && CanMove){

			Vector3 moveDirection = Maincamera.transform.right;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

			//transform.Translate(moveDirection * Time.deltaTime* sidespeed, Space.World);
            _rigidbody.position += moveDirection * Time.deltaTime * sidespeed;
        }
		if(_playerInput.HeroMoveForwardInput < -1 * float.Epsilon && CanMove){
			Vector3 moveDirection = Maincamera.transform.forward;
			moveDirection.y = 0.0f;
			Vector3.Normalize(moveDirection);

            //transform.Translate(-moveDirection * Time.deltaTime* backwardspeed, Space.World);
            _rigidbody.position += -moveDirection * Time.deltaTime * backwardspeed;
        }
        
	}

	static public void EnableMove(){
		CanMove = true;
	}

	static public void DisableMove(){
		CanMove = false;
	}
}

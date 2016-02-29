using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayerInput : NetworkBehaviour {

    private NetworkPlayerObject _networkPlayer;

	void Start () {
        _networkPlayer = gameObject.GetComponent<NetworkPlayerObject>();
	}
	
	public float HeroMoveHorizontalInput {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetKey(KeyCode.A)) return -1;
            if (Input.GetKey(KeyCode.D)) return 1;
            return 0;
        }
    }

    public float HeroMoveForwardInput {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetKey(KeyCode.W)) return 1;
            if (Input.GetKey(KeyCode.S)) return -1;
            return 0;
        }
    }

    public float HeroMeleeAttackInput {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetMouseButton(0)) return 1;
            return 0;
        }
    }

    public float HeroMeleeAttackInputDown {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetMouseButtonDown(0)) return 1;
            return 0;
        }
    }

    public float HeroMeleeAttackInputUp {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetMouseButtonUp(0)) return 1;
            return 0;
        }
    }

    public float HeroMeleeChargeAttackInput {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetMouseButton(1)) return 1;
            return 0;
        }
    }

    public float HeroMeleeChargeAttackInputDown {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetMouseButtonDown(1)) return 1;
            return 0;
        }
    }

    public float HeroMeleeChargeAttackInputUp {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetMouseButtonUp(1)) return 1;
            return 0;
        }
    }

    public float HeroMeleeSuperInput {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetKey(KeyCode.R)) return 1;
            return 0;
        }
    }

    public float HeroMeleeSuperInputDown {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetKeyDown(KeyCode.R)) return 1;
            return 0;
        }
    }

    public float HeroMeleeSuperInputUp {
        get {
            if (!isLocalPlayer) return 0;

            if (Input.GetKeyUp(KeyCode.R)) return 1;
            return 0;
        }
    }
}

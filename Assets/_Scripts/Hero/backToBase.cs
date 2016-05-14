using UnityEngine;
using System.Collections;

public class backToBase : MonoBehaviour {
	
	private GameObject Mage_birthplace;
	private GameObject Knight_birthplace;
	private GameObject Arch_birthplace;
	private bool back;

	// Use this for initialization
	void Start () {
		back = true;
		Mage_birthplace = GameObject.Find("MageSummonPoint");
		Knight_birthplace = GameObject.Find("KnightSummonPoint");
		Arch_birthplace = GameObject.Find("ArchSummonPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (RoundManager.Instance.IsPreround() == true && back) {

			if (this.gameObject.name == "Mage(Clone)")
			{
				this.gameObject.transform.position = Mage_birthplace.transform.position;

			}

			else if (this.gameObject.name == "Knight(Clone)")
			{
				this.gameObject.transform.position = Knight_birthplace.transform.position;

			}

			else if (this.gameObject.name == "Archer(Clone)")
			{
				this.gameObject.transform.position = Arch_birthplace.transform.position;

			}
			back = false;
		}

		if (RoundManager.Instance.IsRound() == true && !back) {
			back = true;
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	int heroWin = EndGameHelper.heroWin;
	public Text words = null;

	// Use this for initialization
	void Start () {

		if (heroWin == 1) {
			HeroWin();
		} else if (heroWin == 2) {
			OverseerWin();
		}
	
	}

	void HeroWin() {
		words.text = "Hero Win";
	}

	void OverseerWin() {
		words.text = "Overseer Win";
	}

	void backToStart() {
		Application.LoadLevel(0);
	}

}

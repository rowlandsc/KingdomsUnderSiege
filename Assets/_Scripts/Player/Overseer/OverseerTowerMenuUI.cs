using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverseerTowerMenuUI : MonoBehaviour {

    public Button ShowTowerMenuButton;
    public RectTransform SlidingMenu;
    public Button ArcherTowerButton;
    public Button MortarTowerButton;
    public Button MageTowerButton;
    public float SlideSpeed = 350;
    public float InDistance = 350;

    public TowerPlacer TowerPlacer;

    private bool slidOut = false;

	void Start () {
        ShowTowerMenuButton.onClick.AddListener(OnSelectShowButton);
        ArcherTowerButton.onClick.AddListener(OnSelectArcherButton);
        MortarTowerButton.onClick.AddListener(OnSelectMortarButton);
        MageTowerButton.onClick.AddListener(OnSelectMageButton);
	}
	
	void Update () {
	    
	}

    public void OnSelectShowButton() {
        slidOut = !slidOut;
        if (slidOut)
            StartCoroutine(SlideOut());
        else {
            TowerPlacer.StopPlacingTower();
            StartCoroutine(SlideIn());
        }
    }

    public void OnSelectArcherButton() {
        TowerPlacer.StopPlacingTower();
        TowerPlacer.StartPlacingTower("TowerArcher1");
    }

    public void OnSelectMortarButton() {
        TowerPlacer.StopPlacingTower();
    }

    public void OnSelectMageButton() {
        TowerPlacer.StopPlacingTower();
    }

    IEnumerator SlideOut() {
        Debug.Log("Sliding out");
        while (slidOut && SlidingMenu.offsetMin.x > 0) {
            SlidingMenu.offsetMin = new Vector2(SlidingMenu.offsetMin.x - SlideSpeed * Time.deltaTime, SlidingMenu.offsetMin.y);
            SlidingMenu.offsetMax = new Vector2(SlidingMenu.offsetMax.x - SlideSpeed * Time.deltaTime, SlidingMenu.offsetMax.y);
            if (SlidingMenu.offsetMin.x < 0) SlidingMenu.offsetMin = new Vector2(0, SlidingMenu.offsetMin.y);
            if (SlidingMenu.offsetMax.x < 0) SlidingMenu.offsetMax = new Vector2(0, SlidingMenu.offsetMax.y);
            yield return null;
        }
    }

    IEnumerator SlideIn() {
        Debug.Log("Sliding in");
        while (!slidOut && SlidingMenu.offsetMin.x < InDistance) {
            SlidingMenu.offsetMin = new Vector2(SlidingMenu.offsetMin.x + SlideSpeed * Time.deltaTime, SlidingMenu.offsetMin.y);
            SlidingMenu.offsetMax = new Vector2(SlidingMenu.offsetMax.x + SlideSpeed * Time.deltaTime, SlidingMenu.offsetMax.y);
            if (SlidingMenu.offsetMin.x > InDistance) SlidingMenu.offsetMin = new Vector2(InDistance, SlidingMenu.offsetMin.y);
            if (SlidingMenu.offsetMax.x > InDistance) SlidingMenu.offsetMax = new Vector2(InDistance, SlidingMenu.offsetMax.y);
            yield return null;
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverseerTowerUpgradeUI : MonoBehaviour {

    public RectTransform MenuRect;
    public Text TowerNameField;

    public Text HealthField;
    public Text AttackField;
    public Text SpeedField;
    public Text FrequencyField;
    public Text RangeField;

    public Text HealthIncreaseField;
    public Text AttackIncreaseField;
    public Text SpeedIncreaseField;
    public Text FrequencyIncreaseField;
    public Text RangeIncreaseField;

    public Text UpgradeLabelField;
    public Text UpgradeTextField;

    public ObjectSelector ObjectSelector;


    private bool slidOut = false;

	void Start () {
        

        
    }

    void OnEnable() {
        ObjectSelector.eOnNewSelection += OnNewSelection;
    }

    void OnDisable() {
        ObjectSelector.eOnNewSelection -= OnNewSelection;
    }
	
	void Update () {
        
	}
    
    void OnNewSelection(GameObject selectedObject) {
        Tower tower = (selectedObject != null) ? selectedObject.GetComponent<Tower>() : null;
        if (tower != null) {
            MenuRect.gameObject.SetActive(true);
            if (tower.GetComponent<TowerAttack>()) {
                TowerNameField.text = "Archer Tower";
            }
            else if (tower.GetComponent<MagicTowerAttack>()) {
                TowerNameField.text = "Magic Tower";
            }
            else if (tower.GetComponent<MortarTowerAttack>()) {
                TowerNameField.text = "Mortar Tower";
            }

            ProfileSystem profileSystem = tower.GetComponent<ProfileSystem>();
            HealthField.text = profileSystem.MaxHealthPoints.ToString();
            AttackField.text = profileSystem.MeleeDamageDealt.ToString();
            SpeedField.text = profileSystem.AttackSpeed.ToString();
            FrequencyField.text = profileSystem.AttackFrequency.ToString();
            RangeField.text = profileSystem.AttackRange.ToString();

        }
        else {
            MenuRect.gameObject.SetActive(false);
        }
    }
}

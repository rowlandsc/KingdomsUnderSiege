using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public Canvas optionsMenu;
    public Button playText;
    public Button optionsText;
    public Button quitMenuText;

	// Use this for initialization
	void Start ()
    {
        optionsMenu = optionsMenu.GetComponent<Canvas>();
        playText = playText.GetComponent<Button>();
        optionsText = optionsText.GetComponent<Button>();
        quitMenuText = quitMenuText.GetComponent<Button>();

        optionsMenu.enabled = false;

	}
	
    public void StartLevel()
    {
        Application.LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OptionsPress()
    {
        optionsMenu.enabled = true;
    }

    public void CloseMenuPress()
    {
        optionsMenu.enabled = false;
    }
}

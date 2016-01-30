using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button playTextBtn;

    public Canvas optionsMenu;
    public Button optionsTextBtn; 
    public Button quitOptionsMenuBtn;
    private bool _optionsOpen = false;

    public Canvas multiplayerMenu;
    public Button hostGameBtn;
    public Button connectToBtn;
    public Button quitMuliBtn;
    public InputField ipAddressField;
    
	/// <summary>
    /// Use this for initialization.
    /// </summary>
	void Start ()
    {
        playTextBtn = playTextBtn.GetComponent<Button>();
        optionsMenu = optionsMenu.GetComponent<Canvas>();
        optionsTextBtn = optionsTextBtn.GetComponent<Button>();
        quitOptionsMenuBtn = quitOptionsMenuBtn.GetComponent<Button>();
        optionsMenu.enabled = false;

        multiplayerMenu = multiplayerMenu.GetComponent<Canvas>();
        hostGameBtn = hostGameBtn.GetComponent<Button>();
        connectToBtn = connectToBtn.GetComponent<Button>();
        quitMuliBtn = quitMuliBtn.GetComponent<Button>();
        multiplayerMenu.enabled = false;
        ipAddressField = ipAddressField.GetComponent<InputField>();

	}

	/// <summary>
    /// Closes the application.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Actions for opening and closing the options button.
    /// </summary>
    public void OptionsPress()
    {
        if(_optionsOpen == false)
        {
            optionsMenu.enabled = true;
            _optionsOpen = true;

            multiplayerMenu.enabled = false;
        }
        else
        {
            optionsMenu.enabled = false;
            _optionsOpen = false;
        }
    }

    /// <summary>
    /// Pressed the close menu button. Closes the options menu.
    /// </summary>
    public void CloseMenuPress()
    {
        optionsMenu.enabled = false;
        _optionsOpen = false;   
    }

    /// <summary>
    /// Pressed play button. Brings you to the multiplayer menu.
    /// </summary>
    public void PlayPress()
    {
        multiplayerMenu.enabled = true;
    }

    /// <summary>
    /// Closes the multiplayer menu
    /// </summary>
    public void CloseMultiPress()
    {
        multiplayerMenu.enabled = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ConnectToPress()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void ipAddressEntered()
    {

    }
}

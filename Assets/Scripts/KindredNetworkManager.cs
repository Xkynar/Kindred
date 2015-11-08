using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class KindredNetworkManager : NetworkManager 
{
    private Button hostBtn;
    private Button joinBtn;
    private Button p1Btn;
    private Button p2Btn;
    private Button specBtn;
    private Text ipAddress;
    private Text stateInfo;
    private InputField playerNameInput;

    void Start()
    {
        SetMenuReferences();
        CheckPlayerPrefs();
    }

    /*
     * Starts up a new server.
     */
    public void CreateHost()
    {
        SetPlayerName();
        SetPort();
        DisplayInfo("The game will be starting in a moment...");

        NetworkManager.singleton.StartHost();
    }

    /*
     * Attempts to join a server.
     */
    public void JoinGame()
    {
        SetPlayerName();
        SetIpAddress();
        SetPort();
        DisplayInfo("You will be in the game in a moment...");
        NetworkManager.singleton.StartClient();
    }

    /*
     * 
     */
    void SetIpAddress()
    {
        NetworkManager.singleton.networkAddress = ipAddress.text;
    }

    /*
     * 
     */
    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    /*
     * Saves the player's name so it can passed onto other scenes.
     */
    public void SetPlayerName()
    {
        string playerName = playerNameInput.text;
        PlayerPrefs.SetString("nickname", playerName);
    }

    /*
     * Saves the player's role so it can passed onto other scenes.
     */
    public void SetPlayerRole(string role)
    {
        PlayerPrefs.SetString("role", role);
    }

    /*
     * Creates references to all elements in the menu so they can be accessed anywhere.
     */
    void SetMenuReferences()
    {
        hostBtn = GameObject.Find("HostBtn").GetComponent<Button>();
        joinBtn = GameObject.Find("JoinBtn").GetComponent<Button>();
        p1Btn = GameObject.Find("P1Btn").GetComponent<Button>();
        p2Btn = GameObject.Find("P2Btn").GetComponent<Button>();
        specBtn = GameObject.Find("SPECBtn").GetComponent<Button>();
        ipAddress = GameObject.Find("AddressInput").transform.FindChild("Text").GetComponent<Text>();
        stateInfo = GameObject.Find("StateInfo").GetComponent<Text>();
        playerNameInput = GameObject.Find("PlayerNameInput").GetComponent<InputField>();
    }

    /*
     * Checks if the player has already set a name before. If he has, the name is pre-selected.
     */
    void CheckPlayerPrefs()
    {
        string storedName = PlayerPrefs.GetString("nickname");

        if (storedName != "")
        {
            playerNameInput.text = storedName;
        }
    }

    /*
     * Used to display general state info and network warnings.
     */
    void DisplayInfo(string info)
    {
        stateInfo.text = info;
        stateInfo.enabled = true;
    }

    /*
     * Called when a new scene is loaded (as this has "Don't Destroy On Load").
     * Note that this is NOT called the very first time the script starts, but only when 
     * changing scenes (after disconnecting and returning to the menu, for example).
     */
    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            StartCoroutine(SetupMenuScene());
        }
        else
        {
            StartCoroutine(SetupGeneralScenes());
        }
    }

    /*
     * Sets up all references in the menu scene.
     * This needs to be coded, and not just linked in the editor, as editor links 
     * are lost on scene changes and Start() does not run.
     */
    IEnumerator SetupMenuScene()
    {
        // when returning to the menu, UNET's singleton handler needs a bit of time to handle 
        // the two NetworkManagers (the new one, and the previous one that should be deleted)
        yield return new WaitForSeconds(0.3f);

        SetMenuReferences();
        CheckPlayerPrefs();

        hostBtn.onClick.RemoveAllListeners();
        hostBtn.onClick.AddListener(CreateHost);

        joinBtn.onClick.RemoveAllListeners();
        joinBtn.onClick.AddListener(JoinGame);

        p1Btn.onClick.RemoveAllListeners();
        p1Btn.onClick.AddListener(delegate { SetPlayerRole("P1"); });

        p2Btn.onClick.RemoveAllListeners();
        p2Btn.onClick.AddListener(delegate { SetPlayerRole("P2"); });

        specBtn.onClick.RemoveAllListeners();
        specBtn.onClick.AddListener(delegate { SetPlayerRole("SPEC"); });
    }

    /*
     * Used to setup references in other scenes (as this has "Don't Destroy On Load").
     * Could be used to setup a Disconnect button, for example.
     */
    IEnumerator SetupGeneralScenes()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
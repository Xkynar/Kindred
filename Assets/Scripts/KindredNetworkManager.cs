using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class KindredNetworkManager : NetworkManager 
{
    void Start()
    {
        InputField playerNameInput = GameObject.Find("PlayerNameInput").GetComponent<InputField>();
        string storedName = PlayerPrefs.GetString("nickname");

        if (storedName != "")
        {
            playerNameInput.text = storedName;
        }
    }

    public void CreateHost()
    {
        SetPlayerName();
        SetPort();
        DisplayInfo("The game will be starting in a moment...");

        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetPlayerName();
        SetIpAddress();
        SetPort();
        DisplayInfo("You will be in the game in a moment...");

        NetworkManager.singleton.StartClient();
    }

    void SetIpAddress()
    {
        string ipAddress = GameObject.Find("AddressInput").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    public void SetPlayerName()
    {
        string storedName = PlayerPrefs.GetString("nickname");

        Debug.Log("storedName: " + storedName);

        string playerName = GameObject.Find("PlayerNameInput").transform.FindChild("Text").GetComponent<Text>().text;
        PlayerPrefs.SetString("nickname", playerName);
    }

    public void SetPlayerRole(string role)
    {
        PlayerPrefs.SetString("role", role);
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

        Button hostBtn = GameObject.Find("HostBtn").GetComponent<Button>();
        Button joinBtn = GameObject.Find("JoinBtn").GetComponent<Button>();
        Button p1Btn = GameObject.Find("P1Btn").GetComponent<Button>();
        Button p2Btn = GameObject.Find("P2Btn").GetComponent<Button>();
        Button specBtn = GameObject.Find("SPECBtn").GetComponent<Button>();
        Text playerName = GameObject.Find("PlayerNameInput").transform.FindChild("Text").GetComponent<Text>();

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

        // @TODO handle nickname in playerprefs
    }

    /*
     * Used to setup references in other scenes (as this has "Don't Destroy On Load").
     * Could be used to setup a Disconnect button, for example.
     */
    IEnumerator SetupGeneralScenes()
    {
        yield return new WaitForSeconds(0.3f);
    }

    /*
     * Used to display general state info and network warnings.
     */
    void DisplayInfo(string info)
    {
        Text stateInfo = GameObject.Find("StateInfo").GetComponent<Text>();
        stateInfo.text = info;
        stateInfo.enabled = true;
    }
}
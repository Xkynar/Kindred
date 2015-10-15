using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance = null;

    private PlayerNetworkManager localPlayer;

    [SerializeField] GameObject readyButton;
    [SerializeField] GameObject ARCamera;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetLocalPlayer(PlayerNetworkManager localPlayer)
    {
        this.localPlayer = localPlayer;
    }

    /*
     * Displays a "Ready" button on the main HUD. Called by the local player.
     */
    public void ShowReadyButton()
    {
        this.readyButton.SetActive(true);
    }

    /*
     * Callback for the "Ready" button clicked event. Alerts the local player to update its copies.
     */
    public void SetPlayerReady(bool ready)
    {
        localPlayer.SetPlayerReady(ready);
        SetMonsters();
    }

    /*
     * Sets monster ownership for local player. 
     */
    public void SetMonsters()
    {
        List<GameObject> imageTargets = ARCamera.GetComponent<TrackableList>().GetImageTargets();
       
        foreach (GameObject imageTarget in imageTargets)
        {
            MonsterController monsterController = imageTarget.GetComponent<MonsterController>();

            if (monsterController != null)
            {
                monsterController.SetMine(true);
            }
        }
    }
}

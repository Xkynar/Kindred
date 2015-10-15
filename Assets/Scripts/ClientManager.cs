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

    public void ShowReadyButton()
    {
        this.readyButton.SetActive(true);
    }

    public void SetPlayerReady(bool ready)
    {
        localPlayer.SetPlayerReady(ready);
        SetMonsters();
    }

    public void SetMonsters()
    {
        List<GameObject> imageTargets = ARCamera.GetComponent<TrackableList>().GetImageTargets();

        foreach (GameObject imageTarget in imageTargets)
        {
            MonsterController monsterController = GetComponent<MonsterController>();

            if (monsterController != null)
            {
                monsterController.SetMine(true);
            }
        }
    }
}

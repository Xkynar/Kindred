using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour 
{
    private PlayerNetworkManager playerNetworkManager;

    public void setPlayerNetworkManager(PlayerNetworkManager playerNetworkManager)
    {
        this.playerNetworkManager = playerNetworkManager;
    }

    void OnMouseDown()
    {
        // gameManager.monsterClicked(this, playerNetworkManager.isLocalPlayer);
    }
}

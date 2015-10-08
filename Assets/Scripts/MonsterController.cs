using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour 
{
    private PlayerNetworkManager playerNetworkManager;

	void Start()
    {
        
	}

    public void setPlayerNetworkManager(PlayerNetworkManager playerNetworkManager)
    {
        this.playerNetworkManager = playerNetworkManager;
    }

    void OnMouseDown()
    {
        if (playerNetworkManager.isLocalPlayer)
            Debug.Log("Mine");
        else
            Debug.Log("Not Mine");
    }
}

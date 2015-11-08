using UnityEngine;
using System.Collections;
using Vuforia;

public class ArenaVirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler {

	// Use this for initialization
	void Start ()
    {
        // Search for all Children from this ImageTarget with type VirtualButtonBehaviour
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            // Register with the virtual buttons TrackableBehaviour
            vbs[i].RegisterEventHandler(this);
        }
	}

    void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("pressed");
    }

    void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("released");
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class TrackableList : MonoBehaviour
{
    void CheckTargets()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Iterate through the list of active trackables
        Debug.Log("List of trackables currently active (tracked): ");
        int numFrameMarkers = 0;
        int numImageTargets = 0;
        int numMultiTargets = 0;
        int numObjectTargets = 0;
        foreach (TrackableBehaviour tb in activeTrackables)
        {
            Debug.Log("Trackable: " + tb.TrackableName);

            if (tb is MarkerBehaviour)
                numFrameMarkers++;
            else if (tb is ImageTargetBehaviour)
                numImageTargets++;
            else if (tb is MultiTargetBehaviour)
                numMultiTargets++;
            else if (tb is ObjectTargetBehaviour)
                numObjectTargets++;
        }

        // Debug.Log("Found " + numFrameMarkers + " frame markers in curent frame");
        Debug.Log("Found " + numImageTargets + " image targets in curent frame");
        // Debug.Log("Found " + numMultiTargets + " multi-targets in curent frame");
        // Debug.Log("Found " + numObjectTargets + " object-targets in current frame");
    }
}

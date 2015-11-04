using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class TrackableList : MonoBehaviour
{
    /*
     * Returns a list of all the image targets in the scene as GameObjects, whether they're active or not.
     */
    public List<GameObject> GetImageTargets()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetTrackableBehaviours();
        List<GameObject> imageTargets = new List<GameObject>();

        foreach (TrackableBehaviour tb in activeTrackables)
        {
            if (tb is ImageTargetBehaviour)
            {
                imageTargets.Add(tb.gameObject);
            }
        }

        return imageTargets;
    }

    /*
     * Returns a list of all the currently active image targets as GameObjects
     */
    public List<GameObject> GetActiveImageTargets()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();
        List<GameObject> imageTargets = new List<GameObject>();

        foreach (TrackableBehaviour tb in activeTrackables)
        {
            if (tb is ImageTargetBehaviour)
            {
                imageTargets.Add(tb.gameObject);
            }
        }

        return imageTargets;
    }
}

using UnityEngine;
using System.Collections;

public class ARTouchController : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                hit.transform.gameObject.SendMessage("OnMouseDown");
            }
        }
	}
}

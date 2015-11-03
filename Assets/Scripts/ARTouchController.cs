using UnityEngine;
using System.Collections;

public class ARTouchController : MonoBehaviour
{
	void Update ()
    {
        RaycastHit hit = new RaycastHit();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Monster")
                {
                    hit.transform.parent.gameObject.SendMessage("OnMouseDown");
                }
                else
                {
                    hit.transform.gameObject.SendMessage("OnMouseDown");
                }
            }
        }
	}
}

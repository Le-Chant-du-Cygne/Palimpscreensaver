using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private Vector3 lastMousePosition;

	void Start ()
    {
        Cursor.visible = false;
        lastMousePosition = Input.mousePosition;
	}
	
	void Update ()
    {
	    if (Input.anyKeyDown || Input.mousePosition != lastMousePosition || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.mouseScrollDelta.magnitude > 0f)
        {
            Application.Quit();
        }
        lastMousePosition = Input.mousePosition;
	}
}

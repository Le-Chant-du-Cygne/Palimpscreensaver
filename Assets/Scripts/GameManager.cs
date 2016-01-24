using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	void Start ()
    {
        Cursor.visible = false;
	}
	
	void Update ()
    {
	    if (Input.anyKeyDown)
        {
            Application.Quit();
        }
	}
}

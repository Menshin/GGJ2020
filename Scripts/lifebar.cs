using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lifebar : MonoBehaviour
{
	public int i = 0;

    public void Addbutton(string text)
    {
    	if (GUI.Button(new Rect(1, 1 + 30 * i++, 200, 30), text))
            Debug.Log("Clicked the button with text");
    }

}

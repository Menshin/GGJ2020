using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
	public int value;

    // Update is called once per frame
    void Update()
    {
        if (value > 100)
        	value = 100;
    }
}

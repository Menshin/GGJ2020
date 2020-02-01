using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
	public Vector3 mousepos;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
        {
        	mousepos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousepos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
            	Debug.Log(Input.mousePosition);
                transform.position = (Input.mousePosition);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
        	if (mousepos.x > Input.mousePosition.x)
        		Debug.Log("Proie !");
        	else if (mousepos.x > Input.mousePosition.x)
        		Debug.Log("Predateur !");
            transform.position = Vector3.zero;
        }
    }
}

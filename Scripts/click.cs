using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
	public Vector3 mousepos;

    public static Cell cellselect;

    public Planet planet;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-100, -100, -100);
    }



    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("space"))
        {
        	mousepos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousepos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                 planet.biomass = 0;
                 planet.slider.value = planet.biomass / planet.biomassMax;
            	//Debug.Log(Input.mousePosition);
                transform.position = (Input.mousePosition);

                cellselect = hit.collider.gameObject.GetComponent<Cell>();

            }

        }
        if (Input.GetKeyUp("space"))
        {
            transform.position = new Vector3(-100, -100, -100);
        }
    }
}

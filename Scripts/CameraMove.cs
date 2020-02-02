using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    public Vector3 click_pos;
    public Vector3 transit = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        click_pos = Vector3.zero;
    }

    void MoveFunction()
    {
        transform.position = Vector3.Lerp(transform.position, transit, 2f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        // If the object has arrived, stop the coroutine
        if (Vector3.Distance(transform.position, transit) < 0.1f)
            transit = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 vec = Vector3.zero;
        Vector3 mousepos = Vector3.zero;
        float speed = 90.0f;
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                ray = new Ray(Vector3.zero, hit.point);
                if (Input.GetMouseButtonDown(1))
                    transit = ray.GetPoint(2.0f);
                Debug.Log(hit.collider.gameObject);
            }
        }

        if (Input.GetMouseButton (2))
        {
            transit = Vector3.zero;
            transform.RotateAround(Vector3.zero, Vector3.up, 500.0f * Input.GetAxis("Mouse X") * Time.deltaTime);
            transform.RotateAround(Vector3.zero, transform.right, 500.0f * -Input.GetAxis("Mouse Y") * Time.deltaTime);
        }
        if (transit != Vector3.zero)
            StartCoroutine("MoveFunction");
        if (Input.GetKey("left"))
            vec += Vector3.up;
        if (Input.GetKey("right"))
        	vec += Vector3.down;
        if (Input.GetKey("up"))
        	vec += transform.right;
        if (Input.GetKey("down"))
        	vec += -transform.right;
        transform.RotateAround(Vector3.zero, vec, speed * Time.deltaTime);
    }
}

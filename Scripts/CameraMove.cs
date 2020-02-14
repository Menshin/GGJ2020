using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    public Vector3 click_pos;
    public Vector3 transit = Vector3.zero;

    public float speed = 90.0f;
    public float maxZoomInLevel = 2f;
    public float maxZoomOutLevel = 5f;

    // Start is called before the first frame update
    void Start()
    {
        click_pos = Vector3.zero;
    }

    void MoveFunction()
    {
        transform.position = Vector3.Lerp(transform.position, transit, 2f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        UIManager.S.clickMenuScript.UpdateScaling();
        // If the object has arrived, stop the coroutine
        if (Vector3.Distance(transform.position, transit) < 0.1f)
            transit = Vector3.zero;
    }

    public void Update()
    {
        Vector3 vec = Vector3.zero;

        //middle mouse button rotation
        if (Input.GetMouseButton(2))
        {
            transit = Vector3.zero;
            transform.RotateAround(Vector3.zero, Vector3.up, 500.0f * Input.GetAxis("Mouse X") * Time.deltaTime);
            transform.RotateAround(Vector3.zero, transform.right, 500.0f * -Input.GetAxis("Mouse Y") * Time.deltaTime);
            UIManager.S.HideClickMenu();
        }

        //zoom in/out
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            transit = Vector3.zero;
            Vector3 vect = Vector3.zero - transform.position;
            if (vect.magnitude > maxZoomInLevel)
            {

                vect.Scale(new Vector3(0.1f, 0.1f, 0.1f));
                transform.Translate(vect, Space.World);
                UIManager.S.clickMenuScript.UpdateScaling();
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            transit = Vector3.zero;
            Vector3 vect = transform.position - Vector3.zero;
            if (vect.magnitude < maxZoomOutLevel)
            {
                vect.Scale(new Vector3(0.1f, 0.1f, 0.1f));
                transform.Translate(vect, Space.World); 
                UIManager.S.clickMenuScript.UpdateScaling();
            }
        }

        //key movement
        if (Input.GetKey("left"))
            vec += Vector3.up;
        if (Input.GetKey("right"))
            vec += Vector3.down;
        if (Input.GetKey("up"))
            vec += transform.right;
        if (Input.GetKey("down"))
            vec += -transform.right;
        transform.RotateAround(Vector3.zero, vec, speed * Time.deltaTime);

        //camera travelling
        if (transit != Vector3.zero)
            StartCoroutine("MoveFunction");
    }

    public void HandleRightClick()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10.0f))
        {

            ray = new Ray(Vector3.zero, hit.point);
            transit = ray.GetPoint(2.0f);
            UIManager.S.HideClickMenu();
        }
    }
}

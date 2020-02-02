using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float panningSpeed;
    float m_FieldOfView;
    public string m_LabelX;
    public string m_LabelY;
    // Start is called before the first frame update
    void Start()
    {
        panningSpeed = 5.0f;
        m_FieldOfView = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.fieldOfView = m_FieldOfView;
        m_LabelX = transform.position.x.ToString();
        m_LabelY = transform.position.y.ToString();

        if (Input.GetKey("right"))
            transform.position = transform.position + new Vector3(panningSpeed * Time.deltaTime, 0f, 0f);
        else if (Input.GetKey("left"))
            transform.position = transform.position + new Vector3(-panningSpeed * Time.deltaTime, 0f, 0f);

        else if (Input.GetKey("up"))
            transform.position = transform.position + new Vector3(0f, panningSpeed * Time.deltaTime, 0f);
        else if (Input.GetKey("down"))
            transform.position = transform.position + new Vector3(0f, -panningSpeed * Time.deltaTime, 0f);
    }
    void OnGUI()
    {
        //Set up the maximum and minimum values the Slider can return (you can change these)
        float max, min;
        max = 100.0f;
        min = 10.0f;
        //This Slider changes the field of view of the Camera between the minimum and maximum values
        m_FieldOfView = GUI.HorizontalSlider(new Rect(20, 20, 100, 40), m_FieldOfView, min, max);

        //GUI.Label(new Rect(20, 70, 150, 40), "(" + m_LabelX + ", " + m_LabelY + ")");
    }

}

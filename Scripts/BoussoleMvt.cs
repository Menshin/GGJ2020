using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoussoleMvt : MonoBehaviour
{
    public float borneMax = 0.4f;
    public float borneMin = 0.35f;
    public float pos = 0.0f;

    private bool dir = true;

    public float rotationSpeed = 5f;
    public float translationSpeed = 0.01f;

    void Start()
    {
        transform.Translate(new Vector3(0, pos, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (dir)
        {
            transform.Translate(new Vector3(0, translationSpeed * Time.deltaTime, 0));
            pos += translationSpeed * Time.deltaTime;
            if (pos >= borneMax)
                dir = false;
        }
        else
        {
            transform.Translate(new Vector3(0, -translationSpeed * Time.deltaTime, 0));
            pos -= translationSpeed * Time.deltaTime;
            if (pos <= borneMin)
                dir = true;
        }

        transform.Rotate(new Vector3(0.0f, rotationSpeed, 0.0f));
    }
}
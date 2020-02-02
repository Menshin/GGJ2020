using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
	public float time;
    int totalsec;
    public string timetext;
    // Update is called once per frame
    void Start()
    {
    	timetext = "00:00";
    }
    void Update()
    {
    	time += Time.deltaTime;
    	totalsec = (int)time;
	 	GetComponent<Text>().text = (totalsec / 60 / 10).ToString() + (totalsec / 60 % 10).ToString()
	 	 + ":" + (totalsec % 60 / 10).ToString() + (totalsec % 10).ToString();

    }
}

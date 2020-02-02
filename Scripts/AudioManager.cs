using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Planet planet;

	AudioSource animalAudioSource;
	AudioSource themeAudioSource;


	public AudioClip[] clips;

	bool play = true;
	bool changePiste = false;

    void Start()
    {
        animalAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (play && !changePiste)
    	{
    		animalAudioSource.Play();
    	}
    	else
    	{
    		animalAudioSource.Stop();
    		changePiste = false;
    	}
    }

    public void ChangeAudio(int piste)
    {
    	/*if (piste < clips.Length)
    	{
     		changePiste = true;
     		AudioSource.clip = clips[piste];

    	}
    	else
    	{
    		Debug.Log("depassement de liste de pistes");
    	}
 */
    }
}

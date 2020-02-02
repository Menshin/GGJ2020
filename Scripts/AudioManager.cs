using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Cell planet;

	AudioSource biomeAudioSource;
	AudioSource themeAudioSource;


	public AudioClip[] clips;
	public AudioClip[] themeClips;

	bool play = true;
	bool playTheme = true;

	bool changePisteTheme = false;
	bool changePiste = false;

	int actualTheme;
	int newTheme;

    void Start()
    {
        biomeAudioSource = gameObject.AddComponent<AudioSource>();
        biomeAudioSource.loop = false;
		biomeAudioSource.volume = 1f;
        themeAudioSource = gameObject.AddComponent<AudioSource>();
        themeAudioSource.loop = true;
        themeAudioSource.volume = 0.1f;
        themeAudioSource.clip = themeClips[0];
        actualTheme = 0;
        newTheme = 0;
        themeAudioSource.Play();
       /* ChangeTheme(1);*/


    }

    // Update is called once per frame
    void Update()
	{
		if (Input.GetKey("z"))
			Cell.cellAlive++;
		if (Cell.cellAlive < Cell.cellTotal / 5)
			newTheme = 0;
		else if (Cell.cellAlive < (Cell.cellTotal / 5) * 2)
			newTheme = 1;
		else if (Cell.cellAlive < (Cell.cellTotal / 5) * 3)
			newTheme = 2;
		else if (Cell.cellAlive < (Cell.cellTotal / 5) * 4)
			newTheme = 3;
		else
			newTheme = 4;
		if (newTheme != actualTheme)
		{
			ChangeTheme(newTheme);
		}

    }


    public void LaunchBiomeAudio(int piste, bool alive)
    {
    	int numpiste = alive ? 0 : 6;
    	numpiste += piste;
		biomeAudioSource.clip = clips[numpiste];
		biomeAudioSource.Play();
    }

    /* 0 soft 1... */
    public void ChangeTheme(int piste)
    {
    	themeAudioSource.clip = themeClips[piste];
    	actualTheme = piste;
    	themeAudioSource.Play();
    }
}

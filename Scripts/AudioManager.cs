using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	static public AudioManager S; //singleton
	
	public Planet planet;

	AudioSource biomeAudioSource;
	AudioSource themeAudioSource;

	public AudioClip[] clips;
	public AudioClip[] themeClips;

	/*bool play = true;
	bool playTheme = true;

	bool changePisteTheme = false;
	bool changePiste = false;*/

	int actualTheme;
	int newTheme;

	private void Awake()
	{
		if (S)
		{
			Debug.Log("ERROR: shouldn't be instantiating more than one AudioManager singleton");
		}

		S = this;
	}

	void Start()
    {
		planet = Planet.S;
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

    void Update()
	{
		if (planet.cellsAlive < planet.cellsTotal / 5)
			newTheme = 0;
		else if (planet.cellsAlive < (planet.cellsTotal / 5) * 2)
			newTheme = 1;
		else if (planet.cellsAlive < (planet.cellsTotal / 5) * 3)
			newTheme = 2;
		else if (planet.cellsAlive < (planet.cellsTotal / 5) * 4)
			newTheme = 3;
		/*else
			newTheme = 4;*/


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

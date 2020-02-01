using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class button : MonoBehaviour
{
	public Texture btnTexture;
    // Start is called before the first frame update
    public void Load()
    {
        SceneManager.LoadScene("Mare", LoadSceneMode.Single);
    }
}

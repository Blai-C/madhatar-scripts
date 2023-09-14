using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroGameSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void gamemode1vs3()
    {
        PlayerPrefs.SetInt("GameMode", 0);
        Play();
    }
    public void gamemode2vs2()
    {
        PlayerPrefs.SetInt("GameMode", 1);
        Play();
    }

    public void howTo1()
    {
        SceneManager.LoadScene(6);
    }

    public void howTo12()
    {
        SceneManager.LoadScene(7);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Replay()
    {
        SceneManager.LoadScene(2);
    }
    public void SelectCar()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSceneControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;
    public Text time;
    int num_bots;
    public GameObject[] PCars;// 0 = PICKUP  1 = VAN  2 = YellowMustang  3 = RedMustang  4 = BUGGY
    public GameObject Image;
    public Sprite[] gasoline;
    public AudioClip[] Songs;
    AudioSource audiosource;
    int GameMode;
    public GameObject PlayerBot;
    public GameObject Bot;





    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audiosource = GameObject.FindGameObjectWithTag("Song").GetComponent<AudioSource>();
        audiosource.clip = Songs[2];
        audiosource.Play();
        GameMode = PlayerPrefs.GetInt("GameMode");
        if (GameMode == 0)
        {
            Destroy(PlayerBot);
        }
        else
        {
            Destroy(Bot);
        }




        if (!PlayerPrefs.HasKey("cotxeSel"))
        {
            PlayerPrefs.SetInt("cotxeSel", 2);
        }


        Instantiate(PCars[PlayerPrefs.GetInt("cotxeSel")], new Vector3(74f, 6.89f, 77f), Quaternion.identity);
        

        timer = 120.0f;

    }
    public void decLife(int life)
    {

        if (life >= 0)
        {
            Image.GetComponent<Image>().sprite = gasoline[life];
            audiosource.clip= Songs[life];
            audiosource.Play();


        }
      

    }
    // Update is called once per frame
    void Update()
    {
        int m = (int)(timer / 60);
        int s = (int)(timer - (m * 60));
        if (s < 10)
        {
            time.text = m + ":0" + s;
        }
        else
        {
            time.text = m + ":" + s;
        }


        timer -= Time.deltaTime;

        num_bots = GameObject.FindGameObjectsWithTag("Bot").Length;
        
        if (num_bots == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);
        }
        if (timer <= 0.0f)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            endgame();

        }
    }
    public void endgame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (GameMode==0) { 
        SceneManager.LoadScene(4);
                                  }
        if (GameMode == 1)
        {
            SceneManager.LoadScene(5);
        }
    }
  
}

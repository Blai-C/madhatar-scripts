using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] cars;
    public Camera cam;
    Vector3 offset;
    int car;
    GameObject carSelected;
    void Start()
    {
        car=0;
        offset=cam.transform.position - cars[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(0, cars[car].transform.eulerAngles.y + 0.1f, 0);
        cars[car].transform.eulerAngles = pos;
    }
    public void left()
    {
       
        if (car == 0)
        {
            car = cars.Length-1;
        }
        else
        {
            car--;
        }
        Quaternion pos = new Quaternion(0, 0, 0, 0);
        cars[car].transform.rotation = pos;
        cam.transform.position = cars[car].transform.position + offset;
    }
    public void right()
    {
       
        if (car == cars.Length-1)
        {
            car = 0;
        }
        else
        {
            car++;
        }
        Quaternion pos = new Quaternion(0, 0, 0, 0);
        cars[car].transform.rotation = pos;
        cam.transform.position = cars[car].transform.position + offset;
    }
    public void play()
    {
        carSelected = cars[car];
        PlayerPrefs.SetInt("cotxeSel", car);// 0 = PICKUP  1 = VAN  2 = YellowMustang  3 = RedMustang  4 = BUGGY
        SceneManager.LoadScene(2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerScript : MonoBehaviour
{
    // Start is called before the first frame update

    //AUdio
    AudioSource ASource;
   

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float yaw = 0.0f;
    public float pitch = 0.0f;

  
    public float minRot;
    public float maxRot;
    Quaternion rotQ;




    public float damage = 10f;
    public float range = 100f;
    public Camera cam;
    public ParticleSystem boom;
    public GameObject boomimpact;

    void Start()
    {
        ASource = GetComponent<AudioSource>();
        minRot = -15f;
        maxRot = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }

        rotQ.y += speedH * Input.GetAxis("Mouse X");
        rotQ.x += speedV * Input.GetAxis("Mouse Y");

        rotQ.x = Mathf.Clamp(rotQ.x, minRot, maxRot);

       
        transform.localRotation = Quaternion.Euler(rotQ.x, rotQ.y, rotQ.z);
       
    }

    void Shooting()
    {
        boom.Play();
        ASource.Play();
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit, range))
        {
            
            if (hit.transform.tag == "Bot") { 
            TargetScript target = hit.transform.GetComponent<TargetScript>();
            if (target != null)
            {
               target.damage(damage);
            }
            }
            else
            {
                PlayerStatsScript target = hit.transform.GetComponent<PlayerStatsScript>();
                if (target != null)
                {
                    target.damage(damage);
                }
            }
            GameObject impact = Instantiate(boomimpact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact,2f);
        }
    }
}

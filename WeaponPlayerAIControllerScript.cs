using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerAIControllerScript : MonoBehaviour
{
    AudioSource ASource;
    //

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float yaw = 0.0f;
    public float pitch = 0.0f;


    public float minRot;
    public float maxRot;
    Quaternion rotQ;

    //Target
    private GameObject[] players;
    private GameObject player;
    private Transform target;
    float dist;
    float timer;
    float delay;
    float ViewRange;




    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem boom;
    public GameObject boomimpact;

    void Start()
    {


        ASource = GetComponent<AudioSource>();
        minRot = -15f;
        maxRot = 15f;
        
        timer = 0;
        delay = 0.2f;
        ViewRange = 30;
    }

    // Update is called once per frame
    void Update()
    {
        

        players = GameObject.FindGameObjectsWithTag("Bot");
        if (players.Length > 0)
        {
            player = players[0];
            target = player.transform;

            dist = Vector3.Distance(transform.position, target.position);


            timer += Time.deltaTime;
            dist = Vector3.Distance(transform.position, target.position);
            if (dist < ViewRange)
            {
                transform.LookAt(target);



                if (timer >= delay)
                {
                    Shooting();
                    timer = 0;

                }
            }
        }
    }
    void Shooting()
    {
        boom.Play();
        ASource.Play();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Bot")
            {
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
            Destroy(impact, 2f);
        }
    }

}
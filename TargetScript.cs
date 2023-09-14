using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float health = 50f;
    Rigidbody rb;
    [SerializeField] private Transform player;
    AudioSource audiosource;
    public AudioClip explosion;
    public GameObject car_explosion;

    public Terrain ter;
    TerrainData tdata;
    Vector3 tsize;
    float hmapsize;
    int live;
    GameSceneControllerScript gscs;

    int GameMode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        tdata = ter.terrainData;
        tsize = tdata.size;
        hmapsize = tdata.heightmapResolution;
        GameMode = PlayerPrefs.GetInt("GameMode");
        if (GameMode == 1)
        {
            live = 1;
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {

    }
    public void damage(float damage)
    {
        if (GameMode == 0)
        {
            health -= damage;
            if (health <= 0f)
            {  
                die();
                PlayerStatsScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsScript>();
                playerScript.incScore(1);

            }
        }
        else
        {
            health -= damage;
            if (health <= 0f)
            {
                if (live > 0)
                 {
                live--;
                die();
                }
                else
                {
                Destroy(gameObject);
                }
            }
        }

    }
    void die()
    {
        Instantiate(car_explosion, transform.position, transform.rotation);
        audiosource.PlayOneShot(explosion);

        float x = Random.Range(25, tdata.size.x - 25);
        float z = Random.Range(25, tdata.size.z - 25);

        Vector3 Pos = new Vector3(x, tdata.GetHeight((int)(x * hmapsize / tsize.x), (int)(z * hmapsize / tsize.z)) + 10, z);
        player.transform.position = Pos;
      
        Physics.SyncTransforms();
        player.transform.eulerAngles= new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity= new Vector3(0, 0, 0);
        health = 50f;


    }
}
   

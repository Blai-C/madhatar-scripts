using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStatsScript : MonoBehaviour
{
   public PostProcessVolume v;
    ColorGrading color = null;


    float health;
    Rigidbody rb;
    Transform player;
    AudioSource audiosource;
    AudioSource voicesurce;
    public AudioClip explosion;
    public AudioClip crash;
    public AudioClip voice;
    public GameObject car_explosion;
    

    public Terrain ter;
    TerrainData tdata;
    Vector3 tsize;
    float hmapsize;
    float timer;
    float timer2;
    bool manual_respawn;
    bool voice_impact;
    int score;
    int live;
    GameSceneControllerScript gscs;
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        //Posto + Audio
        v = GameObject.FindGameObjectWithTag("FX").GetComponent<PostProcessVolume>();
        voicesurce=GameObject.FindGameObjectWithTag("Voice").GetComponent<AudioSource>();
        v.profile.TryGetSettings(out color);
        
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        
        gscs = GameObject.FindGameObjectWithTag("GameController").GetComponent< GameSceneControllerScript > ();
        player = GetComponent<Transform>();
        tdata = ter.terrainData;
        tsize = tdata.size;
        hmapsize = tdata.heightmapResolution;
        timer = 5.0f;
        timer = 5.0f;
        manual_respawn = false;
        voice_impact= false;
        score = 0;
        PlayerPrefs.SetInt("Score", score);
        live = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            if (manual_respawn)
            {
                manualRespawn();
            }
        }
        if (timer < 0.0f)
        {
            manual_respawn = true;
        }
        timer -= Time.deltaTime;
        if (timer2 < 0.0f)
        {
            voice_impact = true;
        }
        timer2 -= Time.deltaTime;
        color.saturation.value = Mathf.Lerp(-100, 0, Mathf.InverseLerp(0, 100, health));
    }
    public void damage(float damage)
    {
        health -= damage;
        if (health <= 50f)
        {            
            //
        }
        if (health <= 0f)
        {
            
            if (live > 0)
            {
                live--;
                die();
                gscs.decLife(live);
            }
            else
            {
                
                gscs.endgame();
            }

        }
    }
    void die()
    {
        
        GameObject impact = Instantiate(car_explosion, transform.position, transform.rotation);
        Destroy(impact, 2f);
        audiosource.PlayOneShot(explosion);


        float x = Random.Range(25, tdata.size.x - 25);
        float z = Random.Range(25, tdata.size.z - 25);


        Vector3 Pos = new Vector3(x, tdata.GetHeight((int)(x * hmapsize / tsize.x), (int)(z * hmapsize / tsize.z)) + 10, z);
        player.transform.position = Pos;
      
        Physics.SyncTransforms();
        player.transform.eulerAngles = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        health = 100f;
    }

    void manualRespawn()
    {
        timer = 5.0f;
        Vector3 Pos = new Vector3(player.transform.position.x+10, player.transform.position.y+10, player.transform.position.z+10);
        player.transform.position = Pos;
        Physics.SyncTransforms();
        player.transform.eulerAngles = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        manual_respawn = false;
    }
    public void incScore(int x)
    {
        score += x;
       
        PlayerPrefs.SetInt("Score", score);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bot")
        {
            audiosource.PlayOneShot(crash);
            if (voice_impact)
            {
                voicesurce.PlayOneShot(voice);
                timer2 = 5.0f;
                voice_impact = false;
            }
        }
    }

}

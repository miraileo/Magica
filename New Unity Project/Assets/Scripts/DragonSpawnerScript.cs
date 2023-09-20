using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonSpawnerScript : MonoBehaviour
{
    public GameObject spawnerObject1;
    public GameObject spawnerObject2;
    public GameObject spawnerObject3;
    public GameObject dragon;
    public GameObject slider;
    public bool dragonIsInstantiated = false;
    public GameObject text;
    Movement player;
    public AudioSource source1;
    public AudioSource source2;
    void Start()
    {
        player = GameObject.Find("Char").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.score == 30 && dragonIsInstantiated == false)
        {
            source1.enabled = false;
            source2.enabled = true;
            text.SetActive(true);
            Invoke("SpawnDragon", 5);
            dragonIsInstantiated = true;
        }
    }

    void SpawnDragon()
    {
        slider.SetActive(true);
        Instantiate(dragon, transform.position, Quaternion.identity);
    }

}

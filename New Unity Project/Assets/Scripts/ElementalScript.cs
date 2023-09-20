using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElementalScript : EnemyAiScript
{
    int layerOfFireElemental = 11;
    int layerOfIceElemental = 12;
    int layerOfEarthElemental = 13;
    int exortSphere = 10;
    int quasSphere = 14;
    int wexSphere = 15;
    public Transform rockArea;
    [SerializeField] private float ultiCooldown;
     private float maxUltiCooldown = 10;
    SpawnerForFinalScene finalScene;
    public bool isDead;
    public AudioSource source;
    

    private void Awake()
    {
        SetCharacteristics();
        if (SceneManager.GetActiveScene().name == "4")
        {
            finalScene = GameObject.Find("Spawner").GetComponent<SpawnerForFinalScene>();
            agent.speed = 2.4f;
            health = maxHealth = 200 * (finalScene.numOfWave / 3);
        }
        else
        {
            health = maxHealth = 400;
        }
        agent.speed = 1f;
        if (SceneManager.GetActiveScene().name != "4")
        {
            slider = GameObject.Find("BossHp").GetComponent<Slider>();
            Invoke("PlayMusic", 0.3f);
        }
        else
        {
            if(gameObject.layer == 11)
            {
                slider = GameObject.Find("BossHp2").GetComponent<Slider>();
            }
            if(gameObject.layer == 12)
            {
                slider = GameObject.Find("BossHp1").GetComponent<Slider>();
            }
            if(gameObject.layer == 13)
            {
                slider = GameObject.Find("BossHp3").GetComponent<Slider>();
            }

        }
        ultiCooldown = maxUltiCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        /*UpdateHealthBar(health, maxHealth);*/
        slider.value = health / maxHealth;
        ChasePlayer();
        AttackPlayer();
        Die();
        Cooldown();
        UltimateArea(player.gameObject.transform);
        if(health<=0 && SceneManager.GetActiveScene().name == "4")
        {
            player.elementalWasKilled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere")
        {
            TakeDamageFromSphere(other, layerOfFireElemental, quasSphere);
            TakeDamageFromSphere(other, layerOfIceElemental, exortSphere);
            TakeDamageFromSphere(other, layerOfEarthElemental, wexSphere);
            animator.Play("TakeDamage");
        }
        if (other.tag == "IceField")
        {
            aoe = GameObject.FindGameObjectWithTag("IceField").GetComponent<AoeScript>();
            TakeDamageFromUltimate(other, layerOfFireElemental, quasSphere);
            animator.Play("TakeDamage");
        }
        if (other.tag == "MeteorField")
        {
            aoe = GameObject.FindGameObjectWithTag("MeteorField").GetComponent<AoeScript>();
            TakeDamageFromUltimate(other, layerOfIceElemental, exortSphere);
            animator.Play("TakeDamage");
        }
        if (other.tag == "Blast")
        {
            aoe = GameObject.FindGameObjectWithTag("Blast").GetComponent<AoeScript>();
            TakeDamageFromUltimate(other, layerOfEarthElemental, wexSphere);
            animator.Play("TakeDamage");
        } 
    }

    void TakeDamageFromSphere(Collider other, int layerOfElemental, int layerOfSphere)
    {
        if (gameObject.layer == layerOfElemental)
        {
            if (other.gameObject.layer == layerOfSphere)
            {
                health -= (player.damageForElementals * 1.15f);
            }
            else
            {
                health -= (player.damageForElementals * 0.25f);
            }
        }
    }
    void TakeDamageFromUltimate(Collider other, int layerOfElemental, int layerOfSphere)
    {
        if (gameObject.layer == layerOfElemental)
        {
            if (other.gameObject.layer == layerOfSphere)
            {
                health -= player.damageForElementals * 3f;
            }
        }
    }

    void UltimateArea(Transform rockAreaPosition)
    {
        if (ultiCooldown <= 0)
        {
            rockAreaPosition.transform.position = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y + 0.15f, player.gameObject.transform.position.z);
            Instantiate(rockArea, rockAreaPosition.position, Quaternion.identity);
            ultiCooldown = maxUltiCooldown;
        }
        ultiCooldown -= Time.deltaTime;
    }
    void PlayMusic()
    {
        source.enabled = true;
    }
}

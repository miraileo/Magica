using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyAiScript : MonoBehaviour
{
    protected GameObject target;
    protected NavMeshAgent agent;
    public bool playerInAttackRange;
    public LayerMask whatIsPlayer;
    public float attackRange;
    public float health;
    public float maxHealth = 100;
    public Animator animator;
    public Slider slider;
    protected AoeScript aoe;
    protected Movement player;
    private float maxSpeed;
    public float timeBtwAttack = 0;
    public float maxTimeBtwAttack = 1;
    public float damage = 20;
    public float maxDamage;
    public GameObject speedDebuff;
    public GameObject damageDebuff;
    Spawner spawner;
    private string sceneName;
    PlayerHealthScript playerHealth;
    SpawnerForFinalScene spawnerForFinalScene;



    private void Awake()
    {
        SetCharacteristics();
    }
    private void Start()
    {
        if (sceneName == "4")
        {
            spawnerForFinalScene = GameObject.Find("Spawner").GetComponent<SpawnerForFinalScene>();
            if (spawnerForFinalScene.numOfWave != 0)
            {
                maxHealth += spawnerForFinalScene.numOfWave * 20;
                health = maxHealth;
                maxDamage += spawnerForFinalScene.numOfWave * 5;
                damage = maxDamage;

            }
        }
    }
    private void Update()
    {
        UpdateHealthBar(health, maxHealth);
        ChasePlayer();
        AttackPlayer();
        Die();
        Cooldown();
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamageFromSphere(other);
        TakeDamageFromAoeField(other);
    }

    public void Destroy()
    {
        if (sceneName != "4")
        {
            spawner.score++;
        }
        else
        {
            player.score++;
            player.lvlProgress += spawnerForFinalScene.numOfWave * 4f;
            player.money += 17;
        }
        slider.gameObject.SetActive(false);
        DestroyImmediate(gameObject);
    }
    public void ChasePlayer()
    {
        if (playerHealth.isDead == false) {
            agent.destination = target.transform.position;
        }
        if(playerHealth.isDead == true && SceneManager.GetActiveScene().name !="4")
        {
            agent.destination = spawner.gameObject.transform.position;
        }
        if(playerHealth.isDead == true && SceneManager.GetActiveScene().name == "4")
        {
            agent.destination = GameObject.Find("Spawner").transform.position;
        }
    }
    public void AttackPlayer()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (playerInAttackRange == true)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
    public void UpdateHealthBar(float currenHealth, float maxHealth)
    {
        slider.value = currenHealth / maxHealth;
    }
    public void Die()
    {
        if (health <= 0)
        {
            agent.speed = 0;
            animator.Play("Die");
            Invoke("Destroy", 1.5f);
        }
    }

    public void SetCharacteristics()
    {
        sceneName = SceneManager.GetActiveScene().name; 
        target = GameObject.Find("Char");
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Char").GetComponent<Movement>();
        playerHealth = GameObject.Find("Char").GetComponent<PlayerHealthScript>();
        agent.speed = 2.8f;
        maxSpeed = agent.speed;
        maxDamage = damage;
        health = maxHealth = 100;
        if (sceneName != "4")
        {
            spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        }
        else
        {
            spawnerForFinalScene = GameObject.Find("Spawner").GetComponent<SpawnerForFinalScene>();
            if (spawnerForFinalScene.numOfWave != 0)
            {
                maxDamage = damage * spawnerForFinalScene.numOfWave;
                health = maxHealth * spawnerForFinalScene.numOfWave;
            }
        }
    }

    public void TakeDamageFromAoeField(Collider other)
    {
        if (other.tag == "MeteorField")
        {
            aoe = GameObject.FindGameObjectWithTag("MeteorField").GetComponent<AoeScript>();
            health = aoe.AoeFieldDamage(health);
            animator.Play("TakeDamage");
        }
        if (other.tag == "IceField")
        {
            aoe = GameObject.FindGameObjectWithTag("IceField").GetComponent<AoeScript>();
            agent.speed = aoe.AoeFieldSlowDown(agent.speed);
            health = aoe.AoeFieldDamageIceField(health);
            Invoke("returnSpeed", aoe.debuffIceFieldDuration);
            speedDebuff.gameObject.SetActive(true);
            animator.Play("TakeDamage");
        }
        if (other.tag == "Blast")
        {
            aoe = GameObject.FindGameObjectWithTag("Blast").GetComponent<AoeScript>();
            damage = aoe.AoeFieldWeakening(damage);
            damageDebuff.gameObject.SetActive(true);
            health = aoe.AoeFieldDamageBlast(health);
            Invoke("returnDamage", aoe.debuffBlastDuration);
            animator.Play("TakeDamage");
        }
    }
    void TakeDamageFromSphere(Collider other)
    {
        if (other.tag == "Sphere")
        {
            health -= player.damage;
            animator.Play("TakeDamage");
        }
    }
    private void returnSpeed()
    {
        agent.speed = maxSpeed;
        speedDebuff.gameObject.SetActive(false);
    }
    private void returnDamage()
    {
        damage = maxDamage;
        damageDebuff.gameObject.SetActive(false);
    }

    public void Cooldown()
    {
        if(timeBtwAttack <= maxTimeBtwAttack)
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

}

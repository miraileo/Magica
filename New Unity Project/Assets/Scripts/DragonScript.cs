using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragonScript : MonoBehaviour
{
    Animator animator;
    public bool playerInAttackRange;
    public bool playerInFireRange;
    public LayerMask whatIsPlayer;
    public float attackRange;
    Movement player;
    private float timeBtwStart = 3f;
    NavMeshAgent agent;
    private string attack = "Attack";
    private string attackFlame = "Flame";
    private bool randomValueWasTaken = true;
    private int randomValue;
    public GameObject Fire;
    public GameObject SpawnPos;
    AoeScript aoe;
    private float health;
    public float maxHealth = 1000;
    public float Width;
    public float Height;
    public Slider slider;
    public PlayerHealthScript playerState;
    public bool isAlive = true;

    void Start()
    {
        playerState = GameObject.Find("Char").GetComponent<PlayerHealthScript>();
        slider = GameObject.Find("DragonHp").GetComponent<Slider>();
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Char").GetComponent<Movement>();
        SpawnPos = GameObject.Find("DragonSpawner");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        if (timeBtwStart <= 0)
        {
            if(playerState.health<=0)
            {
                agent.destination = SpawnPos.transform.position;
            }
            else
            {
                agent.destination = player.transform.position;
            }
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (playerInAttackRange == true)
            {
                RandomAttack();
                Debug.Log(randomValue);
                if (randomValue == 0)
                {
                    transform.LookAt(player.transform);
                    animator.Play(attack);
                }
                if (randomValue == 1)
                {
                    transform.LookAt(player.transform);
                    animator.Play(attackFlame);
                    Fire.SetActive(true);
                    Invoke("SetActiveFire", 1.5f);
                }
            }
        }
        timeBtwStart -= Time.deltaTime;
        Die();
    }
    public void RandomAttack()
    {
        if (randomValueWasTaken == true)
        {
            randomValue = Random.Range(0, 2);
            randomValueWasTaken = false;
            Invoke("ReturnValue", 1.75f);
        }
    }
    void ReturnValue()
    {
        randomValueWasTaken = true;
    }
    void SetActiveFire()
    {
        Fire.SetActive(false);
    }
    public void TakeDamageFromAoeField(Collider other)
    {
        if (other.tag == "MeteorField")
        {
            aoe = GameObject.FindGameObjectWithTag("MeteorField").GetComponent<AoeScript>();
            health = aoe.AoeFieldDamage(health)-20;
            //animator.Play("TakeDamage");
        }
        if (other.tag == "IceField")
        {
            aoe = GameObject.FindGameObjectWithTag("IceField").GetComponent<AoeScript>();
            health = aoe.AoeFieldDamageIceField(health)-20;
            //animator.Play("TakeDamage");
        }
        if (other.tag == "Blast")
        {
            aoe = GameObject.FindGameObjectWithTag("Blast").GetComponent<AoeScript>();
            health = aoe.AoeFieldDamageBlast(health)-20;
            //animator.Play("TakeDamage");
        }
    }
    void TakeDamageFromSphere(Collider other)
    {
        if (other.tag == "Sphere")
        {
            health -= player.damage-15;
            animator.Play("TakeDamage");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        TakeDamageFromSphere(other);
        TakeDamageFromAoeField(other);
    }
    void UpdateHealthBar()
    {
        slider.value = health / maxHealth;
    }

    void Die()
    {
        if(health<=0)
        {
            isAlive = false;
            animator.Play("Die");
            playerInAttackRange = false;
            randomValue = 3;
            Invoke("LoadNextScene", 2);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(6);
    }
}

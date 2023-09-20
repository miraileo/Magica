using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class PlayerHealthScript : MonoBehaviour
{
    
    public float maxHealth;
    public float maxMana;
    public float healthRegen;
    public float maxHealthRegen;
    public float manaRegen;
    public UiHealthBarScript healthBar;
    public float health;
    public float mana;
    EnemyAiScript enemy;
    UltimateScript ulti;
    public GameObject Death;
    public GameObject respawnPosition;
    public bool isDead = false;
    public Text amountOfHealthText;
    public Text amountOfManaText;
    public Text openShop;
    Animator _animator;
    public Movement player;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    void Start()
    {
        player = GetComponent<Movement>();
        _animator= GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name != "4")
        {
            manaRegen = 2;
            maxHealth = 100;
            maxMana = mana = 100;
            healthBar.SetMaxMana(maxMana);
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "4" && YandexGame.SDKEnabled == true)
        {
            amountOfHealthText.text = Mathf.Floor(health).ToString() + "/" + Mathf.Round(maxHealth).ToString();
            amountOfManaText.text = Mathf.Round(mana).ToString() + "/" + maxMana.ToString();
            healthBar.SetMaxMana(maxMana);
            healthBar.SetMaxHealth(maxHealth);
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        if (mana >= maxMana)
        {
            mana = maxMana;
        }
        if (health <= 0 && YandexGame.SDKEnabled == true)
        {
            player.moveSpeed = 0;
            Invoke("Die", 1);
            _animator.Play("Die");
        }
        health += Time.deltaTime * healthRegen;
        mana += Time.deltaTime * manaRegen;
        healthBar.SetHealth(health, healthRegen);
        healthBar.SetMana(mana, manaRegen);
    }
    private void Die()
    {
        isDead = true;
        Death.SetActive(true);
        gameObject.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.GetComponent<EnemyAiScript>();
            if (enemy.timeBtwAttack <= 0)
            {
                health -= enemy.damage;
                enemy.timeBtwAttack = enemy.maxTimeBtwAttack;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RockArea")
        {
            ulti = other.GetComponent<UltimateScript>();
            health -= ulti.damage;
        }
        if(other.tag == "Dragon")
        {
            health -= 30;
        }
        if (other.tag == "Fire")
        {
            health -= 50;
        }
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.moveSpeed = 2;
    }
    public void RespawnForAd(int id)
    {
        YandexGame.RewVideoShow(id);
        Rewarded(id);
        player.moveSpeed = 2;
    }
    void Rewarded(int id)
    {
        if (id == 1)
        {
            Invoke("SetCharacteristicsAfterDeath", 1f);
        }
    }
    void SetCharacteristicsAfterDeath()
    {
        isDead = false;
        gameObject.SetActive(true);
        gameObject.transform.position = respawnPosition.transform.position;
        health = maxHealth;
        Death.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using YG;

public class SpawnerForFinalScene : MonoBehaviour
{
    public Transform[] spawnPos;
    public float numOfWave;
    public int numOfElementals;
    public GameObject[] enemies;
    public float spawnTime;
    Movement player;
    public Image timeBar;
    private float maxSpawnTime = 5;
    public GameObject fireElemental;
    public GameObject iceElemental;
    public GameObject earthElemental;
    public GameObject bossHp1;
    public GameObject bossHp2;
    public GameObject bossHp3;
    public bool elementalInstantiated;
    public AudioSource source;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    void Awake()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }
    private void Start()
    {
        player = GameObject.Find("Char").GetComponent<Movement>();
    }
    private void Update()
    {
        SpawnEnemies();
        Taimer();
    }
    void SpawnEnemies()
    {
        if (spawnTime <= 0)
        {
            numOfWave++;
            if (numOfWave % 4 != 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    {
                        int j = Random.Range(0, 6);
                        Instantiate(enemies[j], spawnPos[i].position, Quaternion.identity);
                    }
                }
            }
            if (numOfWave % 4 == 0 && numOfWave != 0)
            {
                bossHp1.SetActive(true);
                bossHp2.SetActive(true);
                bossHp3.SetActive(true);
                Instantiate(fireElemental, spawnPos[8].position, Quaternion.identity);
                Instantiate(iceElemental, spawnPos[7].position, Quaternion.identity);
                Instantiate(earthElemental, spawnPos[6].position, Quaternion.identity);
                source.enabled = true;
            }
            spawnTime = 5;
        }
    }

    void Taimer()
    {
        if (player.score / 9 == numOfWave && player.score!=0)
        {
            MySave();
            timeBar.gameObject.SetActive(true);
            timeBar.fillAmount = spawnTime / maxSpawnTime;
            spawnTime -= Time.deltaTime;
        }
    }

    public void GetLoad()
    {
        player.money = YandexGame.savesData.coins;
        numOfWave = YandexGame.savesData.numOfWave;
        player.score = YandexGame.savesData._score;
        numOfElementals = YandexGame.savesData.numOfElementals;
    }

    public void MySave()
    {
        YandexGame.savesData.coins = player.money;
        YandexGame.savesData.numOfElementals = numOfElementals;
        YandexGame.savesData.numOfWave = numOfWave;
        YandexGame.savesData._score = player.score;
        // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }
}

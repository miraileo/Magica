using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;


public class Spawner : MonoBehaviour
{
    public bool _wexScene = true;
    public bool _quasScene = true;
    public bool _exortScene = true;
    public float timeBtwSpawn;
    public float spawnTime = 0;
    public GameObject skeleton;
    public GameObject elemental;
    public GameObject bug;
    public GameObject slime;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform spawnPos1;
    [SerializeField] private Transform spawnPos2;
    public int score;
    public float numOfEnemies;
    public  float timeForNextWave;
    public float maxTimeForNextWave = 10;
    public int numOfWave = 0;
    public Slider slider;
    public Text scoreText;
    public GameObject gameEnded;
    public Image timeBar;
    PlayerHealthScript health;
    public bool elementalInstantiated;
    public AudioSource source;

    void Start()
    {
        health = GameObject.Find("Char").GetComponent<PlayerHealthScript>();
        timeForNextWave = maxTimeForNextWave;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(score!= 28)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }*/
        scoreText.text = score.ToString();
        if (spawnTime <= 0)
        {
            if (numOfEnemies < 3)
            {
                InstantiateEnemies(bug);
            }

            if (numOfEnemies >= 3 && numOfEnemies<6 && numOfWave==1)
            {
                InstantiateEnemies(slime);
            }

            if (numOfEnemies >= 6 && numOfEnemies < 9 && numOfWave == 2)
            {
                InstantiateEnemies(skeleton);
            }
            if (numOfWave == 3 && score==numOfEnemies)
            {
                InstantiateBoss(elemental);
                source.enabled = false;
            }
        }
        if (health.isDead == false)
        {
            spawnTime -= Time.deltaTime;
        }
        if (numOfEnemies == score)
        {
            timeBar.gameObject.SetActive(true);
            timeBar.fillAmount = timeForNextWave / maxTimeForNextWave;
            TaimerForNextWave();
        }

        if(score == 10)
        {
            if (SceneManager.GetActiveScene().name == "1")
            {
                YandexGame.savesData.wexScene = _wexScene;
                YandexGame.SaveProgress();
            }
            if (SceneManager.GetActiveScene().name == "3")
            {
                YandexGame.savesData.quasScene = _quasScene;
                YandexGame.SaveProgress();
            }
            if (SceneManager.GetActiveScene().name == "2")
            {
                YandexGame.savesData.exortScene = _exortScene;
                YandexGame.SaveProgress();
            }
            gameEnded.SetActive(true);
        }
    }

    void TaimerForNextWave()
    {
        timeForNextWave -= Time.deltaTime;
        if(timeForNextWave <=0)
        {
            numOfWave++;
            timeForNextWave = maxTimeForNextWave;
            timeBar.gameObject.SetActive(false);
        }
    }

    void InstantiateEnemies(GameObject enemy)
    {
        Instantiate(enemy, spawnPos.position, Quaternion.identity);
        Instantiate(enemy, spawnPos1.position, Quaternion.identity);
        Instantiate(enemy, spawnPos2.position, Quaternion.identity);
        numOfEnemies += 3;
        spawnTime = timeBtwSpawn;
    }
    void InstantiateBoss(GameObject enemy)
    {
        elementalInstantiated = true;
        slider.gameObject.SetActive(true);
        Instantiate(enemy, spawnPos2.position, Quaternion.identity);
        numOfEnemies = 100;
    }
}
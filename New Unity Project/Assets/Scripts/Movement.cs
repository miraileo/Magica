using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class Movement : MonoBehaviour
{
    private InputHandler _input;
    public float moveSpeed;
    public float rotationSpeed;
    public Camera camera;
    public float damage;
    public float maxDamage;
    public float damageForElementals = 20;
    public float score;
    public Text scoreText;
    public Text lvlText;
    public Text lvlProgressText;
    private string scenename;
    public float lvlProgress;
    public float lvl;
    public int amountOfLvlForNextLvel;
    public shoot _shoot;
    public PlayerHealthScript stats;
    public bool elementalWasKilled;
    public int money;
    public Text moneyText;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;



    private void Awake()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
        scenename = SceneManager.GetActiveScene().name;
        _input = GetComponent<InputHandler>();
    }
    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.Space) && SceneManager.GetActiveScene().name == "4")
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }*/
        UpdateLvl();
        /*if(Input.GetKeyDown(KeyCode.F)) 
        {
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            Time.timeScale = 1;
        }*/
        if (scenename == "4")
        {
            lvl = Mathf.Round(lvl);
            scoreText.text = score.ToString();
            lvlText.text = Mathf.Floor(lvl).ToString();
            lvlProgressText.text = lvlProgress.ToString() + "/" + amountOfLvlForNextLvel.ToString();
            moneyText.text = money.ToString();
        }
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        var movementVector = MoveTowardTarget(targetVector);
        RotateTowardMovementVector(movementVector);
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {   
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }
    private void RotateTowardMovementVector(Vector3 movementVector)
    {
        if(movementVector.magnitude == 0)
        {
            return;
        }
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
}
    public void RotateToMouse()
    {
        Ray ray = camera.ScreenPointToRay(_input.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    void UpdateLvl()
    {
        if (SceneManager.GetActiveScene().name == "4")
        {
            if (lvlProgress >= amountOfLvlForNextLvel)
            {
                lvl += lvlProgress / amountOfLvlForNextLvel;
                amountOfLvlForNextLvel += amountOfLvlForNextLvel;
                lvlProgress = 0;
                _shoot.maxAmountOfSpheres += 1;
                maxDamage += 3;
                stats.manaRegen += 0.5f;
                stats.maxHealthRegen += 0.5f;
                
                if (lvl == 1)
                {
                    stats.maxMana += 10;
                    stats.maxHealth += 15;
                }
                else
                {
                    stats.maxMana += 10;
                    stats.maxHealth += lvl * 20;
                }

                MySave();
            }
        }
    }


    public void GetLoad()
    {
        if (SceneManager.GetActiveScene().name == "4")
        {
            stats.maxHealthRegen = YandexGame.savesData.healthRegen;
            stats.manaRegen = YandexGame.savesData.manaRegen;
            maxDamage = YandexGame.savesData.playerDamage;
            stats.maxHealth = YandexGame.savesData._maxPlayerHealth;
            stats.maxMana = YandexGame.savesData._maxPlayerMana;
            stats.health = stats.maxHealth;
            stats.mana = stats.maxMana;
            _shoot.maxAmountOfSpheres = YandexGame.savesData._maxAmountOfSpheres;
            lvl = YandexGame.savesData.lvl;
            amountOfLvlForNextLvel = YandexGame.savesData.amountOfLvlForNextLevel;
        }
    }

    public void MySave()
    {
        YandexGame.savesData.healthRegen = stats.maxHealthRegen;
        YandexGame.savesData.manaRegen = stats.manaRegen;
        YandexGame.savesData.playerDamage = maxDamage;
        YandexGame.savesData._maxPlayerHealth = stats.maxHealth;
        YandexGame.savesData._maxPlayerMana = stats.maxMana;
        YandexGame.savesData._maxAmountOfSpheres = _shoot.maxAmountOfSpheres;
        YandexGame.savesData.lvl = lvl;
        YandexGame.savesData.amountOfLvlForNextLevel = amountOfLvlForNextLvel;
        // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class AoeScript : MonoBehaviour
{
    private float damage = 40;
    private float _damage = 20;
    public float damage1;
    public float damage2;
    public float damage3;
    protected float slowdown = 0.4f;
    protected float weakness = 0.5f;
    public float timeSlowdown = 2;
    public float debuffBlastDuration = 7;
    public float debuffIceFieldDuration = 3.5f;
    public float aoeFieldDuration = 3.8f;
    ShopScript shop;
    public bool blueWandBonusGotten;
    public bool redWandBonusGotten;
    public bool purpleWandBonusGotten;


    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "4")
        {
            shop = GameObject.FindGameObjectWithTag("Shop").GetComponent< ShopScript>();
        }
        if (YandexGame.SDKEnabled == true && SceneManager.GetActiveScene().name == "4")
        {
            GetLoad();
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "4")
        {
            if(shop.blueWandWasBied == true && blueWandBonusGotten == false)
            {
                damage1 = YandexGame.savesData.iceFieldDamage *2;
                blueWandBonusGotten = true;
                MySave();
            }
            if(shop.redWandWasBied == true && redWandBonusGotten == false)
            {
                damage2 = YandexGame.savesData.meteorFieldDamage *2;
                redWandBonusGotten = true;
                MySave();
            }
            if (shop.purpleWandWasBied == true && purpleWandBonusGotten == false)
            {
                damage3 = YandexGame.savesData.blastDamage * 2;
                purpleWandBonusGotten = true;
                MySave();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        Invoke("DestroyAoeField", aoeFieldDuration);
    }

    public void DestroyAoeField()
    {
        Destroy(gameObject);
    }
    public float AoeFieldDamage(float health)
    {
        if (SceneManager.GetActiveScene().name != "4")
        {
            health -= damage;
            return health;
        }
        else
        {
            health -= damage2;
            return health;
        }
    }
    public float AoeFieldSlowDown(float speed)
    {
        speed *= slowdown;
        return speed;
    }
    public float AoeFieldWeakening(float damage)
    {
        damage *= weakness;
        return damage;
    }
    public float AoeFieldDamageIceField(float health)
    {
        if (SceneManager.GetActiveScene().name != "4")
        {
            health -= _damage;
            return health;
        }
        else
        {
            health -= damage1;
            return health;
        }
    }
    public float AoeFieldDamageBlast(float health)
    {
        if (SceneManager.GetActiveScene().name != "4")
        {
            health -= _damage;
            return health;
        }
        else
        {
            health -= damage3;
            return health;
        }
    }

    public void GetLoad()
    {
        blueWandBonusGotten = YandexGame.savesData.blueWandBonusGotten;
        redWandBonusGotten = YandexGame.savesData.redWandBonusGotten;
        purpleWandBonusGotten = YandexGame.savesData.purpleWandBonusGotten;
       damage1 = YandexGame.savesData.iceFieldDamage;
        damage2 = YandexGame.savesData.meteorFieldDamage;
        damage3 = YandexGame.savesData.blastDamage;
    }

    public void MySave()
    {
       YandexGame.savesData.blueWandBonusGotten = blueWandBonusGotten;
       YandexGame.savesData.iceFieldDamage = damage1;
       YandexGame.savesData.redWandBonusGotten = redWandBonusGotten;
       YandexGame.savesData.meteorFieldDamage = damage2;
       YandexGame.savesData.purpleWandBonusGotten = purpleWandBonusGotten;
       YandexGame.savesData.blastDamage = damage3;
       YandexGame.SaveProgress();
    }
}

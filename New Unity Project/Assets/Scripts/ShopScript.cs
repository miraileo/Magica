using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class ShopScript : MonoBehaviour
{

    public GameObject openShop;
    bool shopIsNear = false;
    public GameObject shopWindow;
    public bool blueWandWasBied;
    public Button blueWandButton;
    public Button redWandButton;
    public Button purpleWandButton;
    public bool redWandWasBied;
    public bool purpleWandWasBied;
    public Movement player;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;


    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Char").GetComponent<Movement>();
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(blueWandWasBied == true)
        {
            blueWandButton.interactable = false;
            MySave();
        }
        if(redWandWasBied == true)
        {
            redWandButton.interactable = false;
            MySave();
        }
        if(purpleWandWasBied == true)
        {
            purpleWandButton.interactable = false;
            MySave();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shopIsNear= true;
            openShop.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shopIsNear = false;
            openShop.gameObject.SetActive(false);
        }
    }

    public void OpenShop()
    {
        if (shopIsNear == true)
        {
            Time.timeScale = 0;
            shopWindow.SetActive(true);
        }
    }

    public void CloseShop()
    {
        Time.timeScale = 1;
        shopWindow.SetActive(false);
    }

    public void BuyBlueWand()
    {
        if(player.money >= 150)
        {
            player.money -= 150;
            blueWandWasBied = true;
        }
    }
    public void BuyRedWand()
    {
        if (player.money >= 150)
        {
            player.money -= 150;
            redWandWasBied = true;
        }
    }
    public void BuyPurpleWand()
    {
        if (player.money >= 150)
        {
            player.money -= 150;
            purpleWandWasBied = true;
        }
    }

    public void GetLoad()
    {
        player.money = YandexGame.savesData.coins;
        redWandWasBied = YandexGame.savesData.redWandWasBied;
        blueWandWasBied = YandexGame.savesData.blueWandWasBied;
        purpleWandWasBied = YandexGame.savesData.purpleWandWasBied;
    }

    public void MySave()
    {
        YandexGame.savesData.coins = player.money;
        YandexGame.savesData.purpleWandWasBied = purpleWandWasBied;
        YandexGame.savesData.blueWandWasBied = blueWandWasBied;
        YandexGame.savesData.redWandWasBied = redWandWasBied;
        YandexGame.SaveProgress();
    }

}

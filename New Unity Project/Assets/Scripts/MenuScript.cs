using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuScript : MonoBehaviour
{
    public GameObject lockExortScene;
    public Button exortSceneButton;
    public GameObject lockQuasScene;
    public Button quasSceneButton;
    public GameObject lockPolygonScene;
    public Button polygonSceneButton;
    Animator animator;
    public AudioSource source;
    public AudioClip buttonClip;
    public AudioClip loadingClip;
    public GameObject settingsWindow;
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LvlChoose")
        {
            if (YandexGame.savesData.wexScene == true)
            {
                lockExortScene.SetActive(false);
                exortSceneButton.interactable = true;
            }
            if (YandexGame.savesData.exortScene == true)
            {
                lockQuasScene.SetActive(false);
                quasSceneButton.interactable = true;
            }
            if (YandexGame.savesData.quasScene == true)
            {
                lockPolygonScene.SetActive(false);
                polygonSceneButton.interactable = true;
            }
        }
        /*if(Input.GetKeyUp(KeyCode.Space))
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }*/
        if(YandexGame.savesData.quasScene == true || YandexGame.savesData.exortScene == true || YandexGame.savesData.wexScene == true)
        {
            YandexGame.savesData.newPlayer = false;
        }
    }
    public void Open(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void OpenForNewPlayers(GameObject obj)
    {
        if (YandexGame.savesData.newPlayer == true)
        {
            source.PlayOneShot(buttonClip);
            obj.SetActive(true);
        }
        else
        {
            source.PlayOneShot(buttonClip);
            StartCoroutine(LoadLevel(1));
        }
    }
    public void Close(GameObject obj)
    {
        obj.SetActive(false);
        source.PlayOneShot(buttonClip);
    }
    public void LoadScene(int sceneId)
    {
        source.PlayOneShot(buttonClip);
        StartCoroutine(LoadLevel(sceneId));
    }

    public void GetLoad()
    {
    }

    public void MySave()
    {
    }

    IEnumerator LoadLevel(int lvl_index)
    {
        animator.SetTrigger("Trigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(lvl_index);
    }

    public void OpenSettings(GameObject obj)
    {
        obj.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseSettings(GameObject obj)
    {
        obj.SetActive(false);
        Time.timeScale = 1;
    }
}

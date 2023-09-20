using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Training : MonoBehaviour
{
    public GameObject previousPage;
    public GameObject nextPage;
    public GameObject previousPageButton;
    public GameObject nextPageButton;
    public GameObject blank;
    public bool isOpened;
    Spawner spawner;
    public AudioSource source;
    public AudioSource sourceForBattle;
    bool hasPlayed = false;
    public AudioClip pageClip;
    public AudioClip closeClip;
    public AudioClip openClip;
    public AudioClip battleClip;

    // Update is called once per frame
    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "4")
        {
            spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        }
        Invoke("SetBlankAtive", 1f);

    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "4")
        {
            if (blank.name == "ElementalBlank" && spawner.elementalInstantiated == true)
        {
            isOpened = true;
            if (hasPlayed == false)
            {
                source.PlayOneShot(openClip);
                hasPlayed = true;
            }
            blank.SetActive(true);
            Time.timeScale = 0;
        }
        }
    }
    public void NextPage()
    {
        nextPage.SetActive(true);
        previousPage.SetActive(false);
        nextPageButton.SetActive(false);
        previousPageButton.SetActive(true);
        source.PlayOneShot(pageClip);
    }
    public void PreviousPage()
    {
        nextPage.SetActive(false);
        previousPage.SetActive(true);
        previousPageButton.SetActive(false);
        nextPageButton.SetActive(true);
        source.PlayOneShot(pageClip);
    }

    void SetBlankAtive()
    {
        if (blank.name == "TrainingBlank")
        {
            isOpened = true;
            source.PlayOneShot(openClip);
            blank.SetActive(true);
            if (blank != null)
            {
                Time.timeScale = 0;
            }
        }
    }

    public void CloseBlank()
    {
        isOpened= false;
        source.PlayOneShot(closeClip);
        blank.SetActive(false);
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name != "4")
        {
            spawner.elementalInstantiated = false;
        }
        sourceForBattle.enabled = true;
    }
}

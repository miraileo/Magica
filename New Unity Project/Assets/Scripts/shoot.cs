using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class shoot : MonoBehaviour
{
    public GameObject Exort;
    public GameObject Quas;
    public GameObject Wex;
    public GameObject hand;
    public int numOfSphere;
    public PlayerHealthScript health;
    public GameObject exortIndicate;
    public GameObject quasIndicate;
    public GameObject wexIndicate;
    public GameObject meteor;
    public GameObject iceField;
    public GameObject blast;
    public GameObject castPos;
    private Camera cam;
    Vector3 target;
    public float timeBtwAttack;
    private float maxTimeBtwAttack = 0.4f;
    public int amountOfExortSpheres;
    public int amountOfWexSpheres;
    public int amountOfQuasSpheres;
    public int maxAmountOfSpheres;
    public float refillExortTime;
    public float refillWexTime;
    public float refillQuasTime;
    const  float maxRefillTime = 3;
    AudioSource source;
    public AudioClip attackSound, changeSpheres;
    public Image exortImage;
    public Image quasImage;
    public Image wexImage;
    public Image meteorImage;
    public Image iceFieldImage;
    public Image blastImage;
    public Text amountOfExortSpheresText;
    public Text amountOfQuasSpheresText;
    public Text amountOfWexSpheresText;
    private float maxMeteorCooldown = 25;
    private float maxIceFieldCooldown = 15;
    private float maxBlastCooldown = 10;
    public float meteorCooldown;
    public float iceFieldCooldown;
    public float blastCooldown;
    public GameObject meteorHolder;
    public GameObject iceFieldHolder;
    public GameObject blastHolder;
    Movement movement;
    bool exortActive = false;
    bool quasActive = false;
    bool wexActive = false;
    private string sceneName;
    private Training trainingWindow;
    private Training trainingWindow1;

    private void Awake()
    {
        trainingWindow = GameObject.Find("TrainingBlankHolder").GetComponent<Training>();
        trainingWindow1 = GameObject.Find("ElementalTrainingHolder").GetComponent<Training>();
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "1")
        {
            wexActive = true;

        }
        if(sceneName == "2") 
        {
            wexActive = true;
            quasActive= true;
        }
        if(sceneName == "3" || sceneName == "4") 
        {
            wexActive = true;
            quasActive = true;
            exortActive = true;
        }
        source = GetComponent<AudioSource>();
        movement = GameObject.Find("Char").GetComponent<Movement>();
}
    void Start()
    {
        Invoke("lol", 0.2f);
    }
    private void LateUpdate()
    {

    }
    void Update()
    {
        CooldownSpheresUi(refillExortTime, maxRefillTime, amountOfExortSpheres, exortImage, amountOfExortSpheresText);
        CooldownSpheresUi(refillQuasTime, maxRefillTime, amountOfQuasSpheres, quasImage, amountOfQuasSpheresText);
        CooldownSpheresUi(refillWexTime, maxRefillTime, amountOfWexSpheres, wexImage, amountOfWexSpheresText);
        SpheersCooldown();
        AttackTimer();
        meteorCooldown = UllimateCooldown(meteorCooldown);
        iceFieldCooldown = UllimateCooldown(iceFieldCooldown);
        blastCooldown = UllimateCooldown(blastCooldown);
        UltimateCooldownUi(meteorCooldown, maxMeteorCooldown, meteorImage);
        UltimateCooldownUi(iceFieldCooldown, maxIceFieldCooldown, iceFieldImage);
        UltimateCooldownUi(blastCooldown, maxBlastCooldown, blastImage);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            target = raycastHit.point;
            target.y = castPos.transform.position.y;
        }
        Spheres();
        if (Input.GetMouseButtonDown(0) && timeBtwAttack<=0 && trainingWindow.isOpened == false && trainingWindow1.isOpened == false)
        {
            switch(numOfSphere)
            {
            case 3:
                    if (amountOfExortSpheres != 0 && exortActive == true)
                    {
                        Instantiate(Exort, hand.transform.position, Quaternion.identity);
                        amountOfExortSpheres--;
                        source.PlayOneShot(attackSound);
                    }
                    break;
            case 2:
                    if (amountOfWexSpheres != 0 && wexActive == true)
                    {
                        Instantiate(Wex, hand.transform.position, Quaternion.identity);
                        amountOfWexSpheres--;
                        source.PlayOneShot(attackSound);
                    }
                    break;
            case 1:
                    if (amountOfQuasSpheres != 0 && quasActive == true)
                    {
                        Instantiate(Quas, hand.transform.position, Quaternion.identity);
                        amountOfQuasSpheres--;
                        source.PlayOneShot(attackSound);
                    }
                    break;              
            }
            timeBtwAttack = maxTimeBtwAttack;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            switch (numOfSphere)
            {
                case 3:
                    if (health.mana >= 50 && meteorCooldown <= 0 && exortActive == true)
                    {
                        Instantiate(meteor, target, Quaternion.identity);
                        health.mana -= 50;
                        meteorCooldown = maxMeteorCooldown;
                        meteorImage.fillAmount = 1;
                    }
                    break;
                case 2:
                    if (health.mana >= 20 && blastCooldown <= 0 && wexActive == true)
                    {
                        Instantiate(blast, target, Quaternion.identity);
                        blastCooldown = maxBlastCooldown;
                        health.mana -= 20;
                        blastImage.fillAmount = 1;
                    }
                    break;
                case 1:
                    if (health.mana >= 30 && iceFieldCooldown <= 0 && quasActive == true)
                    {
                        Instantiate(iceField, target, Quaternion.identity);
                        iceFieldCooldown = maxIceFieldCooldown;
                        health.mana -= 30;
                        iceFieldImage.fillAmount = 1;
                    }
                    break;
            }
        }
    }
    void Spheres()
    {
        if (trainingWindow.isOpened == false && trainingWindow1.isOpened == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && quasActive == true)
            {
                quasIndicate.gameObject.SetActive(true);
                exortIndicate.gameObject.SetActive(false);
                wexIndicate.gameObject.SetActive(false);
                meteorHolder.SetActive(false);
                blastHolder.SetActive(false);
                iceFieldHolder.SetActive(true);

                numOfSphere = 1;
                if (SceneManager.GetActiveScene().name == "4")
                {
                    movement.damage = movement.maxDamage;
                    health.healthRegen = health.maxHealthRegen * 2;
                }
                else
                {
                    movement.damage = 20;
                    health.healthRegen = 3;
                }
                movement.moveSpeed = 2.5f;
                source.PlayOneShot(changeSpheres);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && wexActive == true)
            {
                quasIndicate.gameObject.SetActive(false);
                exortIndicate.gameObject.SetActive(false);
                wexIndicate.gameObject.SetActive(true);
                meteorHolder.SetActive(false);
                blastHolder.SetActive(true);
                iceFieldHolder.SetActive(false);
                numOfSphere = 2;
                if (SceneManager.GetActiveScene().name == "4")
                {
                    movement.damage = movement.maxDamage;
                    health.healthRegen = health.maxHealthRegen;
                }
                else
                {
                    movement.damage = 20;
                    health.healthRegen = 1;
                }
                movement.moveSpeed = 3;
                source.PlayOneShot(changeSpheres);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && exortActive == true)
            {
                quasIndicate.gameObject.SetActive(false);
                exortIndicate.gameObject.SetActive(true);
                wexIndicate.gameObject.SetActive(false);
                meteorHolder.SetActive(true);
                blastHolder.SetActive(false);
                iceFieldHolder.SetActive(false);
                numOfSphere = 3;
                if (SceneManager.GetActiveScene().name == "4")
                {
                    movement.damage = movement.maxDamage * 1.75f;
                    health.healthRegen = health.maxHealthRegen;
                }
                else
                {
                    movement.damage = 35;
                    health.healthRegen = 1;
                }
                movement.moveSpeed = 2.5f;
                source.PlayOneShot(changeSpheres);
            }
        }
    }
    void AttackTimer()
    {
        timeBtwAttack -= Time.deltaTime;
    }
    void SpheersCooldown()
    {
        if (amountOfExortSpheres < maxAmountOfSpheres && refillExortTime <= 0)
        {
            amountOfExortSpheres++;
            refillExortTime = maxRefillTime;
        }
        else if (amountOfExortSpheres < maxAmountOfSpheres)
        {
            refillExortTime -= Time.deltaTime;
        }
        if (amountOfWexSpheres < maxAmountOfSpheres && refillWexTime <= 0)
        {
            amountOfWexSpheres++;
            refillWexTime = maxRefillTime;
        }
        else if (amountOfWexSpheres < maxAmountOfSpheres)
        {
            refillWexTime -= Time.deltaTime;
        }
        if (amountOfQuasSpheres < maxAmountOfSpheres && refillQuasTime <= 0)
        {
            amountOfQuasSpheres++;
            refillQuasTime = maxRefillTime;
        }
        else if (amountOfQuasSpheres < maxAmountOfSpheres)
        {
            refillQuasTime -= Time.deltaTime;
        }
    }

    void CooldownSpheresUi(float refillTime, float maxRefillTime, int amountOfSpheres, Image sphereImage, Text amountOfSpheresText)
    {
        if(amountOfSpheres < maxAmountOfSpheres && amountOfSpheres >= 0)
        {
            amountOfSpheresText.gameObject.SetActive(true);
            sphereImage.fillAmount -=  1/ maxRefillTime * Time.deltaTime;
        }
        if (refillTime == maxRefillTime && amountOfSpheres < maxAmountOfSpheres)
        {
            sphereImage.fillAmount = 1;
        }
        if(amountOfSpheres == maxAmountOfSpheres)
        {
            amountOfSpheresText.gameObject.SetActive(false);
            sphereImage.fillAmount = 0;
        }

        amountOfSpheresText.text = amountOfSpheres.ToString();
    }

     float UllimateCooldown(float abilityCooldown)
    {
        abilityCooldown -= Time.deltaTime;
        return abilityCooldown;
    }

    void UltimateCooldownUi(float cooldown, float maxCoooldown, Image ultimateImage)
    {
        if(cooldown <= maxCoooldown && cooldown>0)
        {
            ultimateImage.fillAmount -= 1 / maxCoooldown * Time.deltaTime;
        }
        if(cooldown<=0)
        {
            ultimateImage.fillAmount = 0;
        }
    }
    void lol()
    {
        amountOfExortSpheres = maxAmountOfSpheres;
        amountOfQuasSpheres = maxAmountOfSpheres;
        amountOfWexSpheres = maxAmountOfSpheres;
        refillExortTime = maxRefillTime;
        refillWexTime = maxRefillTime;
        refillQuasTime = maxRefillTime;
    }
}

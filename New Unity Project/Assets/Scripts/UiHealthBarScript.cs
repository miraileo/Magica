using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiHealthBarScript : MonoBehaviour
{
    public RectTransform healthBar;
    public RectTransform manaBar;
    public Text healthRegenBar;
    public Text manaRegenBar;
    public float Health, MaxHealth, Width, Height, Mana, MaxMana;
    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }
    public void SetMaxMana(float maxMana)
    {
        MaxMana = maxMana;
    }
    public void SetHealth(float health, float healthRegen)
    {
        Health= health;
        float newWidth = (Health / MaxHealth) * Width;

        healthBar.sizeDelta = new Vector2(newWidth, Height);
        healthRegenBar.text = "+" + healthRegen.ToString();
    }
    public void SetMana(float mana, float manaRegen)
    {
        Mana = mana;
        float newWidth = (Mana / MaxMana) * Width;

        manaBar.sizeDelta = new Vector2(newWidth, Height);
        manaRegenBar.text = "+" + manaRegen.ToString();
    }
}

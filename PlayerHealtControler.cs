using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealtControler : MonoBehaviour
{
    public static PlayerHealtControler instance;

    public int currentHealh;
    public int maxHealth;
    public string example;

    public float damageinvinclength = 1f;
    private float invincCount;
    public int Hurt,Death;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealh = maxHealth;
        maxHealth = CharacterTracker.instance.MaxHealth;
        currentHealh = CharacterTracker.instance.currentHealth;

        

        UIController.instance.HealthBar.maxValue = maxHealth;
        UIController.instance.HealthBar.value = currentHealh;
        UIController.instance.HealthText.text = currentHealh.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCount > 0)
        {
            invincCount -= Time.deltaTime;
            
        }
        else
        {
            Playercontrol.instance.bodySR.color = new Color(Playercontrol.instance.bodySR.color.r, Playercontrol.instance.bodySR.color.g,Playercontrol.instance.bodySR.color.b, 1f);
        }
    }

    public void DamagePlayer(int Damages)
    {
        if (invincCount <= 0)
        {


            currentHealh = currentHealh - Damages;
            audioManager.instance.playSFX(Hurt);
            invincCount = damageinvinclength;

            Playercontrol.instance.bodySR.color = new Color(Playercontrol.instance.bodySR.color.r, Playercontrol.instance.bodySR.color.g, Playercontrol.instance.bodySR.color.b, .3f);

            if (currentHealh <= 0)
            {
                Playercontrol.instance.gameObject.SetActive(false);

                UIController.instance.DeathScreen.SetActive(true);
                audioManager.instance.PlayGameOver();
                audioManager.instance.playSFX(Death);
            }
            UIController.instance.HealthBar.value = currentHealh;
            UIController.instance.HealthText.text = currentHealh.ToString() + " / " + maxHealth.ToString();
        }
    }
    public void makeInvincible(float lenght)
    {
        invincCount = lenght;
    }
    public void HealPlayer(int healAmount)
    {
        currentHealh += healAmount;
        if(currentHealh > maxHealth)
        {
            currentHealh = maxHealth;
        }
        UIController.instance.HealthBar.value = currentHealh;
        UIController.instance.HealthText.text = currentHealh.ToString() + " / " + maxHealth.ToString();
    }
    public void increaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealh += amount;
        UIController.instance.HealthBar.maxValue = maxHealth;
        UIController.instance.HealthBar.value = currentHealh;
        UIController.instance.HealthText.text = currentHealh.ToString() + " / " + maxHealth.ToString();

    }
}

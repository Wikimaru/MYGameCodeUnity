using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject buyMessage;

    private bool inBuyZone = false;

    public bool isHealthRestrore, isHealthUpgrade, isWeapon;

    public int itemCost;

    public int HealthUpGrade;

    public Gun[] potentialGuns;
    private Gun theGun;
    public SpriteRenderer gunSprite;
    public Text infoText;


    // Start is called before the first frame update
    void Start()
    {
        if (isWeapon)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];

            gunSprite.sprite = theGun.gunShopSprite;
            infoText.text = theGun.weaponName + "\n - " + theGun.itemCost + "Gold -";
            itemCost = theGun.itemCost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(levelmanager.instance.currentCoins >= itemCost)
                {
                    levelmanager.instance.SpendCoins(itemCost);

                    if (isHealthRestrore)
                    {
                        PlayerHealtControler.instance.HealPlayer(PlayerHealtControler.instance.maxHealth);
                    }
                    if (isHealthUpgrade)
                    {
                        PlayerHealtControler.instance.increaseMaxHealth(HealthUpGrade);
                    }
                    if (isWeapon)
                    {
                        Gun gunclone = Instantiate(theGun);
                        gunclone.transform.parent = Playercontrol.instance.gunHand;
                        gunclone.transform.position = Playercontrol.instance.gunHand.position;
                        gunclone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunclone.transform.localScale = Vector3.one;

                        Playercontrol.instance.availableGun.Add(gunclone);

                        //Playercontrol.instance.currentGun = Playercontrol.instance.availableGun.Count + 1;

                        Playercontrol.instance.switchGun();
                    }


                    gameObject.SetActive(false);
                    inBuyZone = false;
                    audioManager.instance.playSFX(18);
                }
                else
                {
                    audioManager.instance.playSFX(19);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}

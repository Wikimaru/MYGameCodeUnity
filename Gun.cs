using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Gun : MonoBehaviour
{
    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBeTweenShot;
    private float shotCounter;

    public string weaponName;
    public Sprite gunUI;

    public int itemCost;
    public Sprite gunShopSprite;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //------------incontro Var-----------------//
        var inputDrivce = InputManager.ActiveDevice;
        //-----------------------------------------//

        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }
        else
        {



            if (Playercontrol.instance.canMove && !levelmanager.instance.pause)
            {
                if (Input.GetMouseButtonDown(0)|| Input.GetMouseButton(0))
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = timeBeTweenShot;
                    audioManager.instance.playSFX(18);
                }
                //if (Input.GetMouseButton(0)||Input.GetKeyDown(KeyCode.Joystick1Button1))
                /*if (inputDrivce.RightStick || Input.GetMouseButton(0))
                {
                    shotCounter = shotCounter - Time.deltaTime;

                    if (shotCounter <= 0)
                    {
                        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        audioManager.instance.playSFX(18);

                        shotCounter = timeBeTweenShot;
                    }
                }*/
            }
        }
    }
}

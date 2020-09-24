using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Playercontrol : MonoBehaviour
{
    public static Playercontrol instance;
    public Transform aimPoint;

    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D theRB;

    public Transform gunHand;

    //private Camera thecam;

    public Animator anim;

 /*   public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBeTweenShot;
    private float shotCounter;*/
    public SpriteRenderer bodySR;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashlengeth = .5f,dashCooldown = 1f,dashinvinibility = .5f;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCounter;

    public int Dash,shoot;
    [HideInInspector]
    public bool canMove = true;

    public List<Gun> availableGun = new List<Gun>();
    [HideInInspector]
    public int currentGun;

    TouchButtonControl target;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Startme update
    void Start()
    { 
        //thecam = Camera.main;



        activeMoveSpeed = moveSpeed;

        var inputDrivce = InputManager.ActiveDevice;

        target = GetComponent<TouchButtonControl>();

        UIController.instance.currentGun.sprite = availableGun[currentGun].gunUI;
        UIController.instance.GunText.text = availableGun[currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        var inputDrivce = InputManager.ActiveDevice;
        if (canMove && !levelmanager.instance.pause)
        {
            //windows controller
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            //phone controller
            //moveInput.x = inputDrivce.LeftStickX;
            //moveInput.y = inputDrivce.LeftStickY;

            moveInput.Normalize();

            theRB.velocity = moveInput * activeMoveSpeed;

            //transform.position += new Vector3(moveInput.x * moveSpeed * Time.deltaTime, moveInput.y * moveSpeed * Time.deltaTime, 0f);
            //transform.position += new Vector3(0.01f, 0, 0);




            //-------------incontro Aimpoint--------------//
            //aimPoint.localPosition = new Vector3(Mathf.Abs(inputDrivce.RightStickX),inputDrivce.RightStickY, 0);


            Vector3 mousePos = Input.mousePosition;
           
            // #####JoyPhone Controller#####
            /*float RotationX = inputDrivce.RightStickX;
            float RotationY = inputDrivce.RightStickY;
            float angles = Mathf.Atan2(RotationY, RotationX);
            gunHand.rotation = Quaternion.Euler(0, 0, angles * Mathf.Rad2Deg);*/
            Vector3 screenPoint = CamaraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);
           
           // if (mousePos.x < screenPoint.x)
           if( mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunHand.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunHand.localScale = Vector3.one;
            }


            //rotate gunhand
            
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunHand.rotation = Quaternion.Euler(0, 0, angle);
             

            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            /* if (Input.GetMouseButtonDown(0))
             {
                 Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                 shotCounter = timeBeTweenShot;
                 audioManager.instance.playSFX(shoot);
             }
             //if (Input.GetMouseButton(0)||Input.GetKeyDown(KeyCode.Joystick1Button1))
             if (inputDrivce.RightStick || Input.GetMouseButton(0))
             {
                 shotCounter = shotCounter - Time.deltaTime;

                 if (shotCounter <= 0)
                 {
                     Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                     audioManager.instance.playSFX(shoot);

                     shotCounter = timeBeTweenShot;
                 }
             }*/

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(availableGun.Count > 0)
                {
                    currentGun++;
                    if(currentGun >= availableGun.Count)
                    {
                        currentGun = 0;
                    }

                    switchGun();
                }
                else
                {
                    Debug.LogError("Player has no gun!");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)||inputDrivce.Action1)
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashlengeth;
                    audioManager.instance.playSFX(Dash);
                    PlayerHealtControler.instance.makeInvincible(dashinvinibility);
                    anim.SetTrigger("dash");
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCounter;
                }
            }
            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }



    }


    public void switchGun()
    {

        foreach(Gun theGun in availableGun)
        {
            theGun.gameObject.SetActive(false);
        }

        availableGun[currentGun].gameObject.SetActive(true);
        UIController.instance.currentGun.sprite = availableGun[currentGun].gunUI;
        UIController.instance.GunText.text = availableGun[currentGun].weaponName;

    }
}

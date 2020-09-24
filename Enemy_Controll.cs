using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controll : MonoBehaviour
{
    public static Enemy_Controll instance;
    public Rigidbody2D theRB;
    public float movespeed;

    
    [Header("chase PLayer")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    [Header("Run Away")]
    public bool shouldRunAway;
    public float runAwayRange;
    [Header("Wandering")]
    public bool ShouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;
    [Header("Patrolling")]
    public bool shoulPatrol;
    public Transform[] patronPoint;
    private int currentPoint;

    
    [Header("Shooting")]
    public bool shouldShoot;

    public GameObject bullet;
    public Transform firePoint;
    public float fireCounter;
    public float fireRate;
    
    public float range;
    [Header("Variable")]
    public int Hurt, Death;
    public Animator anim;
    public SpriteRenderer theBody;
    public int health = 100;

    public GameObject[] deathEffect;
    public GameObject Hiteffect;
    // Start is called before the first frame update
    void Start()
    {
        if (ShouldWander)
        {
            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(theBody.isVisible&& Playercontrol.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;
            if (Vector3.Distance(transform.position, Playercontrol.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer)
            {
                moveDirection = Playercontrol.instance.transform.position - transform.position;
            }
            else
            {
                if (ShouldWander)
                {
                    if(wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        //move the enemy
                        moveDirection = wanderDirection;
                        if(wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauseLength*.75f,pauseLength*1.25f);
                        }
                    }
                    if (pauseCounter>0)
                    {
                        pauseCounter -= Time.deltaTime;
                        if (pauseCounter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.25f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), 0f);
                        }
                    }
                }

                if (shoulPatrol)
                {
                    moveDirection = patronPoint[currentPoint].position - transform.position;

                    if(Vector3.Distance(transform.position, patronPoint[currentPoint].position)< .2f)
                    {
                        currentPoint++;
                        if(currentPoint >= patronPoint.Length)
                        {
                            currentPoint = 0;
                        }
                    }
                }

            }
            if(shouldRunAway && Vector3.Distance(transform.position,Playercontrol.instance.transform.position) < runAwayRange)
            {
                moveDirection = transform.position - Playercontrol.instance.transform.position;
            }

            moveDirection.Normalize();

            theRB.velocity = moveDirection * movespeed;

            if (moveDirection != Vector3.zero)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }

            if (shouldShoot && Vector3.Distance(transform.position,Playercontrol.instance.transform.position) < range)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }



    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        audioManager.instance.playSFX(Hurt);
        Instantiate(Hiteffect, transform.position, transform.rotation);
        if(health <= 0)
        {
            Destroy(gameObject);

            int selected = Random.Range(0,deathEffect.Length);
            int rotations = Random.Range(0, 360);
            audioManager.instance.playSFX(Death);
            Instantiate(deathEffect[selected], transform.position, Quaternion.Euler(0f,0f,rotations));
        }
    }
}

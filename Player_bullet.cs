using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet : MonoBehaviour
{
    public float Speed = 7.5f;

    public Rigidbody2D theRB;

    public GameObject impacteffect;

    public int Damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impacteffect, transform.position, transform.rotation);
        Destroy(gameObject);

        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy_Controll>().DamageEnemy(Damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

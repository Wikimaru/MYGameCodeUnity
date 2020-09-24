using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSc : MonoBehaviour
{

    public float Speed;
    private Vector3 drirection;
    // Start is called before the first frame update
    void Start()
    {
        drirection = Playercontrol.instance.transform.position - transform.position;
        drirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += drirection * Speed * Time.deltaTime ;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealtControler.instance.DamagePlayer(1);
        }

        Destroy(gameObject);
    }
}

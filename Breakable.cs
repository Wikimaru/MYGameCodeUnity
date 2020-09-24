using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] BrokenPiece;
    public int maxPiece = 5;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;
    public int breaksound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash()
    {
        //show broken pieces
        int prieceToDrop = Random.Range(1, maxPiece);

        for (int i = 0; i < prieceToDrop; i++)
        {
            int randomPiece = Random.Range(0, BrokenPiece.Length);
            Instantiate(BrokenPiece[randomPiece], transform.position, transform.rotation);
        }
        audioManager.instance.playSFX(breaksound);
        Destroy(gameObject);
        //drop item
        if (shouldDropItem)
        {
            float dropChance = Random.Range(0f, 100f);

            if (dropChance <= itemDropPercent)
            {
                int randomitem = Random.Range(0, itemsToDrop.Length);
                Instantiate(itemsToDrop[randomitem], transform.position, transform.rotation);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(Playercontrol.instance.dashCounter > 0)
            {
                Smash();
            }
            
        }
        if(other.tag == "PlayerBullet")
        {
            Smash();
        }
    }
}

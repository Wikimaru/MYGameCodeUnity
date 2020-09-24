using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public int coinValue = 1;

    public float waittoBeCollected;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waittoBeCollected > 0)
        {
            waittoBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waittoBeCollected <= 0)
        {
            levelmanager.instance.GetCoins(coinValue);
            audioManager.instance.playSFX(5);

            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpack : MonoBehaviour
{
    public int healsound;
    public int healAmout = 1;
    public float waittoBeCollected = .5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waittoBeCollected > 0)
        {
            waittoBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && waittoBeCollected <= 0 && PlayerHealtControler.instance.currentHealh != PlayerHealtControler.instance.maxHealth)
        {
            PlayerHealtControler.instance.HealPlayer(healAmout);
            audioManager.instance.playSFX(healsound);

            Destroy(gameObject);
        }
    }
}

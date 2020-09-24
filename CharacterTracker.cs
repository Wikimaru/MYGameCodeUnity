using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    // Start is called before the first frame update

    public static CharacterTracker instance;

    public int currentHealth, MaxHealth, currentCoins;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

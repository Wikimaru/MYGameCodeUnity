using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoryscreen : MonoBehaviour
{

    public float waitforanykey = 2f;

    public GameObject anyKeyText;

    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitforanykey > 0)
        {
            waitforanykey -= Time.deltaTime;
            if(waitforanykey <= 0)
            {
                anyKeyText.SetActive(true);
            }
            
        }
        else
        {
            if (Input.anyKeyDown)                
            {
                print("f");
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }
}

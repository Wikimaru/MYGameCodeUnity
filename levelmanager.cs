using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelmanager : MonoBehaviour
{
    public static levelmanager instance;

    public float waitToload = 4f;

    public string nextlevel;

    public bool pause;

    public int currentCoins = 0;

    public Transform startPoint;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Playercontrol.instance.transform.position = startPoint.position;
        Playercontrol.instance.canMove = true;

        currentCoins = CharacterTracker.instance.currentCoins;
        Time.timeScale = 1f;
        UIController.instance.coinText.text = currentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseunPause();
        }
    }

    public IEnumerator LevelEnd()
    {
        audioManager.instance.PlayLevelWin();

        Playercontrol.instance.canMove = false;

        UIController.instance.StartFadeToblack();

        yield return new WaitForSeconds(waitToload);

        CharacterTracker.instance.currentCoins = currentCoins;
        CharacterTracker.instance.currentHealth = PlayerHealtControler.instance.currentHealh;
        CharacterTracker.instance.MaxHealth = PlayerHealtControler.instance.maxHealth;

        SceneManager.LoadScene(nextlevel);
    }

    public void PauseunPause()
    {
        if (!pause)
        {
            UIController.instance.pauseMenu.SetActive(true);

            pause = true;

            Time.timeScale = 0f;
        }
        else 
        {
            UIController.instance.pauseMenu.SetActive(false);

            pause = false;
            Time.timeScale = 1f;
        }
    }
    public void GetCoins(int amount)
    {
        currentCoins += amount;
        UIController.instance.coinText.text = currentCoins.ToString();
    }
    public void SpendCoins(int amount)
    {
        currentCoins -= amount;
        if(currentCoins < 0)
        {
            currentCoins = 0;
        }

        UIController.instance.coinText.text = currentCoins.ToString() ;
    }
}

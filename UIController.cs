using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider HealthBar;
    public Text HealthText;
    
    public GameObject DeathScreen;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, FadeoutBlack;

    public GameObject pauseMenu,MapDisplay;

    public string newGamescene, mainMenuScene;

    public Text coinText;

    public Image currentGun;
    public Text GunText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        FadeoutBlack = true;
        fadeToBlack = false;

        currentGun.sprite = Playercontrol.instance.availableGun[Playercontrol.instance.currentGun].gunUI;
        GunText.text = Playercontrol.instance.availableGun[Playercontrol.instance.currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeoutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                FadeoutBlack = false;
            }
        }
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToblack()
    {
        fadeToBlack = true;
        FadeoutBlack = false;
    }
    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGamescene);
    }
    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        levelmanager.instance.PauseunPause();
    }
}

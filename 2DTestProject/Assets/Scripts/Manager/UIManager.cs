using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject HealthBar;
    public GameObject gameOverPanel;

    [Header("UI Elements")]
    public GameObject pauseMenu;

    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
       else
        {
            Destroy(gameObject);
        }

       
    }

    public void UpdateHealth(float currentHealth)
    {
        switch(currentHealth)
        {
            case 3:
                HealthBar.transform.GetChild(0).gameObject.SetActive(true);
                HealthBar.transform.GetChild(1).gameObject.SetActive(true);
                HealthBar.transform.GetChild(2).gameObject.SetActive(true);
                break;            
            case 2:
                HealthBar.transform.GetChild(0).gameObject.SetActive(true);
                HealthBar.transform.GetChild(1).gameObject.SetActive(true);
                HealthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;            
            case 1:
                HealthBar.transform.GetChild(0).gameObject.SetActive(true);
                HealthBar.transform.GetChild(1).gameObject.SetActive(false);
                HealthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;            
            case 0:
                HealthBar.transform.GetChild(0).gameObject.SetActive(false);
                HealthBar.transform.GetChild(1).gameObject.SetActive(false);
                HealthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1;
    }

    public void GameOverUI(bool playerDead)
    {
        gameOverPanel.SetActive(playerDead);
    }

}

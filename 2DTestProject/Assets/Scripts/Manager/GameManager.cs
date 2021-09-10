using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerController player;
    private DoorController doorExit;

    public bool gameOver;

    public List<Enemy> enemies = new List<Enemy>();


    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        player = FindObjectOfType<PlayerController>();

        doorExit = FindObjectOfType<DoorController>();
    }

    public void Update()
    {
        gameOver = player.isDead;
        UIManager.instance.GameOverUI(gameOver);
    }

    public void IsEnemy(Enemy enemyObj)
    {
        enemies.Add(enemyObj);
    }

    public void EnemyDead(Enemy enemyObj)
    {
        enemies.Remove(enemyObj);
        if(enemies.Count == 0)
        {
            doorExit.OpenDoor();
        }
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}

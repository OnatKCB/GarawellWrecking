using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    public GameStatus Status;
    public enum GameStatus
    {
        Win, Lose, Playing
    }
    #endregion
    #region Singleton Pattern 
    public static GameManager Instance;
    #endregion
    #region Monobehaviour
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }    
        else
        {
            Instance = this;
            Status = GameStatus.Playing;
        }
    }
    #endregion
    #region Restart The Game
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
    #region Game Statuses 
    public void GamePlayStatus()
    {
        if(Status == GameStatus.Playing)
        {
            Time.timeScale = 1;
        }
        if(Status == GameStatus.Win || Status == GameStatus.Lose)
        {
            Time.timeScale = 0;
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarsManager : MonoBehaviour
{
    #region Singleton
	public static EnemyCarsManager Instance;
    #endregion
    #region Variables
    public List<GameObject> enemyCars = new List<GameObject>();
    public bool isEmptyCarList = false;
    #endregion
    #region Monobehavior
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (GameManager.Instance.Status == GameManager.GameStatus.Playing)
            return;
            if (enemyCars.Count <= 0)
            {
                isEmptyCarList = true;
                GameManager.Instance.Status = GameManager.GameStatus.Win;
            }
    }
    #endregion
}

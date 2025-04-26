using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private UpdateUI uiManager;
    public int enemiesLiving = 0;
    private GameObject enemyList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        enemyList = GameObject.FindGameObjectWithTag("EnemyList");
        if (enemyList != null)
        {
            enemiesLiving = enemyList.transform.childCount;
        }
        uiManager = GetComponent<UpdateUI>();
        uiManager.UpdateObjectiveBoard();
    }
    
    public void OnLevelLoaded()
    {
        enemyList = GameObject.FindGameObjectWithTag("EnemyList");
        if (enemyList != null)
        {
            enemiesLiving = enemyList.transform.childCount;
        }
        UpdateUI();
    }
    
    
    public void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateObjectiveBoard();
        }
        else
        {
            Debug.LogError("UI Manager not assigned!");
        }
    }

}

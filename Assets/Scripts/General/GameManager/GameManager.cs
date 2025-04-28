using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
            uiManager.UpdateStatsMenu();
        }
        else
        {
            Debug.LogError("UI Manager not assigned!");
        }
    }

}

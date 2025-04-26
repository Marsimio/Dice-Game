using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    public KeyCode clearKey = KeyCode.C; // Set to keycode for debugging purposes, switch to inputManager later

    private Transform enemyList;

    void Awake()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }
    void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnLevelLoaded();
    }
    public void OnLevelLoaded()
    {
        GameObject enemyListObject = GameObject.FindGameObjectWithTag("EnemyList");
        if (enemyListObject != null)
        {
            enemyList = enemyListObject.transform;
        }
    }
    void Start()
    {
        GameObject enemyListObject = GameObject.FindGameObjectWithTag("EnemyList");
        if (enemyListObject != null)
        {
            enemyList = enemyListObject.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(clearKey) && enemyList != null)
        {
            ClearChildren(enemyList);
            GameManager.instance.enemiesLiving = 0;
            GameManager.instance.UpdateUI();
        }
    }

    void ClearChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}

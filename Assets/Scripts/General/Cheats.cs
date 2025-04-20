using UnityEngine;

public class Cheats : MonoBehaviour
{
    public KeyCode clearKey = KeyCode.C; // Set to keycode for debugging purposes, switch to inputManager later

    private Transform enemyList;

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

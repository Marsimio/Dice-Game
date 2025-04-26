using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UpdateUI : MonoBehaviour
{
    public UIDocument uiDocument;

    private VisualElement root;
    private VisualElement objectivesMenu;
    private Label obj1;
    private Label obj2;
    private Label obj3;

    private void Awake()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
        TryInitUIDocument();
    }


    void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnLevelLoaded();
    }
    public void OnLevelLoaded()
    {
        TryInitUIDocument();

        if (uiDocument != null)
        {
            GameManager.instance.UpdateUI();
        }
    }
    
    void TryInitUIDocument()
    {
        if (uiDocument == null)
        {
            GameObject uiObj = GameObject.FindWithTag("UI");
            if (uiObj != null)
            {
                uiObj.TryGetComponent(out uiDocument);
            }
        }

        if (uiDocument == null) return;

        root           = uiDocument.rootVisualElement;
        objectivesMenu = root.Q<VisualElement>("ObjectiveBoard");
        obj1           = objectivesMenu.Q<Label>("Obj1");
        obj2           = objectivesMenu.Q<Label>("Obj2");
        obj3           = objectivesMenu.Q<Label>("Obj3");
    }
    
    public void UpdateObjectiveBoard()
    {
        GameObject enemyList = GameObject.FindGameObjectWithTag("EnemyList");
        if (enemyList == null)
        {
            Debug.LogWarning("No GameObject with tag 'EnemyList' found.");
            return;
        }

        obj1.text = "Enemies left: " + GameManager.instance.enemiesLiving;
    }
}
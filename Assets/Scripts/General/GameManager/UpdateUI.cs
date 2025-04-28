using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class UpdateUI : MonoBehaviour
{
    public UIDocument objUIDocument;
    public UIDocument statsUIDocument;

    private VisualElement objRoot;
    private VisualElement objectivesMenu;
    private Label obj1;
    private Label obj2;
    private Label obj3;
    
    private VisualElement statsRoot;
    private VisualElement statsMenu;
    private Label health;
    private Label money;
    
    private void Start()
    {
        InitUIDocument();

        UpdateObjectiveBoard();
        UpdateStatsMenu();
    }
    
    void InitUIDocument()
    {
        if (objUIDocument == null)
        {
            GameObject uiObj = GameObject.FindWithTag("ObjUI");
            objUIDocument = uiObj.GetComponent<UIDocument>();
        }
        
        if (statsUIDocument == null)
        {
            GameObject uiStats = GameObject.FindWithTag("StatsUI");
            statsUIDocument = uiStats.GetComponent<UIDocument>();
        }

        objRoot           = objUIDocument.rootVisualElement;
        objectivesMenu = objRoot.Q<VisualElement>("ObjectiveBoard");
        obj1           = objectivesMenu.Q<Label>("Obj1");
        obj2           = objectivesMenu.Q<Label>("Obj2");
        obj3           = objectivesMenu.Q<Label>("Obj3");
        
        statsRoot           = statsUIDocument.rootVisualElement;
        statsMenu = statsRoot.Q<VisualElement>("StatsBox");
        health           = statsMenu.Q<Label>("Health");
        money           = statsMenu.Q<Label>("Money");
        
        
    }
    
    public void UpdateObjectiveBoard()
    {
        obj1.text = "Enemies left: " + GameManager.instance.enemiesLiving;
    }

    public void UpdateStatsMenu()
    {
        health.text = StatsManager.instance.currentHealth.ToString();
        money.text = StatsManager.instance.money.ToString();
    }
}
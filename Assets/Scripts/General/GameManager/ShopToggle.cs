using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class ShopToggle : MonoBehaviour
{
    public UIDocument ShopUIDocument;
    
    private VisualElement shopRoot;
    private Button BuyButtonHP;

    private bool isShopVisible = false;
    
    private void Start()
    {
        shopRoot = ShopUIDocument.rootVisualElement;
        BuyButtonHP = shopRoot.Q<Button>("BuyButton");
        BuyButtonHP.RegisterCallback<ClickEvent>(evt => HealPlayer());
        shopRoot.style.display = DisplayStyle.None;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isShopVisible = !isShopVisible;
            shopRoot.style.display = isShopVisible ? DisplayStyle.Flex : DisplayStyle.None;
            Cursor.visible = isShopVisible;
            Cursor.lockState = isShopVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void HealPlayer()
    {
        if (StatsManager.instance.money >= 2)
        {
            FindFirstObjectByType<PlayerActions>().HealDamage(1); // figure out how to reference the player better
            StatsManager.instance.money -= 2;
            GameManager.instance.UpdateUI();
        }
    }
}

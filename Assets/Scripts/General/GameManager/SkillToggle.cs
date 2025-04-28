using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class SkillToggle : MonoBehaviour
{
    public UIDocument SkillUIDocument;
        
    private VisualElement skillRoot;
    private Button BuySkill1Button;
    private Button BuySkill2Button;
    private Button BuySkill3Button;

    private bool isShopVisible = false;
    private bool skill1Bought = false;
    private bool skill2Bought = false;
    private bool skill3Bought = false;
    
    private void Start()
    {
        skillRoot = SkillUIDocument.rootVisualElement;
        BuySkill1Button = skillRoot.Q<Button>("BuySkill1");
        BuySkill2Button = skillRoot.Q<Button>("BuySkill2");
        BuySkill3Button = skillRoot.Q<Button>("BuySkill3");
        BuySkill1Button.RegisterCallback<ClickEvent>(evt => BuySkill1());
        BuySkill2Button.RegisterCallback<ClickEvent>(evt => BuySkill2());
        BuySkill3Button.RegisterCallback<ClickEvent>(evt => BuySkill3());
        skillRoot.style.display = DisplayStyle.None;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isShopVisible = !isShopVisible;
            skillRoot.style.display = isShopVisible ? DisplayStyle.Flex : DisplayStyle.None;
            Cursor.visible = isShopVisible;
            Cursor.lockState = isShopVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void BuySkill1()
    {
        if (StatsManager.instance.money >= 1 && !skill1Bought)
        {
            StatsManager.instance.maxHealth++;
            FindFirstObjectByType<PlayerActions>().HealDamage(1); // figure out how to reference the player better
            StatsManager.instance.money -= 1;
            GameManager.instance.UpdateUI();
            skill1Bought = true;
            BuySkill2Button.text = "Purchase";
            BuySkill1Button.text = "Bought";
        }
    }
    private void BuySkill2()
    {
        if (StatsManager.instance.money >= 1 && skill1Bought && !skill2Bought)
        {
            StatsManager.instance.maxHealth++;
            FindFirstObjectByType<PlayerActions>().HealDamage(1); // figure out how to reference the player better
            StatsManager.instance.money -= 1;
            GameManager.instance.UpdateUI();
            skill2Bought = true;
            BuySkill2Button.text = "Bought";
        }
    }
    private void BuySkill3()
    {
        if (StatsManager.instance.money >= 2 && !skill3Bought)
        {
            StatsManager.instance.attackDamage++;
            StatsManager.instance.money -= 1;
            GameManager.instance.UpdateUI();
            skill3Bought = true;
            BuySkill3Button.text = "Bought";
        }
    }

}

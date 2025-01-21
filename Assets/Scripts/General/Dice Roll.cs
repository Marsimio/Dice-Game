using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class DiceRoll : MonoBehaviour
{
    public VisualElement ui;

    public GroupBox diceBox;
    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        diceBox = ui.Q<GroupBox>("DiceBox");
    }

    void RollDice(int diceCount, int faceCount)
    {
        List<int> diceValues = new List<int>();

        for (int i = 0; i < diceCount; i++)
        {
            int roll = Random.Range(1, faceCount + 1);
            diceValues.Add(roll);
        }
    }
}

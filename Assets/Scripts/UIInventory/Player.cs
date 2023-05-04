using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int Hunger;
    public int Exp;

    public TMP_Text HungerText;
    public TMP_Text ExpText;

    public void Awake()
    {
        Instance = this;
    }

    public void IncreaseHealth(int value)
    {
        Hunger += value;
        HungerText.text = $"HP :{Hunger}";
    }
    public void IncreaseExp(int value)
    {
        Exp += value;
        ExpText.text = $"HP :{Exp}";
    }
}

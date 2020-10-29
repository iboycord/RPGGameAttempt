using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHud : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text HPText;
    public TMP_Text SPText;
    public TMP_Text levelText;
    public Slider hpSlider;

    public void SetHUD(CharacterStats unit)
    {
        nameText.text = unit.charName;
        HPText.text = unit.hp.GetCurrentValue().ToString() + "/" + unit.hp.GetMaxValue();
        SPText.text = unit.currentSP.ToString() + "/" + unit.maxSP.GetValue();
        levelText.text = "Level " + unit.level;
        //hpSlider.maxValue = unit.maxHP.GetValue();
        //hpSlider.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}

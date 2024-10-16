using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI lvlText;
    public Slider hpSlider;

    public void SetHUD(Fighter fighter)
    {
        nameText.text = fighter.name;
        lvlText.text = fighter.level + " lvl";
        hpSlider.maxValue = fighter.maxHP;
        hpSlider.value = fighter.hp;

    }

    public void SetEnemyHUD(FighterEnemy fighter)
    {
        nameText.text = fighter.name;
        lvlText.text = fighter.level + " lvl";
        hpSlider.maxValue = fighter.maxHP;
        hpSlider.value = fighter.hp;

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}

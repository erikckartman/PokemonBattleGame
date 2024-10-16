using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPokemon", menuName = "ScriptableObjects/Pokemons")]
public class FighterSC : ScriptableObject
{
    public string name;
    public int maxHP;
    public int hp;
    public int damage;
    public int poisoning;
    public int level;
    public int defence;

    public bool TakeDamage(int dmg)
    {
        hp -= dmg;

        if(hp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TakeDefence(int psn)
    {
        defence -= psn;

        if (hp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

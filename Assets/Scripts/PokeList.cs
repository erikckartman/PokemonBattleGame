using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "ScriptableObjects/Pokemon")]
public class PokeList : ScriptableObject
{
    public string name;
    public int maxHP;
    public int hp;
    public int damage;
    public int poisoning;
    public int level;
    public int defence;
    public Sprite pokemon;
    public Vector3 pokeScale;
}

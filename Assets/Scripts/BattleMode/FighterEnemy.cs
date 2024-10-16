using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : MonoBehaviour
{
    [Header("Scrptable Object")]
    public PokeList pokemon;
    public int number;

    [Header("Pokemon parametrs")]
    public string name;
    public int maxHP;
    public int hp;
    public int damage;
    public int poisoning;
    public int level;
    public int defence;

    [Header("Pokemons")]
    public PokeList pikachu;
    public PokeList squirtle;
    public PokeList chermander;
    public PokeList pidgeotto;
    public PokeList mewtwo;
    public PokeList koffing;
    public PokeList haunter;

    private void Awake()
    {
        number = Random.Range(1, 8);

        switch (number)
        {
            case 1:
                pokemon = pikachu;
                break;
            case 2:
                pokemon = squirtle;
                break;
            case 3:
                pokemon = chermander;
                break;
            case 4:
                pokemon = pidgeotto;
                break;
            case 5:
                pokemon = mewtwo;
                break;
            case 6:
                pokemon = haunter;
                break;
            case 7:
                pokemon = koffing;
                break;
            default:
                Debug.Log("FUCK THIS SHIT!");
                break;
        }

        if (pokemon != null)
        {
            name = pokemon.name;
            maxHP = pokemon.maxHP;
            hp = pokemon.hp;
            damage = pokemon.damage;
            poisoning = pokemon.poisoning;
            level = pokemon.level;
            defence = pokemon.defence;       
        }
    }

    public void SpawnPokemon()
    {
        GameObject pkmn = new GameObject("PokeSprite");
        SpriteRenderer spriteRenderer = pkmn.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = pokemon.pokemon;

        pkmn.transform.position = new Vector2(0, 0.6f);
        
        pkmn.transform.localScale = pokemon.pokeScale;

        pkmn.transform.SetParent(this.transform, false);
    }

    public bool TakeDamage(int dmg)
    {
        hp -= dmg;

        if(hp == 0 || hp < 0)
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

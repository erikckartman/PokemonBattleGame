using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
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
    public Sprite pokeSprite;

    [Header("Pokemons")]
    public PokeList pikachu;
    public PokeList squirtle;
    public PokeList chermander;
    public PokeList pidgeotto;

    private void Awake()
    {
        SetPokemon();
    }

    public void SetPokemon()
    {
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
            pokeSprite = pokemon.pokemon;
        }
    }

    public void SpawnPokemon()
    {
        GameObject pkmn = new GameObject("PokeSprite");
        SpriteRenderer spriteRenderer = pkmn.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = pokeSprite;

        pkmn.transform.position = new Vector2(0, 0.6f);
        
        pkmn.transform.localScale = pokemon.pokeScale * new Vector2(-1f, 1f);

        pkmn.transform.SetParent(this.transform, false);
    }

    public void DestroyPokemon()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Update() { }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Fireball fireball = collider.GetComponent<Fireball>();
        // Если в коллайдер монстра попадает другой коллайдер, и это фаербол, то монстр получает урон
        if (fireball) ReceiveDamage();

        Character character = collider.GetComponent<Character>();

        if (character)
        {
            character.ReceiveDamage();
        }
    }
    
}

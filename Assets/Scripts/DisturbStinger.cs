using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbStinger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        // Если объект это Unit, и его можно привести к типу Character, то есть это игрок, которым играем
        if (unit && unit is Character)
        {
            unit.ReceiveDamage();
        }
    }
}

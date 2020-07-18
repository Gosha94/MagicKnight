using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Виртуальный метод, будем переопределять получение урона
    public virtual void ReceiveDamage()
    {
        Die();
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Передаем при создании фаера объект который его выпустил, чтобы не уничтожались снаряды после создания
    private GameObject parent;
    public GameObject Parent { set { parent = value; } get { return parent; } }

    private float speed = 10.0F;
    private Vector3 direction;
    //Свойство позволит передать(set) извне направление полета фаербола
    public Vector3 Direction { set { direction = value; } }

    // Добавляем выбор цвета для фаеров
    public Color Color
    {
        set { sprite.color = value; }
    }

    // Необходима для получения цвета фаербола
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        // После создания фаера он уничтожается через 1,5с
        Destroy(gameObject, 1.4F);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        
        if (unit && unit.gameObject !=parent )
        {            
            Destroy(gameObject);
        }
    }
}

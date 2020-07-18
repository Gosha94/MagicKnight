using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monster
{
    [SerializeField]
    private float rate = 2.0F;
    [SerializeField]
    private Color fireballColor = Color.white;

    private Fireball fireball;
    protected override void Awake()
    {
        fireball = Resources.Load<Fireball>("FireBall");
    }
    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5F;

        Fireball newFireball = Instantiate(fireball, position, fireball.transform.rotation) as Fireball;

        newFireball.Parent = gameObject;
        newFireball.Direction = -newFireball.transform.right;
        newFireball.transform.rotation *= Quaternion.Euler(0, 0, 180.0F);
        newFireball.Color = fireballColor;
    }
    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.5F) ReceiveDamage();
            else unit.ReceiveDamage();
        }
    }
}

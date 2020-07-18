//using UnityEditor.Experimental.U2D;
using UnityEngine;

public class Character : Unit
{
    [SerializeField]
    private int lives = 5;
    private HealthBar healthBar;
    public int Lives
    {
        get { return lives; }
        set 
        {            
            if (value < 5) lives = value;
            healthBar.Refresh();
        }
    }   

    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;

    // Поле отвечает за нахождение персонажа на земле, по умол. в воздухе
    private bool isGrounded = false;

    private Fireball fireball;

    private CharacterState State
    {
        // Связывааем анимацию через перечисление со свойством State класса Character
        get { return (CharacterState) animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int) value); }
    }

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>(); // Получаем поле из дочернего(вложенного) компонента

        // Подгружаем префаб фаербола из папки Resources
        fireball = Resources.Load<Fireball>("FireBall");
    }
    
    // Стандартный метод, вызываемый Unity постоянно
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (MainMenu.instanse.isStarted == false)
        {
            return;
        }

        // Обнуляем состояние анимации
        if (isGrounded) State = CharacterState.Idle;
        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Vertical")) Jump();
    }
    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        // Перемещаем персонажа по направлению нажатой клавиши со скоростью, умн. на время между тек и пред кадрами, для плавности
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        // FlipX разворот персонажа на 180град. если двигается влево
        sprite.flipX = direction.x < 0.0F;
        // Запускаем анимацию бега
        if (isGrounded) State = CharacterState.Run;
    }
    private void Jump()
    {
        // Единоразовый импульс силы по направлению вверх
        rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void Shoot()
    {
        Vector3 position = transform.position; // Корректировка координаты Y запуска фаера position.y += 0.8F;
        // Создаем экземпляр фаербола на игровом поле с координатами персонажа, и его направлением движения
        Fireball newFireball = Instantiate(fireball, position, fireball.transform.rotation) as Fireball;
        
        newFireball.Parent = gameObject;
        // Определяем направление запуска фаера с пом текущего Flip-a по оси Х игрока
        newFireball.Direction = newFireball.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        newFireball.transform.rotation *= Quaternion.Euler(0, 0, (sprite.flipX ? 180.0F : 1.0F));
    }
    public override void ReceiveDamage()
    {
        // Отнимаем жазни
        if (Lives >= 1) Lives--;
        else
        {
            Die();            
        }

        // Устанавливаем вектор скорости в 0 чтобы при прыжке на препятствие также работал отброс вверх
        rigidBody.velocity = Vector3.zero;
        // Отбрасываем игрока, придаем объекту RigidBody2D силу, подбрасыв. вверх
        rigidBody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);
        Debug.Log("Число жизней: " + lives);
    }
    private void CheckGround()
    {
        // Ищем внутри сферы попадающие в нее коллайдеры объеков, и добавляем в массив
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.98F);
        // Если коллайдеров 2 и больше, т.к. коллайдер 1 это граница персонажа
        isGrounded = colliders.Length > 1;
        
        if (!isGrounded) State = CharacterState.Jump; // Запускаем анимацию прыжка
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Fireball fireball = collider.gameObject.GetComponent<Fireball>();
        if (fireball && fireball.Parent != gameObject)
        {            
            ReceiveDamage();
        }
    }
    protected override void Die()
    {
        Destroy(gameObject);
        MainMenu.instanse.isStarted = false;
    }
}

public enum CharacterState
{
    Idle,
    Run,
    Jump,
}

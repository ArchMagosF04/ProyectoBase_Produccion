
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField] private float Speed = 3f;
    private Rigidbody2D playerRB;
    private Vector2 moveInput;
    public Vector2 MoveInput => moveInput;
    [SerializeField] private PlayerDamage playerDamage;
    private Animator animator;

    //[Header ("Dash Settings")]
    //[SerializeField] private float DashSpeed = 20f;
    //[SerializeField] private float DashDuration = 0.25f;
    [SerializeField] private float DashCoolDown = 1.5f;
    private float DashTimer;
    //private bool isDashing = false;
    //private bool canDash = true;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DashTimer = DashCoolDown;
    }

    private void DashCooldown()
    {
        if (animator.GetBool("CanDash") == false)
        {
            DashTimer -= Time.deltaTime;
            if (DashTimer <= 0)
            {
                animator.SetBool("CanDash",true);
                DashTimer = DashCoolDown;
            }
        }
    }


    void Update()
    {
        CalculateMovment();
        DashCooldown();

        //if (Input.GetKeyDown(KeyCode.Space) && canDash == true)
        //{
        //    StartCoroutine(Dash());
        //}

        if (Input.GetKeyDown(KeyCode.Escape) && ScreenManager.Instance.isLevelComplete==false && ScreenManager.Instance.isDialogueActive==false)
        {
            ScreenManager.Instance.PauseMenuButton();
        }
    }

    public void SendDamage(float damage)
    {
        playerDamage.RecieveDamage(damage);
    }

    public void GoIdle()
    {
        animator.SetBool("IsIdle", true);
    }

    private void FixedUpdate()
    {
        if (animator.GetBool("IsDashing")) { return; }
        playerRB.MovePosition(playerRB.position + moveInput * Speed * Time.fixedDeltaTime);//ejecuta el movimiento.
    }

    private void CalculateMovment()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;//toma los resultados de moveX y moveY para crear un vector que va del -1 al 1,
                                                         //esta informacio se usa en el fixedUpdate para el movimiento.
    } 
    
    //private IEnumerator Dash()//el dash funciona por medio de corutinas
    //{
    //    canDash = false;
    //    isDashing = true;
    //    playerRB.velocity = new Vector2(moveInput.x * DashSpeed, moveInput.y * DashSpeed);
    //    yield return new WaitForSeconds(DashDuration);//espera deteminados segundos, los determinados por la varible que este entro los "()".
    //    isDashing = false;
    //    yield return new WaitForSeconds(DashCoolDown);
    //    canDash = true;
    //}
}
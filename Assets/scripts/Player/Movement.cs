using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField] private float Speed = 3f;
    private Rigidbody2D playerRB;
    private Vector2 MoveInput;
    [Header ("Dash Settings")]
    [SerializeField] private float DashSpeed = 20f;
    [SerializeField] private float DashDuration = 0.25f;
    [SerializeField] private float DashCoolDown = 1.5f;
    private bool isDashing = false;
    private bool canDash = true;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        CalculateMovment();

        if (Input.GetKeyDown(KeyCode.Space) && canDash == true) 
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) { return; }
        playerRB.MovePosition(playerRB.position + MoveInput * Speed * Time.fixedDeltaTime);//ejecuta el movimiento.
    }

    private void CalculateMovment()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(moveX, moveY).normalized;//toma los resultados de moveX y moveY para crear un vector que va del -1 al 1,
                                                         //esta informacio se usa en el fixedUpdate para el movimiento.
    } 
    
    private IEnumerator Dash()//el dash funciona por medio de corutinas
    {
        canDash = false;
        isDashing = true;
        playerRB.velocity = new Vector2(MoveInput.x * DashSpeed, MoveInput.y * DashSpeed);
        yield return new WaitForSeconds(DashDuration);//espera deteminados segundos, los determinados por la varible que este entro los "()".
        isDashing = false;
        yield return new WaitForSeconds(DashCoolDown);
        canDash = true;
    }
}

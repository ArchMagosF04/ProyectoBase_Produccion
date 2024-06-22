using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HigorShield : MonoBehaviour
{
    [SerializeField] private float maxShield = 3;
    private float shield;

    public float Shield => shield;

    [SerializeField] private Slider shieldSlider;

    public bool isInvulnerable=false;

    private Animator animator;

    private bool shieldThreshold1=false;
    private bool shieldThreshold2=false;
    private bool shieldThreshold3=false;

    public bool ShieldThreshold1 => shieldThreshold1;
    public bool ShieldThreshold2 => shieldThreshold2;
    public bool ShieldThreshold3 => shieldThreshold3;  

    public UnityEvent OnShieldBreak = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        shield = maxShield;
        animator = GetComponent<Animator>();
        shieldSlider.maxValue = maxShield;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        shield -= damage;

        Debug.Log("Enemy Health: " + shield);

        if (shield <= maxShield * 0.75 && !shieldThreshold1)
        {
            animator.SetTrigger("Below75");
            shieldThreshold1 = true;
        }
        if (shield <= maxShield * 0.5 && !shieldThreshold2)
        {
            animator.SetTrigger("Below50");
            shieldThreshold2 = true;
        }
        if (shield <= maxShield * 0.25 && !shieldThreshold3)
        {
            animator.SetTrigger("Below25");
            shieldThreshold3 = true;
        }
        if (shield <= 0)
        {
            shield = 0;
            shieldSlider.value = shield;
            ShieldDestroyed();
            isInvulnerable = true;
        }
    }

    public void ShieldDestroyed()
    {
        OnShieldBreak?.Invoke();
        Destroy(gameObject);
    }
    private void OnGUI()
    {
        float smooth = Time.deltaTime / 1f;
        shieldSlider.value = Mathf.Lerp(shieldSlider.value, shield, smooth);
    }
}

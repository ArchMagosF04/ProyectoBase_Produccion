using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class HigorController : MonoBehaviour
{
    [SerializeField] private float lineOfSight;
    [SerializeField] private float flameRange;

    public float LineOfSight => lineOfSight;
    public float FlameRange => flameRange;

    private Transform player;
    private float distanceFromPlayer;
    private Animator animator;
    private Rigidbody2D rigidBody;
    AudioManager audioManager;

    public float DistanceFromPlayer => distanceFromPlayer;

    public bool missileCooldown;
    [SerializeField] private float missileTimer = 3.5f;
    private float missileTimerRecord;

    public bool flameCooldown;
    [SerializeField] private float flameTimer = 15f;
    private float flameTimerRecord;

    private CapsuleCollider2D higorCollider;

    public Transform[] teleportRunes;
    private int lastTeleportLocation = 9;

    public bool shouldSummon = false;

    [SerializeField] private GameObject magicMissile;
    [SerializeField] private GameObject flameBlast;

    [SerializeField] private Transform missileOrigin1;
    [SerializeField] private Transform missileOrigin2;
    [SerializeField] private Transform missileOrigin3;
    [SerializeField] private Transform flameOrigin;

    [SerializeField] private HigorShield shield;

    public UnityEvent OnSummon75 = new UnityEvent();
    public UnityEvent OnSummon50 = new UnityEvent();
    public UnityEvent OnSummon25 = new UnityEvent();

    private bool SummonOrder1=false;
    private bool SummonOrder2=false;
    private bool SummonOrder3=false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        higorCollider = GetComponent<CapsuleCollider2D>();
        higorCollider.enabled = false;
        missileTimerRecord = missileTimer;
        flameTimerRecord = flameTimer;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        FlameCooldown();

        if (Input.GetKeyDown(KeyCode.U)) 
        {
            StartBoss();
        }

        if(shield.ShieldThreshold1 && !SummonOrder1)
        {
            shouldSummon = true;
            SummonOrder1 = true;
        }
        if (shield.ShieldThreshold2 && !SummonOrder2)
        {
            shouldSummon = true;
            SummonOrder2 = true;
        }
        if (shield.ShieldThreshold3 && !SummonOrder3)
        {
            shouldSummon = true;
            SummonOrder3 = true;
        }
    }
    public void StartBoss()
    {
        lineOfSight = 75f;
        flameRange = 13f;
        ActivateFlameCooldown();
    }

    public void FacePlayer()
    {
        Vector3 direction = player.position - animator.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rigidBody.rotation = angle;
        direction.Normalize();
    }

    public void LaunchMissileAttack()
    {
        StartCoroutine(MissileAttack());
    }

    IEnumerator MissileAttack()
    {
        LaunchMissile();
        yield return new WaitForSecondsRealtime(0.2f);
        LaunchMissile();
        yield return new WaitForSecondsRealtime(0.2f);
        LaunchMissile();
    }

    private void LaunchMissile()
    {
        audioManager.PlaySFX(audioManager.mageMissile);
        GameObject missile = Instantiate(magicMissile);
        missile.transform.position = missileOrigin1.position;
        missile.transform.localRotation = missileOrigin1.rotation;

        GameObject missile2 = Instantiate(magicMissile);
        missile2.transform.position = missileOrigin2.position;
        missile2.transform.localRotation = missileOrigin2.rotation;

        GameObject missile3 = Instantiate(magicMissile);
        missile3.transform.position = missileOrigin3.position;
        missile3.transform.localRotation = missileOrigin3.rotation;
    }

    public void LaunchFlameblast()
    {
        audioManager.PlaySFX(audioManager.mageFlame);
        GameObject blast = Instantiate(flameBlast);
        blast.transform.position=flameOrigin.position;
        blast.transform.localRotation=flameOrigin.rotation;
    }

    public void Teleport()
    {
        int teleportLocation = Random.Range(0, teleportRunes.Length);
        Debug.Log("location chosen: " + teleportLocation);
        if (teleportLocation == lastTeleportLocation)
        {
            if (teleportLocation == 4)
            {
                teleportLocation=0;
            }else
            {
                teleportLocation++;
            }
        }
        lastTeleportLocation = teleportLocation;
        audioManager.PlaySFX(audioManager.mageTeleport);
        transform.position = teleportRunes[teleportLocation].position;
    }

    public void SummonCrows()
    {
        audioManager.PlaySFX(audioManager.crowHit);
        if (shield.ShieldThreshold3)
        {
            OnSummon25?.Invoke();
            shouldSummon = false;
            return;
        }
        else if(shield.ShieldThreshold2)
        {
            OnSummon50?.Invoke();
            shouldSummon = false;
            return;
        } 
        else if(shield.ShieldThreshold1)
        {
            OnSummon75?.Invoke();
            shouldSummon = false;
        }
    }

    public void SecondPhase()
    {
        higorCollider.enabled = true;
    }

    public void ActivateFlameCooldown()
    {
        flameTimer = flameTimerRecord;
        flameCooldown = true;
    }

    private void FlameCooldown()
    {
        if (flameCooldown == true)
        {
            flameTimer -= Time.deltaTime;
            if (flameTimer <= 0)
            {
                flameCooldown = false;
                flameTimer = flameTimerRecord;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, flameRange);
    }
}

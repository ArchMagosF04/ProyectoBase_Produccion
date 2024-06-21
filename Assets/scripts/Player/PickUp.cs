using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform holdSpot;
    public Transform holdTarget;
    public LayerMask pickUpMask;
    public float pickUpRange=1.4f;

    public Vector3 Direction { get; set; }
    private GameObject itemHolding;

    private bool isHoldingItem;
    public bool IsHoldingItem=>isHoldingItem;

    private Animator animator;

    private void Start()
    {
        animator= GetComponent<Animator>();
    }


    void Update()
    {
        if (animator.GetBool("IsIdle"))
        {
            SetDirection();

            if (Input.GetKeyDown(KeyCode.E) && !ScreenManager.Instance.isDialogueActive)
            {
                if (itemHolding)
                {
                    itemHolding.transform.position = holdTarget.position;
                    itemHolding.transform.parent = null;
                    if (itemHolding.GetComponent<Rigidbody2D>())
                        itemHolding.GetComponent<Rigidbody2D>().simulated = true;
                    itemHolding = null;
                    isHoldingItem = false;
                    //playerCombo.ChangeState(PlayerState.idle);
                }
                else
                {
                    Physics2D.queriesStartInColliders = false;

                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, pickUpRange, pickUpMask);

                    Collider2D pickUpItem = hit.collider;

                    if (pickUpItem)
                    {
                        itemHolding = pickUpItem.gameObject;
                        itemHolding.transform.position = holdSpot.position;
                        itemHolding.transform.parent = transform;
                        itemHolding.transform.rotation = transform.rotation;
                        if (itemHolding.GetComponent<Rigidbody2D>())
                            itemHolding.GetComponent<Rigidbody2D>().simulated = false;
                        isHoldingItem = true;
                        //playerCombo.ChangeState(PlayerState.grab);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemHolding && !ScreenManager.Instance.isDialogueActive)
                {
                    ThrowItem(itemHolding);
                    itemHolding = null;
                }
            }
        }

        Debug.DrawRay(transform.position, Direction * pickUpRange, Color.cyan);
    }

    private void ThrowItem(GameObject item)
    {
        item.transform.parent = null;

        if (item.GetComponent<Rigidbody2D>())
            item.GetComponent<Rigidbody2D>().simulated = true;
        item.GetComponent<Rigidbody2D>().AddForce(Direction * 4000);
        item.gameObject.GetComponent<ThrownObject>().Toss();
        isHoldingItem = false;
        //playerCombo.ChangeState(PlayerState.idle);
    }

    //IEnumerator ThrowIem(GameObject item)
    //{
    //    Vector3 startpoint=item.transform.position;
    //    Vector3 endPoint=transform.position+Direction*2;
    //    item.transform.parent=null;
    //    for(int i = 0; i < 25; i++)
    //    {
    //        item.transform.position=Vector3.Lerp(startpoint,endPoint,i*.04f);
    //    }
    //    if (item.GetComponent<Rigidbody2D>())
    //        item.GetComponent<Rigidbody2D>().simulated=true;
    //    Destroy(item);
    //    yield return null;
    //}

    public void SetDirection()
    {
        Direction = (holdTarget.position - transform.position).normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendAttack : MonoBehaviour, iAction
{
    [SerializeField, Range(0.1f, 3.0f)]
    float range = 2.0f;

    BlendMove move = null;
    ActionManager actionManager = null;
    Transform target = null;
    Animator animator = null;
    Damage damage = null;

    void Awake()
    {
        move = GetComponent<BlendMove>();
        actionManager = GetComponent<ActionManager>();

        animator = GetComponent<Animator>();
    }

    public void Begin(object obj)
    {
        Damage attackTarget = obj as Damage;
        if(attackTarget == null)
        {
            Debug.LogError("Damage형 필요");
            return;
        }

        actionManager.StartAction(this);
        target = attackTarget.transform;
    }

    public void End()
    {
        target = null;
    }

    void Update()
    {
        if (target == null) return;

        if(IsInRange() == true && 
            animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Base Layer.attacking"))
        {
            move.End();
            animator.SetTrigger("isAttack");
            transform.LookAt(target);
            damage = target.GetComponent<Damage>();
            this.End();
        }
        else
        {
            move.MoveTo(target.position);
        }

    }

    bool IsInRange()
    {
        Vector2 other = new Vector2(target.position.x, target.position.z);

        Vector2 my = new Vector2(transform.position.x, transform.position.z);

        return Vector2.Distance(other, my) < range;
    }

    public void Hit()
    {
        Debug.Log("Hit");
        if(damage != null)
        {
            damage.Damaged();
            damage = null;
        }
    }
}

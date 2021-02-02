using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [ Has Exit Time ]
 * 체크 시 해당 애니메이션이 끝날 때 까지
 * 다른 애니메이션으로 전환을 막는다.
*/


[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(CharacterController))]
public class MechanimControl : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public float RotSpeed = 360.0f;
    public UIManager ManagerObject = null;

    CharacterController pcController;
    Animator animator;
    Vector3 dir;
    int HP = 3;
    bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ManagerObject.OnHit(HP);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", pcController.velocity.magnitude);
        CharacterControl_Slerp();

        if(Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetTrigger("PressSpace");
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetTrigger("PressF");
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            animator.SetTrigger("PressR");
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("IsPressCtrl", true);
        }
        else
        {
            animator.SetBool("IsPressCtrl", false);
        }
    }

    void CharacterControl_Slerp()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (dir.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp(transform.forward, dir,
                RotSpeed * Time.deltaTime / Vector3.Angle(transform.forward, dir));

            transform.LookAt(transform.position + forward);
        }

        pcController.Move(dir * MoveSpeed * Time.deltaTime + Physics.gravity);
    }

    void CheckAnimationIsPlaying()
    {
        // 해당 Layer[index]의 Animation이 있는 지
        if(animator.GetCurrentAnimatorStateInfo(1).IsName("UpperBodyMask.Run"))
        {
            // 1.0f 보다 크거나 같으면 애니메이션 재생 완료
            if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1.0f)
            {

            }
            else  // 애니메이션 재생 중
            {

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            int currentScore = GlobalGameManager.Instance.GetScore() + 100;
            GlobalGameManager.Instance.SetScore(currentScore);
            ManagerObject.OnGetScore(currentScore);

            other.gameObject.GetComponent<Coin>().OnCollecterPickUp();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isInvincible) return;

        if(hit.transform.tag == "Enemy0")
        {
            StartCoroutine(OnHit(3.0f));

            HP--;

            if(HP <= 0)
            {
                HP = 0;
                ManagerObject.OnGameOver();
            }

            ManagerObject.OnHit(HP);
        }
    }

    IEnumerator OnHit(float invincibleTime)
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
}

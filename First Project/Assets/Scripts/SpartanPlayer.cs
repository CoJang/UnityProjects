using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartanPlayer : MonoBehaviour
{
    Animation spartanKing;
    float animBlendingTime = 0.3f;
    CharacterController pcCtrl;
    public float RunSpeed = 12.0f;
    public float RotSpeed = 360;

    Vector3 velocity;

    public GameObject objSword = null;

    void Start()
    {
        spartanKing = GetComponentInChildren<Animation>();
        spartanKing.wrapMode = WrapMode.Loop;
        pcCtrl = GetComponent<CharacterController>();
        objSword.SetActive(false);
    }

    void Update()
    {
        CharacterControl_Slerp();
    }

    void PlayAnimation(string animName)
    {
        spartanKing.wrapMode = WrapMode.Loop;
        //spartanKing.Play(animName);           // None Blending
        spartanKing.CrossFade(animName, animBlendingTime);  // Animation Blending
    }

    void PlayAnimation(string animName, bool isLoop)
    {
        if(isLoop)
        {
            spartanKing.wrapMode = WrapMode.Loop;
        }
        else
        {
            spartanKing.wrapMode = WrapMode.Once;
        }

        spartanKing.CrossFade(animName, 0.3f);
    }

    void PlayAnimation(string animName, string idleAnimName)
    {
        spartanKing.wrapMode = WrapMode.Once;
        spartanKing.CrossFade(animName, 0.3f);

        StartCoroutine(BackToIdleAnim(idleAnimName, idleAnimName));
    }

    IEnumerator BackToIdleAnim(string animName, string idleAnimName)
    {
        float dTime = spartanKing.GetClip(animName).length - animBlendingTime;

        yield return new WaitForSeconds(dTime);

        PlayAnimation(idleAnimName);
        objSword.SetActive(false);
    }

    void InputAnimations()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!spartanKing.IsPlaying("attack"))
            {
                objSword.SetActive(true);
                PlayAnimation("attack", "idlebattle");
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            PlayAnimation("idle");
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            PlayAnimation("walk");
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            PlayAnimation("run");
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            PlayAnimation("charge");
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            PlayAnimation("idlebattle");
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            PlayAnimation("resist", "idlebattle");
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            PlayAnimation("victory", "idle");
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            PlayAnimation("salute", "idle");
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            PlayAnimation("die", false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            PlayAnimation("diehard", false);
        }
    }

    void CharacterControl()
    {
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        velocity *= RunSpeed;

        if(velocity.magnitude > 0.5f)
        {
            PlayAnimation("run");
            transform.LookAt(transform.position + velocity);
        }
        else if(velocity.magnitude == 0)
        {
            PlayAnimation("idle");
        }

        // 캐릭터 컨트롤러는 물리 적용 x
        // 따라서 따로 중력값을 넣어주지 않으면 공중에 뜬 상태로 움직임 처리가 된다.
        // 하지만 SimpleMove는 중력도 같이 처리됨.
        pcCtrl.Move(velocity * Time.deltaTime);
    }

    void CharacterControl_Slerp()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if(dir.sqrMagnitude > 0.01f)
        {
            if(spartanKing.IsPlaying("idle") || spartanKing.IsPlaying("idlebattle"))
                PlayAnimation("run");

            Vector3 forward = Vector3.Slerp(transform.forward, dir, 
                RotSpeed * Time.deltaTime / Vector3.Angle(transform.forward, dir));

            transform.LookAt(transform.position + forward);
        }
        else if(spartanKing.IsPlaying("run"))
        {
            PlayAnimation("idle");
        }

        InputAnimations();

        if (!spartanKing.IsPlaying("attack"))
            pcCtrl.Move(dir * RunSpeed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }
}

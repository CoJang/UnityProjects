using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable, IPunObservable
{
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    [SerializeField] Item[] items;
    int itemIndex;
    int preItemIndex = -1;

    bool isGrounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    const float maxHealth = 100.0f;
    float currentHealth = maxHealth;

    [HideInInspector] public Camera playerCamera;
    Rigidbody rb;
    PhotonView PV;
    PlayerManager playerManager;
    Animator anim;
    Transform spine;

    float MaxYAxis = 1.5f;
    Vector3 relativeVec = new Vector3(0, -55, -100);

    Vector2 AnimControlVelocity = Vector2.zero;
    float DecreaseFactor = 0.1f;
    float MaxAnimVelocity = 1.0f;

    Vector3 realPos = Vector3.zero;
    Vector3 realRot = Vector3.zero;
    //Quaternion realRot = Quaternion.identity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        spine = anim.GetBoneTransform(HumanBodyBones.Spine);

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            EquipItem(0);
        }
        else
        {
            //Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        
        Move();
        Jump();
        SwapWeapon();

        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
        }
        if(Input.GetKeyDown(KeyCode.R) && !anim.GetBool("IsReloading"))
        {
            anim.SetBool("IsReloading", true);
        }
        else
        {
            anim.SetBool("IsReloading", false);
        }

        if(transform.position.y < - 10f)
        {
            Die();
        }
    }

    private void LateUpdate()
    {
        Look();

        if(!PV.IsMine)
        {
            spine.position = Vector3.Lerp(spine.position, realPos, 0.1f);
            spine.eulerAngles = Vector3.Lerp(spine.eulerAngles, realRot, 0.1f);
            Debug.Log("spine.localRotation :" + spine.eulerAngles);
            Debug.Log("spine.realRot :" + realRot);
        }
    }

    void SwapWeapon()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (itemIndex <= items.Length - 1)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed),
            ref smoothMoveVelocity, smoothTime);

        AnimControlVelocity.x += moveAmount.x * Time.deltaTime * 2;
        AnimControlVelocity.y += moveAmount.z * Time.deltaTime;

        AnimControlVelocity.x = Mathf.Clamp(AnimControlVelocity.x, -MaxAnimVelocity, MaxAnimVelocity);
        AnimControlVelocity.y = Mathf.Clamp(AnimControlVelocity.y, -MaxAnimVelocity, MaxAnimVelocity);

        if(moveDir.x == 0 && AnimControlVelocity.x > 0)
        {
             AnimControlVelocity.x -= DecreaseFactor;

            if (AnimControlVelocity.x < 0)
                AnimControlVelocity.x = 0;
        }
        else if (moveDir.x == 0 && AnimControlVelocity.x < 0)
        {
            AnimControlVelocity.x += DecreaseFactor;

            if (AnimControlVelocity.x > 0)
                AnimControlVelocity.x = 0;
        }

        if (moveDir.z == 0 && AnimControlVelocity.y > 0)
        {
            AnimControlVelocity.y -= DecreaseFactor;

            if (AnimControlVelocity.y < 0)
                AnimControlVelocity.y = 0;
        }
        else if (moveDir.z == 0 && AnimControlVelocity.y < 0)
        {
            AnimControlVelocity.y += DecreaseFactor;

            if (AnimControlVelocity.y > 0)
                AnimControlVelocity.y = 0;
        }

        anim.SetFloat("Horizontal", AnimControlVelocity.x);
        anim.SetFloat("Vertical", AnimControlVelocity.y);
    }

    void Look()
    {
        if (!PV.IsMine) return;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        ray.origin = playerCamera.transform.position;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(PV.IsMine && hit.collider.tag == "Player") 
                return;

            Vector3 target = hit.point;
            target.y = Mathf.Clamp(target.y, 0, MaxYAxis);
            spine.LookAt(target);
            transform.LookAt(new Vector3(target.x, 0, target.z));

            Quaternion spineRot = spine.rotation * Quaternion.Euler(relativeVec);
            spine.rotation = spineRot;
            //float RotY = spine.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;

            //if (Mathf.Abs(RotY) >= MaxRotation)
            //{
            //    transform.Rotate(new Vector3(0, RotY, 0), Space.World);
            //    spine.Rotate(new Vector3(0, -RotY, 0), Space.World);
            //}

        }

    }

    public void SetGroundedState(bool grounded)
    {
        isGrounded = grounded;
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    void EquipItem(int _index)
    {
        if (_index == preItemIndex)
            return;

        anim.SetTrigger("Swap");

        itemIndex = _index;

        items[itemIndex].itemGameObject.SetActive(false);
        anim.SetLayerWeight(itemIndex, 1);
        
        if(preItemIndex != -1)
        {
            items[preItemIndex].itemGameObject.SetActive(false);
            anim.SetLayerWeight(preItemIndex, 0);
        }

        preItemIndex = itemIndex;

        if(PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(!PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }

    // 총을 쏜 사람의 컴퓨터에서 실행된다.
    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    // 모두의 컴퓨터에서 실행된다.
    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if (!PV.IsMine)
            return;

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        playerManager.Die();
    }

    public void BindPlayerCamera(Camera camera)
    {
        playerCamera = camera;
    }

    public void OnSwapFinish()
    {
        items[itemIndex].itemGameObject.SetActive(true);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(spine.position);
            stream.SendNext(spine.eulerAngles);
        }
        else
        {
            realPos = (Vector3)stream.ReceiveNext();
            //realRot = (Quaternion)stream.ReceiveNext();
            realRot = (Vector3)stream.ReceiveNext();
        }
    }
}

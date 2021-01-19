using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(AudioSource))]
public class Player2D : MonoBehaviour
{
    public float MoveSpeed = 0.1f;
    private Rigidbody2D rig;
    private SpriteRenderer spriteRenderer;

    public float PlayerHP = 100;
    public int PlayerScore = 0;
    private bool isDead = false;

    public GameObject Bullet = null;
    public UIManager UIManagerScript;

    [SerializeField]
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if(!isDead)
            CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(-1, 0);
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(1, 0);
            spriteRenderer.flipX = false;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            Shot();
            PlaySoundEffect(audioClips[0]);
        }
    }

    void Move(float x, float y)
    {
        Vector3 position = rig.transform.position;
        position = new Vector3(position.x + (x * MoveSpeed),
                                position.y + (y * MoveSpeed),
                                position.z);

        rig.MovePosition(position);
    }

    void Shot()
    {
        if(Bullet != null)
        {
            GameObject obj;
            obj = Instantiate(Bullet, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Bullet is Null!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        Debug.Log("<color=red>충돌</color>");

        if(collision.tag == "Enemy0")
        {
            OnPlayerDamaged(20.0f);
            OnPlayerGetScore(50);
        }

        if (collision.tag == "Coin")
        {
            OnPlayerGetScore(100);
            Destroy(collision.gameObject);
        }
    }

    public void OnPlayerDamaged(float damage)
    {
        PlayerHP -= damage;
        PlaySoundEffect(audioClips[1]);
        StartCoroutine(OnPlayerHit());

        if (PlayerHP <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log("Player Dead!");
            UIManagerScript.OnGameOver();
        }
    }

    public void OnPlayerGetScore(int score)
    {
        if(UIManagerScript)
        {
            PlayerScore += score;
            UIManagerScript.OnGetScore(PlayerScore);
            PlaySoundEffect(audioClips[2]);
        }
    }

    void PlaySoundEffect(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = clip;
        audioSource.Play();
    }

    IEnumerator OnPlayerHit()
    {
        for(int i = 0; i < 2; i++)
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.33f);
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.33f);
            spriteRenderer.color = Color.white;
        }
        yield return null;
    }
}

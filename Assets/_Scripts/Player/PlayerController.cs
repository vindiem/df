using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Animator animator;

    // fields
    [Header("Fields")]
    public int _health = 100;
    public float _speed;
    float _jumpForce = 10;
    [HideInInspector] public float _moveInput;
    private Pause _pause;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public bool faceRight = true;

    [Header("Jump Variables")]
    // jump variables
    public ParticleSystem jumpEffect;
    public LayerMask ground;
    public Transform feetPos;
    float chekRadius = .5f;
    [HideInInspector] public bool isGrounded;

    int jumpBuffer = 1;

    [Header("Hp Instantiate")]
    // hp show()
    public Image healthBar;
    float maxHp = 100f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _pause = GetComponentInChildren<Pause>().GetComponent<Pause>();
    }

    private void FixedUpdate()
    {
        // movement
        _moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(_moveInput * _speed, rb.velocity.y);

        #region Flip
        // flip body
        FlipExamination();
        #endregion
    }

    private void Update()
    {
        // hp instantiate
        healthBar.fillAmount = _health / maxHp;

        // die when your position under -10f or your _health <= 0
        if (_health <= 0 || transform.position.y <= -15f)
        {
            Destroy(gameObject);
            SceneTransition.SwitchToScene("Menu");
        }
        
        // menu on/off
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pause.MenuPause();
        }

        #region jump
        // jump
        isGrounded = Physics2D.OverlapCircle(feetPos.position, chekRadius, ground);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("jump", true);
            rb.velocity = Vector2.up * _jumpForce;
            StartCoroutine(WaitAnimTime(0.8f)); // Wait Anim Time() - wait some seconds and animation will be stop work
            // effect? Instantiate
            Instantiate(jumpEffect, transform.position, Quaternion.identity);
            // for double jump
            jumpBuffer--;
        }

        // double jump realisation
        else if (isGrounded == false && jumpBuffer >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // effect? Instantiate
                Instantiate(jumpEffect, transform.position, Quaternion.identity);

                rb.velocity = Vector2.up * _jumpForce;
                jumpBuffer--;
            }
        }

        else if (isGrounded == true)
        {
            jumpBuffer = 1;
        }

        #endregion

        #region animations
        // animations
        if (_moveInput == 0)
        {
            animator.SetBool("isRunning", false);
        }

        else if (_moveInput > 0 || _moveInput < 0)
        {
            animator.SetBool("isRunning", true);
        }
        #endregion
    }

    public void Flip()
    {
        faceRight = !faceRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    // Skill
    public void ReSize(bool isUse)
    {
        if (isUse == true)
        {
            Vector3 Scale = transform.localScale;
            Scale /= 2;
            transform.localScale = Scale;
        }
        else if (isUse == false)
        {
            Vector3 Scale = transform.localScale;
            Scale *= 2;
            transform.localScale = Scale;
        }
        
    }

    public void FlipExamination()
    {
        if ((faceRight == false && _moveInput > 0))
        {
            Flip();
        }
        else if ((faceRight == true && _moveInput < 0))
        {
            Flip();
        }
    }

    public void ChangeHealth(int changeValue)
    {
        _health += changeValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("hPotion"))
        {
            ChangeHealth(10);
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("sPotion"))
        {
            _speed *= 1.5f;
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("jPotion"))
        {
            _jumpForce *= 1.4f;
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Portal"))
        {
            UnLockLevel();
            SceneTransition.SwitchToScene("Menu");
        }

        else if (collision.CompareTag("damageTrigger"))
        {
            ChangeHealth(-13);
            _jumpForce += 3;

        }

        else if (collision.CompareTag("spike"))
        {
            ChangeHealth(-17);
            _jumpForce -= 5;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("damageTrigger"))
            {
                _jumpForce -= 3;

            }

        else if (collision.CompareTag("spike"))
            {
                _jumpForce += 5;

            }
    }

    IEnumerator WaitAnimTime(float sec)
    {
        yield return new WaitForSeconds(sec);
        animator.SetBool("jump", false);
    }

    public void UnLockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levels"))
        {
            PlayerPrefs.SetInt("levels", currentLevel + 1);
        }
    }
    
}

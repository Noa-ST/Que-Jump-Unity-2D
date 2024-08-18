using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 jumpForce; //Lực nhảy hiện tại của người chơi.
    public Vector2 jumpForceUp; //Tốc độ tăng lực nhảy khi giữ chuột.
    public float minForceX;
    public float maxForceX;
    public float minForceY;
    public float maxForceY;

    [HideInInspector]
    public int lastPlatformId;

    bool m_didJump;
    bool m_powerSetted;

    Rigidbody2D rb;
    Animator amin;
    float m_curPowerBarVal = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        amin = GetComponent<Animator>();
    }

    private void Update()
    {
        if(GameManager.Ins.IsGameStarted)
        {
            SetPower();

            if (Input.GetMouseButtonDown(0))
            {
                SetPower(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                SetPower(false);
            }
        }
    }

    void SetPower()
    {
        if (m_powerSetted && !m_didJump)
        {
            //jumpForce.x và jumpForce.y được tăng dần theo giá trị jumpForceUp.x và jumpForceUp.y mỗi khung hình (frame).
            jumpForce.x += jumpForceUp.x * Time.deltaTime;
            jumpForce.y += jumpForceUp.y * Time.deltaTime;

            //Mathf.Clamp được sử dụng để giới hạn giá trị của jumpForce.x và jumpForce.y trong khoảng từ minForceX đến maxForceX và từ minForceY đến maxForceY.
            jumpForce.x = Mathf.Clamp(jumpForce.x, minForceX, maxForceX);
            jumpForce.y = Mathf.Clamp(jumpForce.y, minForceY, maxForceY);

            m_curPowerBarVal += GameManager.Ins.powerBarup * Time.deltaTime;

            GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);
        }
    }

    public void SetPower(bool isHoldingMouse)
    {
        m_powerSetted = isHoldingMouse;

        if (!m_powerSetted && !m_didJump)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!rb || jumpForce.x <= 0 || jumpForce.y <= 0) return;

        rb.velocity = jumpForce;
        m_didJump = true;

        if (amin)
            amin.SetBool("didJump", true);

        AudioController.Ins.PlaySound(AudioController.Ins.jump);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tagconsts.GROUND))
        {
            Platform p = collision.transform.root.GetComponent<Platform>();

            if (m_didJump)
            {
                m_didJump = false;

                if (amin)
                    amin.SetBool("didJump", false);

                if (rb)
                    rb.velocity = Vector2.zero;

                jumpForce = Vector2.zero;

                m_curPowerBarVal = 0;

                GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);
            }

            if (p && p.id != lastPlatformId)
            {
                Debug.Log("Jumped to new platform: " + p.id); // Thêm thông báo gỡ lỗi
                GameManager.Ins.CreatePlatformAndLerp(transform.position.x);
                lastPlatformId = p.id;
                GameManager.Ins.AddScore();
            }
        }

        if (collision.CompareTag(Tagconsts.DEAD_ZONE))
        {
            GameGUIManager.Ins.ShowGameovertDialog();

            AudioController.Ins.PlaySound(AudioController.Ins.gameover);

            Destroy(gameObject);
        }
    }
}

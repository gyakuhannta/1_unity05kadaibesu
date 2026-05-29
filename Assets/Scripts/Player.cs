using UnityEngine;
using UnityEngine.InputSystem;
using R3;               // R3 core
using R3.Triggers;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] int jumpcnt=2;

    public float MaxLife => 100f;
    public ReactiveProperty<float> life { get; private set; } = new();

    PlayerInput playerInput;
    Rigidbody2D rb;
    private int jumpct;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        life.Value = MaxLife;
        jumpct = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Floor") 
        {
            jumpct = 0;


        }
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        if (move.x != 0f)
        {
            rb.linearVelocityX = move.x * speed;

            // 向き
            var localScale = transform.localScale;
            if (move.x < 0)
            {
                localScale.x = 1f;
            }
            else
            {
                localScale.x = -1f;
            }
            transform.localScale = localScale;
        }

        if (jumpcnt > jumpct)
        {
            // ジャンプ
            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                rb.linearVelocityY = jumpSpeed;
                jumpct++;

            }
        }
    }
}

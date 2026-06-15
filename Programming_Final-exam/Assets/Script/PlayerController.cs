using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float bounceForce = 12f;
    public float dashForwardForce = 15f;

    private Rigidbody rb;
    private Renderer playerRenderer;
    private Color originalColor;
    private TrailRenderer trailRenderer;

    public bool hasJumpItem = false;
    private bool isDashing = false; // 대쉬 중인지 확인하는 변수 추가

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;

        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null) trailRenderer.emitting = false;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        // [핵심] 대쉬 중일 때 반대 키(A키 = -1)를 누르면 즉시 대쉬 해제
        if (isDashing && h < 0)
        {
            CancelDash();
        }

        Vector3 moveDir = new Vector3(0f, 0f, h).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
            transform.forward = moveDir;
        }

        if (hasJumpItem && Input.GetButtonDown("Jump"))
        {
            UseJumpItem();
        }
    }

    private void CancelDash()
    {
        isDashing = false;
        if (trailRenderer != null) trailRenderer.emitting = false;
        // 필요하다면 여기서 물리 속도를 즉시 줄이는 코드를 넣을 수도 있습니다.
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f, rb.linearVelocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Platform"))
        {
            isDashing = false; // 땅에 닿으면 대쉬 종료
            if (trailRenderer != null) trailRenderer.emitting = false;

            if (rb.linearVelocity.y <= 0.1f)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, bounceForce, rb.linearVelocity.z);
            }
        }
    }

    public void PickUpItem()
    {
        if (!hasJumpItem)
        {
            hasJumpItem = true;
            playerRenderer.material.color = originalColor * 0.5f;
        }
    }

    private void UseJumpItem()
    {
        hasJumpItem = false;
        isDashing = true; // 대쉬 시작

        float halfBounce = bounceForce * 0.5f;
        Vector3 dashVelocity = transform.forward * dashForwardForce;
        dashVelocity.y = halfBounce;

        rb.linearVelocity = dashVelocity;
        playerRenderer.material.color = originalColor;

        if (trailRenderer != null) trailRenderer.emitting = true;
    }
}
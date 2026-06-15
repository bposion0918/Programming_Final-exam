using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(3f, 0f, 0f); // 이동할 범위 (X축으로 3만큼 왕복)
    public float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Mathf.PingPong을 이용한 자연스러운 왕복 운동
        float factor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPos, startPos + moveOffset, factor);
    }

    // 플레이어가 발판 위에 올라탔을 때 미끄러지지 않게 처리
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
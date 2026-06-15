using UnityEngine;

public class Score : MonoBehaviour
{
    public int scoreValue = 10;

    void Update()
    {
        // 아이템이 제자리에서 회전하는 연출
        transform.Rotate(Vector3.up * 50f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManager에 점수 추가 요청 (4단계에서 구현)
            GameManager.instance.AddScore(scoreValue);
            Destroy(gameObject); // 먹었으니 삭제
        }
    }
}
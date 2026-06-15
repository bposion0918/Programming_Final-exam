using UnityEngine;

public class JumpItem : MonoBehaviour
{
    public float rotationSpeed = 90f; // 아이템이 빙글빙글 도는 속도

    void Update()
    {
        // 아이템이 제자리에서 회전하는 시각적 연출 (선택 사항)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    // 플레이어가 아이템에 닿았을 때 감지
    private void OnTriggerEnter(Collider other)
    {
        // 부딪힌 대상이 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            // 플레이어의 PlayerController 스크립트를 가져옵니다.
            PlayerController player = other.GetComponent<PlayerController>();

            // 플레이어가 있고, 현재 아이템을 가지고 있지 않을 때만 획득
            if (player != null && !player.hasJumpItem)
            {
                player.PickUpItem(); // 플레이어에게 아이템 획득 처리 지시
                Destroy(gameObject); // 화면에서 아이템 삭제
            }
        }
    }
}
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    // [수정 포인트] 가운데 있는 Y값(높이)을 7f에서 15f나 20f 등으로 크게 올려주세요.
    // 필요하다면 Z값(거리)도 -15f 등으로 더 멀게 빼주시면 시야가 훨씬 넓어집니다!
    public Vector3 offset = new Vector3(0f, 15f, -12f);

    void LateUpdate()
    {
        if (target == null) return;

        // 플레이어의 X, Z 위치만 가져옴 (플레이어가 점프해도 카메라 높이는 안 변함)
        Vector3 targetPositionFlat = new Vector3(target.position.x, 0f, target.position.z);

        // offset의 Y값이 카메라의 고정 높이가 됩니다.
        transform.position = targetPositionFlat + offset;

        transform.LookAt(target);
    }

}
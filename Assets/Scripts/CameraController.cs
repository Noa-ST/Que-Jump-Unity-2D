using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float lerpTime; //Thời gian để camera di chuyển từ vị trí hiện tại đến vị trí đích
    public float xOffset; //Khoảng cách nhỏ để camera dừng lại gần vị trí đích
    bool m_canLerp; // camera có đang trong trạng thái di chuyển ko
    float m_lerpXDist; //Vị trí x mà camera đang di chuyển đến

    private void Update()
    {
        if (m_canLerp)
        {
            MoveLerp();
        }
    }

    void MoveLerp()
    {
        float xPos = transform.position.x;

        //Mathf.Lerp(a, b, t) là hàm nội suy tuyến tính, dùng để tính toán giá trị giữa hai điểm a và b, với t là hệ số nội suy từ 0 đến 1.
        //Trong trường hợp này, a là xPos (vị trí hiện tại của camera), và b là m_lerpXDist (vị trí đích mà camera cần di chuyển đến).
        xPos = Mathf.Lerp(xPos, m_lerpXDist, lerpTime * Time.deltaTime);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        if (transform.position.x >= (m_lerpXDist - xOffset))
        {
            m_canLerp = false;
        }
    }

    public void LerpTrigger(float dist)
    {
        m_canLerp = true;
        m_lerpXDist = dist;
    }
}


// Tại sao cần xOffset?
//Tránh việc camera bị kẹt tại điểm đích: Nếu không sử dụng xOffset, camera có thể dừng lại chính xác tại điểm đích, dẫn đến tình trạng "kẹt" nếu không tính toán chính xác. xOffset tạo ra một vùng xung quanh điểm đích để đảm bảo camera dừng lại một cách mượt mà và tự nhiên.
//Tạo cảm giác chuyển động mượt mà: Việc dừng lại một chút trước khi đạt điểm đích giúp di chuyển camera trở nên tự nhiên hơn, tránh sự dừng đột ngột.
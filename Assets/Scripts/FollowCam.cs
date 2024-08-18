using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Ins.mainCam)
        {
            transform.position = new Vector3(
                GameManager.Ins.mainCam.transform.position.x, 
                GameManager.Ins.mainCam.transform.position.y,
                0f
                );

        }
    }
}

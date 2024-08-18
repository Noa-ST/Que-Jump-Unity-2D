using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfLimit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tagconsts.PLATFORM))
            {
            Destroy(collision.gameObject);
        }
    }

}

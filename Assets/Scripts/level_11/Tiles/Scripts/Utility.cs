using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Utility : MonoBehaviour
{
    public void GetUtil()
    {
        PlayerController.instance.util_Counter++;
        Destroy(gameObject);
    }
}

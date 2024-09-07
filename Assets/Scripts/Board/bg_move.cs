using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_move : MonoBehaviour
{
    public float sebesseg;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(this.transform.position.x - sebesseg, this.transform.position.y);
    }

}

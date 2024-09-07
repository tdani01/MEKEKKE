using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectorset : MonoBehaviour
{
    public GameObject detector;
    // Start is called before the first frame update
    void Start()
    {
        detector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            detector.SetActive(true);
        }
        else
        {
            detector.SetActive(false);
        }
    }
}

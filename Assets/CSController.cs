using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CSController : MonoBehaviour
{
    public GameObject cutscene;
    //public GameObject map;
    public VideoClip vp;
    double vpTimer;
    bool vpactive = false;

    int nextscene;
    bool scanning = false;

    // Update is called once per frame
    void Update()
    {
        if (scanning)
        {
            if (vpTimer >= vp.length + 2d && vpactive)
            {
                SceneManager.LoadScene(nextscene);
            }
            else
            {
                vpTimer += Time.deltaTime;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.name == "goDown")
        {
            cutscene.SetActive(true);
            nextscene = 8;
            scanning = true;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name == "goUp")
        {
            cutscene.SetActive(true);
            nextscene = 7;
            scanning = true;
            Destroy(collision.gameObject);
        }
    }

}

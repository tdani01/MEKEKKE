using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Watcher : MonoBehaviour
{
    public class ig
    {
        public ig(int a)
        {
            s = a;
            g = null;
        }
        public int s;
        public GameObject g;
    }

    public GameObject[] target;
    public GameObject[] now;
    private List<ig> sig = new List<ig>();

    public GameObject Victory;

    public UnityEvent[] unselects;

    // Start is called before the first frame update

    void Start()
    {
        sig.Add(new ig(-1));
        sig.Add(new ig(-1));
    }

    // Update is called once per frame
    void Update()
    {
        if (sig[0].s != -1 && sig[1].s != -1)
        {
            Debug.Log("Kiválasztva 2 object!");
            Change(sig[0], sig[1]);
            
        }
    }

    public void Select0(int a)
    {
        
        if (sig[0].s == -1)
        {
            sig[0].s = a;
        }
        else
        {
            sig[1].s = a;
        }
    }
    public void Select1(GameObject b)
    {
        if (sig[0].g == null)
        {
            sig[0].g = b;
        }
        else
        {
            sig[1].g = b;
        }
    }

    public void Unselect(int a)
    {
        if (sig[0].s == a)
        {
            sig[0] = new ig(-1);
        }
        else
        {
            sig[1] = new ig(-1);
        }
    }

    public void Change(ig a, ig b)
    {
        GameObject go1 = target[a.s];
        GameObject go2 = target[b.s];
        int Counter = 0;
        int now1 = default;
        int now2 = default;
        foreach (GameObject item in now)
        {
            if(item == go1)
            {
                go1 = item;
                now1 = Counter;
            }else if(item == go2)
            {
                go2 = item;
                now2 = Counter;
            }
            Counter++;
        }

        Vector3 help1 = new Vector3(go1.transform.position.x, go1.transform.position.y, go1.transform.position.z);
        Vector3 help2 = new Vector3(go2.transform.position.x, go2.transform.position.y, go2.transform.position.z);
        go1.transform.position = help2;
        go2.transform.position = help1;

        now[now1] = go2;
        now[now2] = go1;

        this.sig[0] = new ig(-1);
        this.sig[1] = new ig(-1);

        unselects[a.s].Invoke();
        unselects[b.s].Invoke();

        Check();
    }

    private void Check()
    {
        bool equals = false;
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i] == now[i])
            {
                equals = true;
            }
            else
            {
                equals = false;
                break;
            }
        }

        if (equals)
        {
            Victory.SetActive(true);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caltest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        int[] s = { 101, 102, -100, 200, -200, 501 };
        int sum = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] < 0) continue;
            sum += s[i];
        }
        print(sum);
    }

    // Update is called once per frame

}

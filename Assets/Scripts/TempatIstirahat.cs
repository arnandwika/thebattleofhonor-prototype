using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempatIstirahat : MonoBehaviour
{
    public static float[] xIstirahat = new float[8];
    public static float[] yIstirahat = new float[8];
    public int indeks;
    // Start is called before the first frame update
    void Start()
    {
        xIstirahat[indeks] = transform.position.x;
        yIstirahat[indeks] = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

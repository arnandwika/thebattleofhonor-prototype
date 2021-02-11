using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posisi : MonoBehaviour
{
    public int gerak;
    public static float x;
    public static float y;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        
    }

    void OnTriggerExit2D(Collider2D collision){
        
    }
}

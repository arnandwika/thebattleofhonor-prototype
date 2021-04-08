using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pemain : MonoBehaviour
{
    public static GameObject[] listBidak = new GameObject[25];
    // Start is called before the first frame update
    void Start()
    {
        listBidak = GameObject.FindGameObjectsWithTag("Pemain");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

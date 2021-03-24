using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // Start is called before the first frame update
    public static int[ , ] pangkatBidak = new int[14,5]{{0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0},
                                                        {0 , 0 , 0 , 0 , 0}};
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void printAll(){
        print(pangkatBidak);
    }

    public static void printSpesifik(int baris, int kolom){
        print(pangkatBidak[baris,kolom]);
    }

    public static void insertPangkat(int baris, int kolom, int pangkat){
        pangkatBidak[baris,kolom] = pangkat;
    }
}

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

    [ContextMenu("Debug Collection")]
    public void printAll(){
        foreach(var pangkat in pangkatBidak){
            Debug.Log(pangkat);
        }
    }

    public static void dataPangkatPindah(int barisPosAwal, int kolomPosAwal, int barisPosTujuan, int kolomPosTujuan, int pangkat){
        insertPangkat(barisPosTujuan, kolomPosTujuan, pangkat);
        zeroPangkat(barisPosAwal, kolomPosAwal);
    }

    public static void insertPangkat(int baris, int kolom, int pangkat){
        pangkatBidak[baris,kolom] = pangkat;
    }

    public static void zeroPangkat(int baris, int kolom){
        pangkatBidak[baris,kolom] = 0;
    }

    public static int getPangkat(int baris, int kolom){
        return pangkatBidak[baris,kolom];
    }

    public static string bidakBertabrakan(int barisPosAwal, int kolomPosAwal, int barisPosTujuan, int kolomPosTujuan){
        int pangkatMenabrak = getPangkat(barisPosAwal, kolomPosAwal);
        int pangkatDitabrak = getPangkat(barisPosTujuan, kolomPosTujuan);
        if(pangkatDitabrak == -2){
            return "draw";
        }else if(pangkatDitabrak == -1 && pangkatMenabrak == 2){
            return "menang";
        }else if(pangkatDitabrak == -1 && pangkatMenabrak !=2){
            return "draw";
        }else if(pangkatDitabrak == 1){
            return "selesai";
        }else if(pangkatMenabrak > pangkatDitabrak){
            return "menang";
        }else if(pangkatMenabrak == pangkatDitabrak){
            return "draw";
        }else{
            return "kalah";
        }
    }
}

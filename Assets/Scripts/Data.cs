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

    public static char[ , ] kepemilikanBidak = new char[14,5]{{'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'},
                                                            {'n' , 'n' , 'n' , 'n' , 'n'}};

    public static GameObject[ , ] listPosisi = new GameObject[14,5]{{null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null},
                                                                    {null , null , null , null , null}};

    public GameObject[] posisi;
    void Start()
    {
        int indeks = 0;
        for(var i = 0; i < 14; i++){
            for(var j = 0; j < 5; j++){
                if((i == 6 && j == 1) || (i == 6 && j == 3) || (i == 7 && j == 1) || (i == 7 && j == 3)){
                    
                }else{
                    listPosisi[i,j] = posisi[indeks];
                    indeks++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("printAllPangkat")]
    public void printAllPangkat(){
        foreach(var pangkat in pangkatBidak){
            Debug.Log(pangkat);
        }
    }

    [ContextMenu("printSpesifikPangkat")]
    public void printSpesifikPangkat(){
        print(pangkatBidak[6,2]);
        print(pangkatBidak[0,0]);
    }

    [ContextMenu("printAllKepemmilikan")]
    public void printAllKepemmilikan(){
        foreach(var kepemilikan in kepemilikanBidak){
            Debug.Log(kepemilikan);
        }
    }

    [ContextMenu("printSpesifikKepemilikan")]
    public void printSpesifikKepemilikan(){
        print(kepemilikanBidak[6,2]);
        print(kepemilikanBidak[0,0]);
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

    public static void dataKepemilikanPindah(int barisPosAwal, int kolomPosAwal, int barisPosTujuan, int kolomPosTujuan, char kepemilikan){
        insertKepemilikan(barisPosTujuan, kolomPosTujuan, kepemilikan);
        noneKepemilikan(barisPosAwal, kolomPosAwal);
    }

    public static void insertKepemilikan(int baris, int kolom, char kepemilikan){
        kepemilikanBidak[baris,kolom] = kepemilikan;
    }

    public static void noneKepemilikan(int baris, int kolom){
        kepemilikanBidak[baris,kolom] = 'n';
    }

    public static char getKepemilikan(int baris, int kolom){
        return kepemilikanBidak[baris,kolom];
    }

    public static string bidakBertabrakan(int barisPosAwal, int kolomPosAwal, int barisPosTujuan, int kolomPosTujuan){
        int pangkatMenabrak = getPangkat(barisPosAwal, kolomPosAwal);
        int pangkatDitabrak = getPangkat(barisPosTujuan, kolomPosTujuan);
        if(pangkatDitabrak == -2 || pangkatMenabrak == -2){
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

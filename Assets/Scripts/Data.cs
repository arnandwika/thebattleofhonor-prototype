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

    public static string namaPangkat(int pangkat){
        if(pangkat == -2){
            return "Bom";
        }else if(pangkat == -1){
            return "Dinamit";
        }else if(pangkat == 0){
            return "Error";
        }else if(pangkat == 1){
            return "Bendera";
        }else if(pangkat == 2){
            return "Prajurit";
        }else if(pangkat == 3){
            return "Sersan";
        }else if(pangkat == 4){
            return "Letnan";
        }else if(pangkat == 5){
            return "Kapten";
        }else if(pangkat == 6){
            return "Mayor";
        }else if(pangkat == 7){
            return "Kolonel";
        }else if(pangkat == 8){
            return "Brigjen";
        }else if(pangkat == 9){
            return "Letjen";
        }else if(pangkat == 10){
            return "Panglima";
        }else{
            return "Error";
        }
    }

    public static int nilaiPangkat(string nama){
        if(nama == "Bom"){
            return -2;
        }else if(nama == "Dinamit"){
            return -1;
        }else if(nama == "Error"){
            return 0;
        }else if(nama == "Bendera"){
            return 1;
        }else if(nama == "Prajurit"){
            return 2;
        }else if(nama == "Sersan"){
            return 3;
        }else if(nama == "Letnan"){
            return 4;
        }else if(nama == "Kapten"){
            return 5;
        }else if(nama == "Mayor"){
            return 6;
        }else if(nama == "Kolonel"){
            return 7;
        }else if(nama == "Brigjen"){
            return 8;
        }else if(nama == "Letjen"){
            return 9;
        }else if(nama == "Panglima"){
            return 0;
        }else{
            return 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

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

    public static GameObject[ , ] posisiBidak;

    public GameObject[] posisi;
    public static List<char[,]> dataKepemilikanBidak = new List<char[,]>();
    public static List<GameObject[,]> dataPosisiBidak = new List<GameObject[,]>();
    private static int turn = 1;
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
        dataKepemilikanBidak.Add(kepemilikanBidak);
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

    public static void updatePosisiBidak(){
        posisiBidak = new GameObject[14,5]{{null , null , null , null , null},
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
        GameObject[] bidakPemain = GameObject.FindGameObjectsWithTag("Pemain");
        GameObject[] bidakAI = GameObject.FindGameObjectsWithTag("Lawan");
        foreach(var bidak in bidakPemain){
            if(bidak!=null && bidak.GetComponent<Bidak>()){
                int baris = bidak.GetComponent<Bidak>().baris;
                int kolom = bidak.GetComponent<Bidak>().kolom;
                posisiBidak[baris,kolom] = bidak;
            }
        }
        foreach(var bidak in bidakAI){
            if(bidak!=null && bidak.GetComponent<Bidak>()){
                int baris = bidak.GetComponent<Bidak>().baris;
                int kolom = bidak.GetComponent<Bidak>().kolom;
                posisiBidak[baris,kolom] = bidak;
            }
        }
        dataPosisiBidak.Add(posisiBidak);
        WriteString();
    }

    public static void WriteResponseAI(string response){
        WriteTxt(response);
    }

    public static string bidakBertabrakan(int barisPosAwal, int kolomPosAwal, int barisPosTujuan, int kolomPosTujuan){
        int pangkatMenabrak = getPangkat(barisPosAwal, kolomPosAwal);
        int pangkatDitabrak = getPangkat(barisPosTujuan, kolomPosTujuan);
        if(pangkatDitabrak == -2 || pangkatMenabrak == -2){
            if((pangkatDitabrak == 10 && getKepemilikan(barisPosTujuan, kolomPosTujuan) == 'p') || (pangkatMenabrak == 10 && getKepemilikan(barisPosAwal, kolomPosAwal) == 'p')){
                AI.benderaExposed = true;
            }
            return "draw";
        }else if(pangkatDitabrak == -1 && pangkatMenabrak == 2){
            return "menang";
        }else if(pangkatDitabrak == -1 && pangkatMenabrak !=2){
            if(pangkatMenabrak == 10 && getKepemilikan(barisPosAwal, kolomPosAwal) == 'p'){
                AI.benderaExposed = true;
            }
            return "draw";
        }else if(pangkatDitabrak == 1){
            WriteTxt("GAME OVER");
            return "selesai";
        }else if(pangkatMenabrak > pangkatDitabrak){
            return "menang";
        }else if(pangkatMenabrak == pangkatDitabrak){
            if(pangkatDitabrak == 10 || pangkatMenabrak == 10){
                AI.benderaExposed = true;
            }
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
            return 10;
        }else{
            return 0;
        }
    }

    [MenuItem("Tools/Write file")]
    static void WriteString()
    {
        string path = "Assets/Resources/data.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        bool giliranPemain = Giliran.getGiliran();
        string giliran = "";
        if(giliranPemain){
            giliran = "AI";
        }else{
            giliran = "Player";
        }
        writer.WriteLine("Phase "+turn+", "+giliran+" turn");
        
        for(int i = 0; i < 14; i++){
            bool first = true;
            for(int j = 0; j < 5; j++){
                if(first && posisiBidak[i,j] != null){
                    writer.Write("| "+posisiBidak[i,j].GetComponent<Bidak>().nama+"("+posisiBidak[i,j].GetComponent<Bidak>().kepemilikan+")"+" | ");
                    first = false;
                }else if(!first && posisiBidak[i,j] != null){
                    writer.Write(posisiBidak[i,j].GetComponent<Bidak>().nama+"("+posisiBidak[i,j].GetComponent<Bidak>().kepemilikan+")"+" | ");
                }else if(first && posisiBidak[i,j] == null){
                    writer.Write("|   KOSONG   | ");
                    first = false;
                }else if(!first && posisiBidak[i,j] == null){
                    writer.Write("  KOSONG   | ");
                }
            }
            writer.WriteLine(" ");
        }
        turn+=1;
        writer.WriteLine(" ");
        
        writer.Close();

        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path); 
        //TextAsset asset = Resources.Load("test");

        //Print the text from the file
        //Debug.Log(asset.text);
    }

    [MenuItem("Tools/Write file")]
    static void WriteTxt(string response){
        string path = "Assets/Resources/data.txt";

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(response);
        writer.WriteLine(" ");
        writer.Close();
    }

    [MenuItem("Tools/Write file")]
    public static void WriteCase(string response){
        string path = "Assets/Resources/case.txt";

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(response);
        writer.WriteLine(" ");
        writer.Close();
    }

    [MenuItem("Tools/Read file")]
    static void ReadString()
    {
        string path = "Assets/Resources/data.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}

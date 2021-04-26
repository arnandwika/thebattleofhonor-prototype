using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    struct Cases
    {
        public int pangkatAI;
        public int predictionPangkatPemain;
        public int barisPos;
        public int kolomPos;

        public Cases(int pangkatAI, int predictionPangkatPemain, int barisPos, int kolomPos){
            this.pangkatAI = pangkatAI;
            this.predictionPangkatPemain = predictionPangkatPemain;
            this.barisPos = barisPos;
            this.kolomPos = kolomPos;
        }
    }
    
    struct Target
    {
        public GameObject bidakPemain;
        public GameObject bidakAI;
        public int jumlahLangkah;
        public GameObject hasilPathPosisi;

        public Target(GameObject bidakPemain, GameObject bidakAI, int jumlahLangkah, GameObject hasilPathPosisi){
            this.bidakPemain = bidakPemain;
            this.bidakAI = bidakAI;
            this.jumlahLangkah = jumlahLangkah;
            this.hasilPathPosisi = hasilPathPosisi;
        }
    }
    private char tempatIstirahat1;
    private char tempatIstirahat2;
    private char tempatIstirahat3;
    private char tempatIstirahat4;
    private bool giliranPemain = true;
    private int barisPosAwal;
    private int kolomPosAwal;
    private int barisPosTujuan;
    private int kolomPosTujuan;
    private static int strategy; //1 bertahan, 2 menyerang
    public GameObject[] listBidak;
    private float timer = 2;
    private int gerak;
    private static List<Cases> listKasus = new List<Cases>();
    private List<int> jumlahLangkah;
    private List<GameObject> bidakAIGerak;
    private List<int> cekLangkah;
    private int min=0;
    private GameObject tempBidak;
    private List<Target> listTarget;
    private List<GameObject> pathPosisi;
    private Dictionary<int, GameObject> tempPathPosisi;
    private List<GameObject> listPathPosisi;
    private GameObject hasilPathPosisi;
    private Dictionary<int, GameObject> tempBidakAI;
    private int counter;
    public static bool benderaExposed = false;
    // Start is called before the first frame update
    void Start()
    {
        strategy = Random.Range(1,3);
        listBidak = GameObject.FindGameObjectsWithTag("Lawan");
        giliranPemain = Giliran.getGiliran();
    }

    // Update is called once per frame
    void Update()
    {
        giliranPemain = Giliran.getGiliran();
        if(!giliranPemain){
            if(timer > 0){
                timer -= Time.deltaTime;
            }else{
                randomMovement();
                timer -= Time.deltaTime;
                if(timer < -10){
                    print("Error AI think too long");
                    strategy = Random.Range(1,4);
                }
            }
        }
    }

    public void randomMovement(){
        giliranPemain = Giliran.getGiliran();
        if(strategy == 1 && !giliranPemain){
            print("strategy 1");
            tempatIstirahat1 = Data.getKepemilikan(2, 1);
            tempatIstirahat2 = Data.getKepemilikan(2, 3);
            tempatIstirahat3 = Data.getKepemilikan(4, 1);
            tempatIstirahat4 = Data.getKepemilikan(4, 3);
            if(tempatIstirahat4 == 'n' || tempatIstirahat3 == 'n'){
                int random = Random.Range(0, 2);
                if(tempatIstirahat4 == 'n' && random == 0){
                    GameObject posisi = Data.listPosisi[4,3];
                    barisPosAwal = Random.Range(3, 6);
                    kolomPosAwal = Random.Range(2, 5);
                    if(AturanGerak.cekPangkatBergerak(barisPosAwal, kolomPosAwal)){
                        foreach(var bidak in listBidak){
                            if(bidak == null || (barisPosAwal == 4 && kolomPosAwal == 3)){
                                continue;
                            }else if(bidak.GetComponent<Bidak>().baris == barisPosAwal && bidak.GetComponent<Bidak>().kolom == kolomPosAwal){
                                bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                                posisi.tag = "Lawan";
                                GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                posisiAwal.tag = "Untagged";
                                Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, 4, 3, Data.getPangkat(barisPosAwal, kolomPosAwal));
                                Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, 4, 3, Data.getKepemilikan(barisPosAwal, kolomPosAwal));
                                Giliran.setGiliranPemain();
                                bidak.GetComponent<Bidak>().baris = 4;
                                bidak.GetComponent<Bidak>().kolom = 3;
                                bidak.GetComponent<Bidak>().gerak = posisi.GetComponent<Posisi>().gerak;
                                timer = 2;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                
                            }
                        }
                    }  
                }else if(tempatIstirahat3 == 'n'){
                    GameObject posisi = Data.listPosisi[4,1];
                    barisPosAwal = Random.Range(3, 6);
                    kolomPosAwal = Random.Range(0, 3);
                    if(AturanGerak.cekPangkatBergerak(barisPosAwal, kolomPosAwal)){
                        foreach(var bidak in listBidak){
                            if(bidak == null || (barisPosAwal == 4 && kolomPosAwal == 1)){
                                continue;
                            }else if(bidak.GetComponent<Bidak>().baris == barisPosAwal && bidak.GetComponent<Bidak>().kolom == kolomPosAwal){
                                bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                                posisi.tag = "Lawan";
                                GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                posisiAwal.tag = "Untagged";
                                Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, 4, 1, Data.getPangkat(barisPosAwal, kolomPosAwal));
                                Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, 4, 1, Data.getKepemilikan(barisPosAwal, kolomPosAwal));
                                Giliran.setGiliranPemain();
                                bidak.GetComponent<Bidak>().baris = 4;
                                bidak.GetComponent<Bidak>().kolom = 1;
                                bidak.GetComponent<Bidak>().gerak = posisi.GetComponent<Posisi>().gerak;
                                timer = 2;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                
                            }
                        }
                    }
                }else if(tempatIstirahat4 == 'n'){
                    GameObject posisi = Data.listPosisi[4,3];
                    barisPosAwal = Random.Range(3, 6);
                    kolomPosAwal = Random.Range(2, 5);
                    if(AturanGerak.cekPangkatBergerak(barisPosAwal, kolomPosAwal)){
                        foreach(var bidak in listBidak){
                            if(bidak == null || (barisPosAwal == 4 && kolomPosAwal == 3)){
                                continue;
                            }else if(bidak.GetComponent<Bidak>().baris == barisPosAwal && bidak.GetComponent<Bidak>().kolom == kolomPosAwal){
                                bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                                posisi.tag = "Lawan";
                                GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                posisiAwal.tag = "Untagged";
                                Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, 4, 3, Data.getPangkat(barisPosAwal, kolomPosAwal));
                                Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, 4, 3, Data.getKepemilikan(barisPosAwal, kolomPosAwal));
                                Giliran.setGiliranPemain();
                                bidak.GetComponent<Bidak>().baris = 4;
                                bidak.GetComponent<Bidak>().kolom = 3;
                                bidak.GetComponent<Bidak>().gerak = posisi.GetComponent<Posisi>().gerak;
                                timer = 2;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                
                            }
                        }
                    }
                }else{
                    randomMovement();
                }
            }
            else if(tempatIstirahat2 == 'n' || tempatIstirahat1 == 'n'){
                int random = Random.Range(0, 2);
                if(tempatIstirahat2 == 'n' && random == 0){
                    GameObject posisi = Data.listPosisi[2,3];
                    barisPosAwal = Random.Range(1, 4);
                    kolomPosAwal = Random.Range(2, 5);
                    if(AturanGerak.cekPangkatBergerak(barisPosAwal, kolomPosAwal)){
                        foreach(var bidak in listBidak){
                            if(bidak == null || (barisPosAwal == 2 && kolomPosAwal == 3)){
                                continue;
                            }else if(bidak.GetComponent<Bidak>().baris == barisPosAwal && bidak.GetComponent<Bidak>().kolom == kolomPosAwal){
                                bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                                posisi.tag = "Lawan";
                                GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                posisiAwal.tag = "Untagged";
                                Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, 2, 3, Data.getPangkat(barisPosAwal, kolomPosAwal));
                                Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, 2, 3, Data.getKepemilikan(barisPosAwal, kolomPosAwal));
                                Giliran.setGiliranPemain();
                                bidak.GetComponent<Bidak>().baris = 2;
                                bidak.GetComponent<Bidak>().kolom = 3;
                                bidak.GetComponent<Bidak>().gerak = posisi.GetComponent<Posisi>().gerak;
                                timer = 2;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                
                            }
                        }
                    }
                }else if(tempatIstirahat1 == 'n'){
                    GameObject posisi = Data.listPosisi[2,1];
                    barisPosAwal = Random.Range(1, 4);
                    kolomPosAwal = Random.Range(0, 3);
                    if(AturanGerak.cekPangkatBergerak(barisPosAwal, kolomPosAwal)){
                        foreach(var bidak in listBidak){
                            if(bidak == null || (barisPosAwal == 2 && kolomPosAwal == 1)){
                                continue;
                            }else if(bidak.GetComponent<Bidak>().baris == barisPosAwal && bidak.GetComponent<Bidak>().kolom == kolomPosAwal){
                                bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                                posisi.tag = "Lawan";
                                GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                posisiAwal.tag = "Untagged";
                                Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, 2, 1, Data.getPangkat(barisPosAwal, kolomPosAwal));
                                Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, 2, 1, Data.getKepemilikan(barisPosAwal, kolomPosAwal));
                                Giliran.setGiliranPemain();
                                bidak.GetComponent<Bidak>().baris = 2;
                                bidak.GetComponent<Bidak>().kolom = 1;
                                bidak.GetComponent<Bidak>().gerak = posisi.GetComponent<Posisi>().gerak;
                                timer = 2;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                
                            }
                        }
                    }
                }else if(tempatIstirahat2 == 'n'){
                    GameObject posisi = Data.listPosisi[2,3];
                    barisPosAwal = Random.Range(1, 4);
                    kolomPosAwal = Random.Range(2, 5);
                    if(AturanGerak.cekPangkatBergerak(barisPosAwal, kolomPosAwal)){
                        foreach(var bidak in listBidak){
                            if(bidak == null || (barisPosAwal == 2 && kolomPosAwal == 3)){
                                continue;
                            }else if(bidak.GetComponent<Bidak>().baris == barisPosAwal && bidak.GetComponent<Bidak>().kolom == kolomPosAwal){
                                bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                                posisi.tag = "Lawan";
                                GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                posisiAwal.tag = "Untagged";
                                Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, 2, 3, Data.getPangkat(barisPosAwal, kolomPosAwal));
                                Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, 2, 3, Data.getKepemilikan(barisPosAwal, kolomPosAwal));
                                Giliran.setGiliranPemain();
                                bidak.GetComponent<Bidak>().baris = 2;
                                bidak.GetComponent<Bidak>().kolom = 3;
                                bidak.GetComponent<Bidak>().gerak = posisi.GetComponent<Posisi>().gerak;
                                timer = 2;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                
                            }
                        }
                    }
                }else{
                    randomMovement();
                }
            }else{
                strategy = 2;
            }
        }
        if(strategy == 2 && !giliranPemain){
            print("strategy 2");
            int indeks = Random.Range(0, listBidak.Length);
            GameObject bidak = listBidak[indeks];
            if(bidak != null){
                barisPosAwal = bidak.GetComponent<Bidak>().baris;
                kolomPosAwal = bidak.GetComponent<Bidak>().kolom;
                if(AturanGerak.cekPangkatBergerak(barisPosAwal, kolomPosAwal)){
                    gerak = bidak.GetComponent<Bidak>().gerak;
                    bool hasil = false;
                    int loop = 0;
                    while(hasil == false){
                        if(loop >10){
                            break;
                        }
                        if(bidak.GetComponent<Bidak>().baris < 7){
                            barisPosTujuan = Random.Range(bidak.GetComponent<Bidak>().baris+1, 8);
                            kolomPosTujuan = Random.Range(0, 5);
                        }else{
                            barisPosTujuan = Random.Range(bidak.GetComponent<Bidak>().baris+1, 14);
                            kolomPosTujuan = Random.Range(0, 5);
                        }
                        if(AturanGerak.opsiGerak(gerak, barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan)){
                            GameObject posisi = Data.listPosisi[barisPosTujuan,kolomPosTujuan];
                            if(posisi != null && posisi.tag != "Lawan"){
                                if(posisi.tag == "Untagged"){
                                    bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                                    posisi.tag = "Lawan";
                                    GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                    posisiAwal.tag = "Untagged";
                                    Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan, Data.getPangkat(barisPosAwal, kolomPosAwal));
                                    Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan, Data.getKepemilikan(barisPosAwal, kolomPosAwal));
                                    Giliran.setGiliranPemain();
                                    bidak.GetComponent<Bidak>().baris = posisi.GetComponent<Posisi>().barisPos;
                                    bidak.GetComponent<Bidak>().kolom = posisi.GetComponent<Posisi>().kolomPos;
                                    bidak.GetComponent<Bidak>().gerak = posisi.GetComponent<Posisi>().gerak;
                                    timer = 2;
                                    hasil = true;
                                    strategy = Random.Range(1,3);
                                    Data.WriteResponseAI("AI moving forward");
                                    Data.updatePosisiBidak();
                                    
                                }else if(posisi.tag == "Pemain"){
                                    if((barisPosTujuan == TempatIstirahat.barisIstirahat[0] && kolomPosTujuan == TempatIstirahat.kolomIstirahat[0]) || 
                                    (barisPosTujuan == TempatIstirahat.barisIstirahat[1] && kolomPosTujuan == TempatIstirahat.kolomIstirahat[1]) ||
                                    (barisPosTujuan == TempatIstirahat.barisIstirahat[2] && kolomPosTujuan == TempatIstirahat.kolomIstirahat[2]) ||
                                    (barisPosTujuan == TempatIstirahat.barisIstirahat[3] && kolomPosTujuan == TempatIstirahat.kolomIstirahat[3]) ||
                                    (barisPosTujuan == TempatIstirahat.barisIstirahat[4] && kolomPosTujuan == TempatIstirahat.kolomIstirahat[4]) ||
                                    (barisPosTujuan == TempatIstirahat.barisIstirahat[5] && kolomPosTujuan == TempatIstirahat.kolomIstirahat[5]) ||
                                    (barisPosTujuan == TempatIstirahat.barisIstirahat[6] && kolomPosTujuan == TempatIstirahat.kolomIstirahat[6]) ||
                                    (barisPosTujuan == TempatIstirahat.barisIstirahat[7] && kolomPosTujuan == TempatIstirahat.kolomIstirahat[7])){
                                        break;
                                    }
                                    string output = Data.bidakBertabrakan(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan);
                                    if(output == "menang"){
                                        bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                                        posisi.tag = "Lawan";
                                        GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                        posisiAwal.tag = "Untagged";
                                        bidak.GetComponent<Bidak>().baris = barisPosTujuan;
                                        bidak.GetComponent<Bidak>().kolom = kolomPosTujuan;
                                        bidak.GetComponent<Bidak>().gerak = gerak;
                                        Giliran.setGiliranLawan();
                                        Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan, Data.getPangkat(barisPosAwal, kolomPosAwal));
                                        Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan, Data.getKepemilikan(barisPosAwal, kolomPosAwal));
                                        GameObject[] listBidakPemain = GameObject.FindGameObjectsWithTag("Pemain");
                                        foreach(var bidakPemain in listBidakPemain){
                                            if(bidakPemain.GetComponent<Bidak>().baris == barisPosTujuan && bidakPemain.GetComponent<Bidak>().kolom == kolomPosTujuan){
                                                Destroy(bidakPemain);
                                                break;
                                            }
                                        }
                                        timer = 2;
                                        hasil = true;
                                        strategy = Random.Range(1,3);
                                        Data.WriteResponseAI("AI moving forward and win");
                                        Data.updatePosisiBidak();
                                        
                                        print("AI menang bertabrakan dengan pemain");
                                    }else if(output == "draw"){
                                        posisi.tag = "Untagged";
                                        GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                        posisiAwal.tag = "Untagged";
                                        bidak.GetComponent<Bidak>().baris = barisPosTujuan;
                                        bidak.GetComponent<Bidak>().kolom = kolomPosTujuan;
                                        bidak.GetComponent<Bidak>().gerak = gerak;
                                        Data.zeroPangkat(barisPosAwal, kolomPosAwal);
                                        Data.zeroPangkat(barisPosTujuan, kolomPosTujuan);
                                        Data.noneKepemilikan(barisPosAwal, kolomPosAwal);
                                        Data.noneKepemilikan(barisPosTujuan, kolomPosTujuan);
                                        Giliran.setGiliranPemain();
                                        GameObject[] listBidakPemain = GameObject.FindGameObjectsWithTag("Pemain");
                                        foreach(var bidakPemain in listBidakPemain){
                                            if(bidakPemain.GetComponent<Bidak>().baris == barisPosTujuan && bidakPemain.GetComponent<Bidak>().kolom == kolomPosTujuan){
                                                Destroy(bidakPemain);
                                                break;
                                            }
                                        }
                                        Destroy(bidak);
                                        timer = 2;
                                        hasil = true;
                                        strategy = Random.Range(1,3);
                                        Data.WriteResponseAI("AI moving forward and draw");
                                        Data.updatePosisiBidak();
                                        
                                        print("Draw");
                                    }else if(output == "kalah"){
                                        GameObject[] listBidakPemain = GameObject.FindGameObjectsWithTag("Pemain");
                                        foreach(var bidakPemain in listBidakPemain){
                                            if(bidakPemain.GetComponent<Bidak>().baris == barisPosTujuan && bidakPemain.GetComponent<Bidak>().kolom == kolomPosTujuan){
                                                AI.markBidak(bidak.GetComponent<Bidak>().pangkat, bidakPemain, barisPosTujuan, kolomPosTujuan);
                                                break;
                                            }
                                        }
                                        GameObject posisiAwal = Data.listPosisi[barisPosAwal, kolomPosAwal];
                                        posisiAwal.tag = "Untagged";
                                        bidak.GetComponent<Bidak>().baris = barisPosTujuan;
                                        bidak.GetComponent<Bidak>().kolom = kolomPosTujuan;
                                        bidak.GetComponent<Bidak>().gerak = gerak;
                                        Data.zeroPangkat(barisPosAwal, kolomPosAwal);
                                        Data.noneKepemilikan(barisPosAwal, kolomPosAwal);
                                        Giliran.setGiliranPemain();
                                        Destroy(bidak);
                                        timer = 2;
                                        hasil = true;
                                        Data.WriteResponseAI("AI moving forward and lose");
                                        Data.updatePosisiBidak();
                                        
                                        print("AI kalah bertabrakan dengan pemain");
                                    }else if(output == "selesai"){
                                        print("GAME SELESAI");
                                        SceneManager.LoadScene("Main Menu");
                                    }
                                }
                            }
                        }
                        loop++;
                    }
                }
            }
        }
        if(strategy == 3 && !giliranPemain){
            print("strategy 3");
            listTarget = new List<Target>();
            GameObject[] listBidakPemain = GameObject.FindGameObjectsWithTag("Pemain");
            foreach(var bidakPemain in listBidakPemain){
                if(bidakPemain != null && bidakPemain.GetComponentInChildren<Text>()){
                    min=0;
                    cekLangkah = new List<int>();
                    listPathPosisi = new List<GameObject>();
                    bidakAIGerak = new List<GameObject>();
                    string predictionNamaPemain = bidakPemain.GetComponentInChildren<Text>().text;
                    int predictionPangkatPemain = 0;
                    int jumlahPenghalang = 0;
                    if(predictionNamaPemain == " " && bidakPemain.GetComponent<Bidak>().baris < 7){
                        foreach(var bidakAI in listBidak){
                            if(bidakAI != null && bidakAI.GetComponent<Bidak>().kolom == 0 && bidakPemain.GetComponent<Bidak>().kolom == 0 && bidakAI.GetComponent<Bidak>().baris < 6){
                                jumlahPenghalang+=1;
                            }else if(bidakAI != null && bidakAI.GetComponent<Bidak>().kolom == 4 && bidakPemain.GetComponent<Bidak>().kolom == 4 && bidakAI.GetComponent<Bidak>().baris < 6){
                                jumlahPenghalang+=1;
                            }
                        }
                        if(jumlahPenghalang < 2){
                            foreach(var bidakAI in listBidak){
                                if(bidakAI != null){
                                    if(bidakAI.GetComponent<Bidak>().pangkat > 1){
                                        counter = 0;
                                        jumlahLangkah = new List<int>();
                                        tempPathPosisi = new Dictionary<int, GameObject>();
                                        tempBidakAI = new Dictionary<int, GameObject>();
                                        // pathPosisi = new List<GameObject>();
                                        int barisPosAI = bidakAI.GetComponent<Bidak>().baris;
                                        int kolomPosAI = bidakAI.GetComponent<Bidak>().kolom;
                                        int barisPosPemain = bidakPemain.GetComponent<Bidak>().baris;
                                        int kolomPosPemain = bidakPemain.GetComponent<Bidak>().kolom;
                                        int langkah = 0;
                                        GameObject posisiAI = Data.listPosisi[barisPosAI, kolomPosAI];
                                        GameObject posisiPemain = Data.listPosisi[barisPosPemain, kolomPosPemain];
                                        GameObject posisiNextAI = posisiPemain;
                                        int hasil = cekPath(posisiAI, posisiPemain, langkah, posisiNextAI, bidakAI);
                                        if(tempBidakAI.Count > 0 && jumlahLangkah.Count > 0 && tempPathPosisi.Count > 0){
                                            jumlahLangkah.Sort();
                                            bidakAIGerak.Add(tempBidakAI[jumlahLangkah[0]]);
                                            cekLangkah.Add(jumlahLangkah[0]);
                                            listPathPosisi.Add(tempPathPosisi[jumlahLangkah[0]]);
                                        }
                                    }
                                }
                            }
                            if(cekLangkah.Count > 0 && bidakAIGerak.Count > 0 && listPathPosisi.Count > 0){
                                min = cekLangkah[0];
                                tempBidak = bidakAIGerak[0];
                                hasilPathPosisi = listPathPosisi[0];
                                //print(hasilPathPosisi);
                                for(int i = 1; i<cekLangkah.Count; i++){
                                    if(cekLangkah[i] < min ){
                                        min = cekLangkah[i];
                                        tempBidak = bidakAIGerak[i];
                                        hasilPathPosisi = listPathPosisi[i];
                                    }
                                }
                                Target target = new Target(bidakPemain, tempBidak, min, hasilPathPosisi);
                                listTarget.Add(target);
                            }
                        }
                    }else if(predictionNamaPemain != " "){
                        predictionPangkatPemain = Data.nilaiPangkat(predictionNamaPemain);
                        foreach(var bidakAI in listBidak){
                            if(bidakAI != null){
                                if(bidakAI.GetComponent<Bidak>().pangkat > predictionPangkatPemain || (predictionPangkatPemain > 8 && bidakAI.GetComponent<Bidak>().pangkat == -2)){
                                    counter = 0;
                                    jumlahLangkah = new List<int>();
                                    tempPathPosisi = new Dictionary<int, GameObject>();
                                    tempBidakAI = new Dictionary<int, GameObject>();
                                    // pathPosisi = new List<GameObject>();
                                    int barisPosAI = bidakAI.GetComponent<Bidak>().baris;
                                    int kolomPosAI = bidakAI.GetComponent<Bidak>().kolom;
                                    int barisPosPemain = bidakPemain.GetComponent<Bidak>().baris;
                                    int kolomPosPemain = bidakPemain.GetComponent<Bidak>().kolom;
                                    if((barisPosPemain == TempatIstirahat.barisIstirahat[0] && kolomPosPemain == TempatIstirahat.kolomIstirahat[0]) || 
                                    (barisPosPemain == TempatIstirahat.barisIstirahat[1] && kolomPosPemain == TempatIstirahat.kolomIstirahat[1]) ||
                                    (barisPosPemain == TempatIstirahat.barisIstirahat[2] && kolomPosPemain == TempatIstirahat.kolomIstirahat[2]) ||
                                    (barisPosPemain == TempatIstirahat.barisIstirahat[3] && kolomPosPemain == TempatIstirahat.kolomIstirahat[3]) ||
                                    (barisPosPemain == TempatIstirahat.barisIstirahat[4] && kolomPosPemain == TempatIstirahat.kolomIstirahat[4]) ||
                                    (barisPosPemain == TempatIstirahat.barisIstirahat[5] && kolomPosPemain == TempatIstirahat.kolomIstirahat[5]) ||
                                    (barisPosPemain == TempatIstirahat.barisIstirahat[6] && kolomPosPemain == TempatIstirahat.kolomIstirahat[6]) ||
                                    (barisPosPemain == TempatIstirahat.barisIstirahat[7] && kolomPosPemain == TempatIstirahat.kolomIstirahat[7])){
                                        break;
                                    }
                                    int langkah = 0;
                                    GameObject posisiAI = Data.listPosisi[barisPosAI, kolomPosAI];
                                    GameObject posisiPemain = Data.listPosisi[barisPosPemain, kolomPosPemain];
                                    GameObject posisiNextAI = posisiPemain;
                                    int hasil = cekPath(posisiAI, posisiPemain, langkah, posisiNextAI, bidakAI);
                                    if(tempBidakAI.Count > 0 && jumlahLangkah.Count > 0 && tempPathPosisi.Count > 0){
                                        jumlahLangkah.Sort();
                                        bidakAIGerak.Add(tempBidakAI[jumlahLangkah[0]]);
                                        cekLangkah.Add(jumlahLangkah[0]);
                                        listPathPosisi.Add(tempPathPosisi[jumlahLangkah[0]]);
                                    }
                                }
                            }
                        }
                        if(cekLangkah.Count > 0 && bidakAIGerak.Count > 0 && listPathPosisi.Count > 0){
                            min = cekLangkah[0];
                            tempBidak = bidakAIGerak[0];
                            hasilPathPosisi = listPathPosisi[0];
                            //print(hasilPathPosisi);
                            for(int i = 1; i<cekLangkah.Count; i++){
                                if(cekLangkah[i] < min ){
                                    min = cekLangkah[i];
                                    tempBidak = bidakAIGerak[i];
                                    hasilPathPosisi = listPathPosisi[i];
                                }
                            }
                            Target target = new Target(bidakPemain, tempBidak, min, hasilPathPosisi);
                            listTarget.Add(target);
                        }
                    }
                    
                }else{
                    continue;
                }
            }
            bool moved = false;
            listTarget.Sort((s1 , s2) => s1.jumlahLangkah.CompareTo(s2.jumlahLangkah));
            if(listTarget.Count > 0){
                print("Jumlah target : "+listTarget.Count);
                foreach(var target in listTarget){
                    print("Bidak AI : "+target.bidakAI.GetComponent<Bidak>().nama);
                    print("Perkiraan Bidak Pemain : "+target.bidakPemain.GetComponentInChildren<Text>().text);
                    print("Langkah Pertama Bidak AI : "+target.hasilPathPosisi);
                    print("Jumlah Langkah : "+target.jumlahLangkah);
                }
                foreach(var target in listTarget){
                    if(moved){
                        break;
                    }
                    int barisAwal = target.bidakAI.GetComponent<Bidak>().baris;
                    int kolomAwal = target.bidakAI.GetComponent<Bidak>().kolom;
                    int gerak = target.bidakAI.GetComponent<Bidak>().gerak;
                    int barisKe = target.hasilPathPosisi.GetComponent<Posisi>().barisPos;
                    int kolomKe = target.hasilPathPosisi.GetComponent<Posisi>().kolomPos;
                    if(Data.getKepemilikan(barisKe, kolomKe) == 'n' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                        print(target.hasilPathPosisi.GetComponent<Posisi>().tag);
                        target.bidakAI.transform.position = new Vector3(target.hasilPathPosisi.transform.position.x, target.hasilPathPosisi.transform.position.y, target.hasilPathPosisi.transform.position.z);
                        target.hasilPathPosisi.tag = "Lawan";
                        GameObject posisiAwal = Data.listPosisi[barisAwal, kolomAwal];
                        posisiAwal.tag = "Untagged";
                        Data.dataPangkatPindah(barisAwal, kolomAwal, barisKe, kolomKe, Data.getPangkat(barisAwal, kolomAwal));
                        Data.dataKepemilikanPindah(barisAwal, kolomAwal, barisKe, kolomKe, Data.getKepemilikan(barisAwal, kolomAwal));
                        Giliran.setGiliranPemain();
                        target.bidakAI.GetComponent<Bidak>().baris = target.hasilPathPosisi.GetComponent<Posisi>().barisPos;
                        target.bidakAI.GetComponent<Bidak>().kolom = target.hasilPathPosisi.GetComponent<Posisi>().kolomPos;
                        target.bidakAI.GetComponent<Bidak>().gerak = target.hasilPathPosisi.GetComponent<Posisi>().gerak;
                        timer = 2;
                        moved = true;
                        Data.WriteResponseAI("AI try to take down predicted "+target.bidakPemain.GetComponentInChildren<Text>().text+" by "+target.bidakAI.GetComponent<Bidak>().nama+" with "+target.jumlahLangkah+" step");
                        Data.updatePosisiBidak();
                        
                        print("AI berusaha menjatuhkan bidak pemain yang diperkirakan berpangkat "+target.bidakPemain.GetComponentInChildren<Text>().text);
                    }else if(Data.getKepemilikan(barisKe, kolomKe) == 'l' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                        print(target.hasilPathPosisi.GetComponent<Posisi>().tag);
                        GameObject[] listPosisiGerak = target.hasilPathPosisi.GetComponent<Posisi>().ke;
                        foreach(var posisiGerak in listPosisiGerak){
                            int tempBaris = posisiGerak.GetComponent<Posisi>().barisPos;
                            int tempKolom = posisiGerak.GetComponent<Posisi>().kolomPos;
                            gerak = target.hasilPathPosisi.GetComponent<Posisi>().gerak;
                            if(Data.getKepemilikan(tempBaris, tempKolom) == 'n' ){
                                GameObject bidakFirstPath = null;
                                foreach(var bidak in listBidak){
                                    if(bidak != null && bidak.GetComponent<Bidak>().baris == barisKe && bidak.GetComponent<Bidak>().kolom == kolomKe){
                                        bidakFirstPath = bidak;
                                    }
                                }
                                if(bidakFirstPath != null && AturanGerak.opsiGerak(gerak, barisKe, kolomKe, tempBaris, tempKolom)){
                                    bidakFirstPath.transform.position = new Vector3(posisiGerak.transform.position.x, posisiGerak.transform.position.y, posisiGerak.transform.position.z);
                                    posisiGerak.tag = "Lawan";
                                    GameObject posisiAwal = Data.listPosisi[barisKe, kolomKe];
                                    posisiAwal.tag = "Untagged";
                                    Data.dataPangkatPindah(barisKe, kolomKe, tempBaris, tempKolom, Data.getPangkat(barisKe, kolomKe));
                                    Data.dataKepemilikanPindah(barisKe, kolomKe, tempBaris, tempKolom, Data.getKepemilikan(barisKe, kolomKe));
                                    Giliran.setGiliranPemain();
                                    bidakFirstPath.GetComponent<Bidak>().baris = posisiGerak.GetComponent<Posisi>().barisPos;
                                    bidakFirstPath.GetComponent<Bidak>().kolom = posisiGerak.GetComponent<Posisi>().kolomPos;
                                    bidakFirstPath.GetComponent<Bidak>().gerak = posisiGerak.GetComponent<Posisi>().gerak;
                                    timer = 2;
                                    moved = true;
                                    Data.WriteResponseAI("AI try to take down predicted "+target.bidakPemain.GetComponentInChildren<Text>().text+" by "+target.bidakAI.GetComponent<Bidak>().nama+" with "+target.jumlahLangkah+" step");
                                    Data.updatePosisiBidak();
                                    
                                    print("AI berusaha menjatuhkan bidak pemain yang diperkirakan berpangkat "+target.bidakPemain.GetComponentInChildren<Text>().text);
                                }
                            }
                        }
                    }else if(Data.getKepemilikan(barisKe, kolomKe) == 'p' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                        print(target.hasilPathPosisi.GetComponent<Posisi>().tag);
                        if((barisKe == TempatIstirahat.barisIstirahat[0] && kolomKe == TempatIstirahat.kolomIstirahat[0]) || 
                        (barisKe == TempatIstirahat.barisIstirahat[1] && kolomKe == TempatIstirahat.kolomIstirahat[1]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[2] && kolomKe == TempatIstirahat.kolomIstirahat[2]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[3] && kolomKe == TempatIstirahat.kolomIstirahat[3]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[4] && kolomKe == TempatIstirahat.kolomIstirahat[4]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[5] && kolomKe == TempatIstirahat.kolomIstirahat[5]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[6] && kolomKe == TempatIstirahat.kolomIstirahat[6]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[7] && kolomKe == TempatIstirahat.kolomIstirahat[7])){
                            Data.WriteResponseAI("Targeted bidak in Tempat Istirahat");
                            print("Pemain di tempat istirahat");
                        }else{
                            string output = Data.bidakBertabrakan(barisAwal, kolomAwal, barisKe, kolomKe);
                            if(output == "menang"){
                                target.bidakAI.transform.position = new Vector3(target.hasilPathPosisi.transform.position.x, target.hasilPathPosisi.transform.position.y, target.hasilPathPosisi.transform.position.z);
                                target.hasilPathPosisi.tag = "Lawan";
                                GameObject posisiAwal = Data.listPosisi[barisAwal, kolomAwal];
                                posisiAwal.tag = "Untagged";
                                target.bidakAI.GetComponent<Bidak>().baris = barisKe;
                                target.bidakAI.GetComponent<Bidak>().kolom = kolomKe;
                                target.bidakAI.GetComponent<Bidak>().gerak = gerak;
                                Giliran.setGiliranLawan();
                                Data.dataPangkatPindah(barisAwal, kolomAwal, barisKe, kolomKe, Data.getPangkat(barisAwal, kolomAwal));
                                Data.dataKepemilikanPindah(barisAwal, kolomAwal, barisKe, kolomKe, Data.getKepemilikan(barisAwal, kolomAwal));
                                foreach(var bidakPemain in listBidakPemain){
                                    if(bidakPemain.GetComponent<Bidak>().baris == barisKe && bidakPemain.GetComponent<Bidak>().kolom == kolomKe){
                                        Destroy(bidakPemain);
                                        break;
                                    }
                                }
                                timer = 2;
                                moved = true;
                                Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" and win");
                                Data.updatePosisiBidak();
                                
                                print("AI menang bertabrakan dengan pemain");
                            }else if(output == "draw"){
                                target.hasilPathPosisi.tag = "Untagged";
                                GameObject posisiAwal = Data.listPosisi[barisAwal, kolomAwal];
                                posisiAwal.tag = "Untagged";
                                target.bidakAI.GetComponent<Bidak>().baris = barisKe;
                                target.bidakAI.GetComponent<Bidak>().kolom = kolomKe;
                                target.bidakAI.GetComponent<Bidak>().gerak = gerak;
                                Data.zeroPangkat(barisAwal, kolomAwal);
                                Data.zeroPangkat(barisKe, kolomKe);
                                Data.noneKepemilikan(barisAwal, kolomAwal);
                                Data.noneKepemilikan(barisKe, kolomKe);
                                Giliran.setGiliranPemain();
                                foreach(var bidakPemain in listBidakPemain){
                                    if(bidakPemain.GetComponent<Bidak>().baris == barisKe && bidakPemain.GetComponent<Bidak>().kolom == kolomKe){
                                        Destroy(bidakPemain);
                                        break;
                                    }
                                }
                                Destroy(target.bidakAI);
                                timer = 2;
                                moved = true;
                                Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" draw");
                                Data.updatePosisiBidak();
                                
                                print("Draw");
                            }else if(output == "kalah"){
                                foreach(var bidakPemain in listBidakPemain){
                                    if(bidakPemain.GetComponent<Bidak>().baris == barisKe && bidakPemain.GetComponent<Bidak>().kolom == kolomKe){
                                        AI.markBidak(target.bidakAI.GetComponent<Bidak>().pangkat, bidakPemain, barisKe, kolomKe);
                                        break;
                                    }
                                }
                                GameObject posisiAwal = Data.listPosisi[barisAwal, kolomAwal];
                                posisiAwal.tag = "Untagged";
                                target.bidakAI.GetComponent<Bidak>().baris = barisKe;
                                target.bidakAI.GetComponent<Bidak>().kolom = kolomKe;
                                target.bidakAI.GetComponent<Bidak>().gerak = gerak;
                                Data.zeroPangkat(barisAwal, kolomAwal);
                                Data.noneKepemilikan(barisAwal, kolomAwal);
                                Giliran.setGiliranPemain();
                                Destroy(target.bidakAI);
                                timer = 2;
                                moved = true;
                                Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" lose");
                                Data.updatePosisiBidak();
                                
                                print("AI kalah bertabrakan dengan pemain");
                            }else if(output == "selesai"){
                                print("GAME SELESAI");
                                SceneManager.LoadScene("Main Menu");
                            }
                        }
                    }
                }
            }
            if(moved){
                strategy = 3;
            }else if(!moved){
                Data.WriteResponseAI("Try another strategy than 3");
                print("Random Move karena persyaratan tidak terpenuhi");
                strategy = Random.Range(1,3);
            }
        }
        if(strategy == 4 && !giliranPemain){
            print("strategy 4");
            List <GameObject> listBidakAIDepan = new List<GameObject>();
            GameObject[] listBidakPemain = GameObject.FindGameObjectsWithTag("Pemain");
            GameObject bidakBenderaPemain = null;
            int barisPosBendera = 0;
            int kolomPosBendera = 0;
            if(!benderaExposed){
                Data.WriteResponseAI("Bendera not exposed yet, randoming guess");
                print("Bendera belum tertebak");
                int random = Random.Range(1,3);
                if(random == 1){
                    barisPosBendera = 13;
                    kolomPosBendera = 1;
                }else{
                    barisPosBendera = 13;
                    kolomPosBendera = 3;
                }
            }else{
                foreach(var bidakBendera in listBidakPemain){
                    if(bidakBendera.GetComponent<Bidak>().nama == "bendera"){
                        barisPosBendera = bidakBendera.GetComponent<Bidak>().baris;
                        kolomPosBendera = bidakBendera.GetComponent<Bidak>().kolom;
                        bidakBenderaPemain = bidakBendera;
                    }
                }
            }
            foreach(var bidakAI in listBidak){
                if(bidakAI != null && bidakAI.GetComponent<Bidak>().baris > 5){
                    listBidakAIDepan.Add(bidakAI);
                }
            }
            if(listBidakAIDepan.Count>0){
                foreach(var bidakAI in listBidakAIDepan){
                    if(bidakAI != null){
                        counter = 0;
                        jumlahLangkah = new List<int>();
                        tempPathPosisi = new Dictionary<int, GameObject>();
                        tempBidakAI = new Dictionary<int, GameObject>();
                        // pathPosisi = new List<GameObject>();
                        int barisPosAI = bidakAI.GetComponent<Bidak>().baris;
                        int kolomPosAI = bidakAI.GetComponent<Bidak>().kolom;
                        int langkah = 0;
                        GameObject posisiAI = Data.listPosisi[barisPosAI, kolomPosAI];
                        GameObject posisiPemain = Data.listPosisi[barisPosBendera, kolomPosBendera];
                        GameObject posisiNextAI = posisiPemain;
                        int hasil = cekPath(posisiAI, posisiPemain, langkah, posisiNextAI, bidakAI);
                        if(tempBidakAI.Count > 0 && jumlahLangkah.Count > 0 && tempPathPosisi.Count > 0){
                            jumlahLangkah.Sort();
                            bidakAIGerak.Add(tempBidakAI[jumlahLangkah[0]]);
                            cekLangkah.Add(jumlahLangkah[0]);
                            listPathPosisi.Add(tempPathPosisi[jumlahLangkah[0]]);
                        }
                    }
                }
                if(cekLangkah.Count > 0 && bidakAIGerak.Count > 0 && listPathPosisi.Count > 0 && bidakBenderaPemain != null){
                    min = cekLangkah[0];
                    tempBidak = bidakAIGerak[0];
                    hasilPathPosisi = listPathPosisi[0];
                    //print(hasilPathPosisi);
                    for(int i = 1; i<cekLangkah.Count; i++){
                        if(cekLangkah[i] < min ){
                            min = cekLangkah[i];
                            tempBidak = bidakAIGerak[i];
                            hasilPathPosisi = listPathPosisi[i];
                        }
                    }
                    Target target = new Target(bidakBenderaPemain, tempBidak, min, hasilPathPosisi);
                    listTarget.Add(target);
                }
                bool moved = false;
                listTarget.Sort((s1 , s2) => s1.jumlahLangkah.CompareTo(s2.jumlahLangkah));
                if(listTarget.Count > 0){
                    print("Menarget Bendera");
                    foreach(var target in listTarget){
                        print("Bidak AI : "+target.bidakAI.GetComponent<Bidak>().nama);
                        print("Langkah Pertama Bidak AI : "+target.hasilPathPosisi);
                        print("Jumlah Langkah : "+target.jumlahLangkah);
                    }
                    foreach(var target in listTarget){
                        if(moved){
                            break;
                        }
                        int barisAwal = target.bidakAI.GetComponent<Bidak>().baris;
                        int kolomAwal = target.bidakAI.GetComponent<Bidak>().kolom;
                        int gerak = target.bidakAI.GetComponent<Bidak>().gerak;
                        int barisKe = target.hasilPathPosisi.GetComponent<Posisi>().barisPos;
                        int kolomKe = target.hasilPathPosisi.GetComponent<Posisi>().kolomPos;
                        if(Data.getKepemilikan(barisKe, kolomKe) == 'n' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                            print(target.hasilPathPosisi.GetComponent<Posisi>().tag);
                            target.bidakAI.transform.position = new Vector3(target.hasilPathPosisi.transform.position.x, target.hasilPathPosisi.transform.position.y, target.hasilPathPosisi.transform.position.z);
                            target.hasilPathPosisi.tag = "Lawan";
                            GameObject posisiAwal = Data.listPosisi[barisAwal, kolomAwal];
                            posisiAwal.tag = "Untagged";
                            Data.dataPangkatPindah(barisAwal, kolomAwal, barisKe, kolomKe, Data.getPangkat(barisAwal, kolomAwal));
                            Data.dataKepemilikanPindah(barisAwal, kolomAwal, barisKe, kolomKe, Data.getKepemilikan(barisAwal, kolomAwal));
                            Giliran.setGiliranPemain();
                            target.bidakAI.GetComponent<Bidak>().baris = target.hasilPathPosisi.GetComponent<Posisi>().barisPos;
                            target.bidakAI.GetComponent<Bidak>().kolom = target.hasilPathPosisi.GetComponent<Posisi>().kolomPos;
                            target.bidakAI.GetComponent<Bidak>().gerak = target.hasilPathPosisi.GetComponent<Posisi>().gerak;
                            timer = 2;
                            moved = true;
                            Data.WriteResponseAI("AI try to take down predicted bendera by "+target.bidakAI.GetComponent<Bidak>().nama+" with "+target.jumlahLangkah+" step");
                            Data.updatePosisiBidak();
                            
                            print("AI berusaha menjatuhkan bendera");
                        }else if(Data.getKepemilikan(barisKe, kolomKe) == 'l' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                            print(target.hasilPathPosisi.GetComponent<Posisi>().tag);
                            GameObject[] listPosisiGerak = target.hasilPathPosisi.GetComponent<Posisi>().ke;
                            foreach(var posisiGerak in listPosisiGerak){
                                int tempBaris = posisiGerak.GetComponent<Posisi>().barisPos;
                                int tempKolom = posisiGerak.GetComponent<Posisi>().kolomPos;
                                gerak = target.hasilPathPosisi.GetComponent<Posisi>().gerak;
                                if(Data.getKepemilikan(tempBaris, tempKolom) == 'n' ){
                                    GameObject bidakFirstPath = null;
                                    foreach(var bidak in listBidak){
                                        if(bidak != null && bidak.GetComponent<Bidak>().baris == barisKe && bidak.GetComponent<Bidak>().kolom == kolomKe){
                                            bidakFirstPath = bidak;
                                        }
                                    }
                                    if(bidakFirstPath != null && AturanGerak.opsiGerak(gerak, barisKe, kolomKe, tempBaris, tempKolom)){
                                        bidakFirstPath.transform.position = new Vector3(posisiGerak.transform.position.x, posisiGerak.transform.position.y, posisiGerak.transform.position.z);
                                        posisiGerak.tag = "Lawan";
                                        GameObject posisiAwal = Data.listPosisi[barisKe, kolomKe];
                                        posisiAwal.tag = "Untagged";
                                        Data.dataPangkatPindah(barisKe, kolomKe, tempBaris, tempKolom, Data.getPangkat(barisKe, kolomKe));
                                        Data.dataKepemilikanPindah(barisKe, kolomKe, tempBaris, tempKolom, Data.getKepemilikan(barisKe, kolomKe));
                                        Giliran.setGiliranPemain();
                                        bidakFirstPath.GetComponent<Bidak>().baris = posisiGerak.GetComponent<Posisi>().barisPos;
                                        bidakFirstPath.GetComponent<Bidak>().kolom = posisiGerak.GetComponent<Posisi>().kolomPos;
                                        bidakFirstPath.GetComponent<Bidak>().gerak = posisiGerak.GetComponent<Posisi>().gerak;
                                        timer = 2;
                                        moved = true;
                                        Data.WriteResponseAI("AI try to take down predicted bendera by "+target.bidakAI.GetComponent<Bidak>().nama+" with "+target.jumlahLangkah+" step");
                                        Data.updatePosisiBidak();
                                        
                                        print("AI berusaha menjatuhkan bendera");
                                    }
                                }
                            }
                        }else if(Data.getKepemilikan(barisKe, kolomKe) == 'p' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                            print(target.hasilPathPosisi.GetComponent<Posisi>().tag);
                            if((barisKe == TempatIstirahat.barisIstirahat[0] && kolomKe == TempatIstirahat.kolomIstirahat[0]) || 
                            (barisKe == TempatIstirahat.barisIstirahat[1] && kolomKe == TempatIstirahat.kolomIstirahat[1]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[2] && kolomKe == TempatIstirahat.kolomIstirahat[2]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[3] && kolomKe == TempatIstirahat.kolomIstirahat[3]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[4] && kolomKe == TempatIstirahat.kolomIstirahat[4]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[5] && kolomKe == TempatIstirahat.kolomIstirahat[5]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[6] && kolomKe == TempatIstirahat.kolomIstirahat[6]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[7] && kolomKe == TempatIstirahat.kolomIstirahat[7])){
                                print("Pemain di tempat istirahat");
                            }else{
                                string output = Data.bidakBertabrakan(barisAwal, kolomAwal, barisKe, kolomKe);
                                if(output == "menang"){
                                    target.bidakAI.transform.position = new Vector3(target.hasilPathPosisi.transform.position.x, target.hasilPathPosisi.transform.position.y, target.hasilPathPosisi.transform.position.z);
                                    target.hasilPathPosisi.tag = "Lawan";
                                    GameObject posisiAwal = Data.listPosisi[barisAwal, kolomAwal];
                                    posisiAwal.tag = "Untagged";
                                    target.bidakAI.GetComponent<Bidak>().baris = barisKe;
                                    target.bidakAI.GetComponent<Bidak>().kolom = kolomKe;
                                    target.bidakAI.GetComponent<Bidak>().gerak = gerak;
                                    Giliran.setGiliranLawan();
                                    Data.dataPangkatPindah(barisAwal, kolomAwal, barisKe, kolomKe, Data.getPangkat(barisAwal, kolomAwal));
                                    Data.dataKepemilikanPindah(barisAwal, kolomAwal, barisKe, kolomKe, Data.getKepemilikan(barisAwal, kolomAwal));
                                    foreach(var bidakPemain in listBidakPemain){
                                        if(bidakPemain.GetComponent<Bidak>().baris == barisKe && bidakPemain.GetComponent<Bidak>().kolom == kolomKe){
                                            Destroy(bidakPemain);
                                            break;
                                        }
                                    }
                                    timer = 2;
                                    moved = true;
                                    Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" and win");
                                    Data.updatePosisiBidak();
                                    
                                    print("AI menang bertabrakan dengan pemain");
                                }else if(output == "draw"){
                                    target.hasilPathPosisi.tag = "Untagged";
                                    GameObject posisiAwal = Data.listPosisi[barisAwal, kolomAwal];
                                    posisiAwal.tag = "Untagged";
                                    target.bidakAI.GetComponent<Bidak>().baris = barisKe;
                                    target.bidakAI.GetComponent<Bidak>().kolom = kolomKe;
                                    target.bidakAI.GetComponent<Bidak>().gerak = gerak;
                                    Data.zeroPangkat(barisAwal, kolomAwal);
                                    Data.zeroPangkat(barisKe, kolomKe);
                                    Data.noneKepemilikan(barisAwal, kolomAwal);
                                    Data.noneKepemilikan(barisKe, kolomKe);
                                    Giliran.setGiliranPemain();
                                    foreach(var bidakPemain in listBidakPemain){
                                        if(bidakPemain.GetComponent<Bidak>().baris == barisKe && bidakPemain.GetComponent<Bidak>().kolom == kolomKe){
                                            Destroy(bidakPemain);
                                            break;
                                        }
                                    }
                                    Destroy(target.bidakAI);
                                    timer = 2;
                                    moved = true;
                                    Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" and draw");
                                    Data.updatePosisiBidak();
                                    
                                    print("Draw");
                                }else if(output == "kalah"){
                                    foreach(var bidakPemain in listBidakPemain){
                                        if(bidakPemain.GetComponent<Bidak>().baris == barisKe && bidakPemain.GetComponent<Bidak>().kolom == kolomKe){
                                            AI.markBidak(target.bidakAI.GetComponent<Bidak>().pangkat, bidakPemain, barisKe, kolomKe);
                                            break;
                                        }
                                    }
                                    GameObject posisiAwal = Data.listPosisi[barisAwal, kolomAwal];
                                    posisiAwal.tag = "Untagged";
                                    target.bidakAI.GetComponent<Bidak>().baris = barisKe;
                                    target.bidakAI.GetComponent<Bidak>().kolom = kolomKe;
                                    target.bidakAI.GetComponent<Bidak>().gerak = gerak;
                                    Data.zeroPangkat(barisAwal, kolomAwal);
                                    Data.noneKepemilikan(barisAwal, kolomAwal);
                                    Giliran.setGiliranPemain();
                                    Destroy(target.bidakAI);
                                    timer = 2;
                                    moved = true;
                                    Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" and lose");
                                    Data.updatePosisiBidak();
                                    
                                    print("AI kalah bertabrakan dengan pemain");
                                }else if(output == "selesai"){
                                    print("GAME SELESAI");
                                    SceneManager.LoadScene("Main Menu");
                                }
                            }
                        }
                    }
                }
                if(moved){
                    strategy = 4;
                }else if(!moved){
                    Data.WriteResponseAI("Try another strategy than 4");
                    print("Random Move karena persyaratan tidak terpenuhi");
                    strategy = Random.Range(1,4);
                }
            }else{
                Data.WriteResponseAI("Try another strategy than 4");
                print("Random Move karena persyaratan tidak terpenuhi");
                strategy = Random.Range(1,4);
            }
        }
    }

    public int cekPath(GameObject posisiAI, GameObject posisiPemain, int langkah, GameObject posisiNextAI, GameObject bidakAI){
        //print(posisiAI.GetComponent<Posisi>().name);
        //bool ketemu = false;
        if(langkah == 1){
            posisiNextAI = posisiAI;
        }
        
        int barisAI = posisiAI.GetComponent<Posisi>().barisPos;
        int kolomAI = posisiAI.GetComponent<Posisi>().kolomPos;
        int barisPemain = posisiPemain.GetComponent<Posisi>().barisPos;
        int kolomPemain = posisiPemain.GetComponent<Posisi>().kolomPos;
        langkah+=counter;
        if(langkah >= 6 || jumlahLangkah.Count >= 10000){
            return langkah;
        }

        if(langkah > 0 && Data.getKepemilikan(barisAI, kolomAI) == 'l' && (Data.getPangkat(barisAI, kolomAI) == -1 || Data.getPangkat(barisAI, kolomAI) == 1)){
            return langkah;
        }
        
        if(barisAI == barisPemain && kolomAI == kolomPemain){
            if(tempPathPosisi.ContainsKey(langkah)){
                return langkah;
            }else{
                tempPathPosisi.Add(langkah, posisiNextAI);
                tempBidakAI.Add(langkah, bidakAI);
                jumlahLangkah.Add(langkah);
                return langkah;
            }
        }else if(barisAI == barisPemain && (barisAI == 1 || barisAI == 5 || barisAI == 8 || barisAI == 12)){
            if(tempPathPosisi.ContainsKey(langkah+1)){
                return langkah+1;
            }else{
                if(barisAI == barisPemain && (kolomAI - kolomPemain > 1 || kolomPemain - kolomAI > 1)){
                    bool hasil = AturanGerak.cekLompatKolom(kolomAI, kolomPemain, barisAI);
                    if(!hasil && kolomAI > kolomPemain){
                        for(int j = kolomPemain+1; j<kolomAI; j++){
                            if(Data.getKepemilikan(barisAI, j) == 'l' && (Data.getPangkat(barisAI, j) == -1 || Data.getPangkat(barisAI, j) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'l'){
                                langkah+=1;
                            }
                        }
                    }else if(!hasil && kolomAI < kolomPemain){
                        for(int j = kolomAI+1; j<kolomPemain; j++){
                            if(Data.getKepemilikan(barisAI, j) == 'l' && (Data.getPangkat(barisAI, j) == -1 || Data.getPangkat(barisAI, j) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'l'){
                                langkah+=1;
                            }
                        }
                    }
                }
                tempPathPosisi.Add(langkah+1, posisiNextAI);
                tempBidakAI.Add(langkah+1, bidakAI);
                jumlahLangkah.Add(langkah+1);
                return langkah+1;
            }
        }else if(kolomAI == kolomPemain && barisAI > 0 && barisAI < 7 && barisPemain > 0 && barisPemain < 7 && (kolomAI == 0 || kolomAI == 4)){
            if(tempPathPosisi.ContainsKey(langkah+1)){
                return langkah+1;
            }else{
                if(kolomAI == kolomPemain && (barisAI - barisPemain > 1 || barisPemain - barisAI > 1)){
                    bool hasil = AturanGerak.cekLompatBaris(barisAI, barisPemain, kolomAI);
                    if(!hasil && barisAI > barisPemain){
                        for(int j = barisPemain+1; j<barisAI; j++){
                            if(Data.getKepemilikan(j, kolomAI) == 'l' && (Data.getPangkat(j, kolomAI) == -1 || Data.getPangkat(j, kolomAI) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'l'){
                                langkah+=1;
                            }
                        }
                    }else if(!hasil && barisAI < barisPemain){
                        for(int j = barisAI+1; j<barisPemain; j++){
                            if(Data.getKepemilikan(j, kolomAI) == 'l' && (Data.getPangkat(j, kolomAI) == -1 || Data.getPangkat(j, kolomAI) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'l'){
                                langkah+=1;
                            }
                        }
                    }
                }
                tempPathPosisi.Add(langkah+1, posisiNextAI);
                tempBidakAI.Add(langkah+1, bidakAI);
                jumlahLangkah.Add(langkah+1);
                return langkah+1;
            }
        }else if(kolomAI == kolomPemain && barisAI > 6 && barisAI < 13 && barisPemain > 6 && barisPemain < 13 && (kolomAI == 0 || kolomAI == 4)){
            if(tempPathPosisi.ContainsKey(langkah+1)){
                return langkah+1;
            }else{
                if(kolomAI == kolomPemain && (barisAI - barisPemain > 1 || barisPemain - barisAI > 1)){
                    bool hasil = AturanGerak.cekLompatBaris(barisAI, barisPemain, kolomAI);
                    if(!hasil && barisAI > barisPemain){
                        for(int j = barisPemain+1; j<barisAI; j++){
                            if(Data.getKepemilikan(j, kolomAI) == 'l' && (Data.getPangkat(j, kolomAI) == -1 || Data.getPangkat(j, kolomAI) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'l'){
                                langkah+=1;
                            }
                        }
                    }else if(!hasil && barisAI < barisPemain){
                        for(int j = barisAI+1; j<barisPemain; j++){
                            if(Data.getKepemilikan(j, kolomAI) == 'l' && (Data.getPangkat(j, kolomAI) == -1 || Data.getPangkat(j, kolomAI) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'l'){
                                langkah+=1;
                            }
                        }
                    }
                }
                tempPathPosisi.Add(langkah+1, posisiNextAI);
                tempBidakAI.Add(langkah+1, bidakAI);
                jumlahLangkah.Add(langkah+1);
                return langkah+1;
            }
        }
        GameObject[] listKe = posisiAI.GetComponent<Posisi>().ke;
        int size = listKe.Length;
        for(int i=0; i<size; i++){
            if(barisPemain > barisAI){
                int barisKe = listKe[size-i-1].GetComponent<Posisi>().barisPos;
                int kolomKe = listKe[size-i-1].GetComponent<Posisi>().kolomPos;
                if(barisAI == barisKe && (kolomAI - kolomKe > 1 || kolomKe - kolomAI > 1)){
                    bool hasil = AturanGerak.cekLompatKolom(kolomAI, kolomKe, barisAI);
                    if(!hasil && kolomAI > kolomKe){
                        for(int j = kolomKe+1; j<kolomAI; j++){
                            if(Data.getKepemilikan(barisAI, j) == 'l' && (Data.getPangkat(barisAI, j) == -1 || Data.getPangkat(barisAI, j) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'l'){
                                counter+=1;
                            }
                        }
                    }else if(!hasil && kolomAI < kolomKe){
                        for(int j = kolomAI+1; j<kolomKe; j++){
                            if(Data.getKepemilikan(barisAI, j) == 'l' && (Data.getPangkat(barisAI, j) == -1 || Data.getPangkat(barisAI, j) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'l'){
                                counter+=1;
                            }
                        }
                    }
                }else if(kolomAI == kolomKe && (barisAI - barisKe > 1 || barisKe - barisAI > 1)){
                    bool hasil = AturanGerak.cekLompatBaris(barisAI, barisKe, kolomAI);
                    if(!hasil && barisAI > barisKe){
                        for(int j = barisKe+1; j<barisAI; j++){
                            if(Data.getKepemilikan(j, kolomAI) == 'l' && (Data.getPangkat(j, kolomAI) == -1 || Data.getPangkat(j, kolomAI) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'l'){
                                counter+=1;
                            }
                        }
                    }else if(!hasil && barisAI < barisKe){
                        for(int j = barisAI+1; j<barisKe; j++){
                            if(Data.getKepemilikan(j, kolomAI) == 'l' && (Data.getPangkat(j, kolomAI) == -1 || Data.getPangkat(j, kolomAI) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'l'){
                                counter+=1;
                            }
                        }
                    }
                }else if(Data.getKepemilikan(barisKe, kolomKe) == 'l'){
                    counter+=1;
                }
                cekPath(listKe[size-i-1], posisiPemain, langkah+1, posisiNextAI, bidakAI);
            }else{
                int barisKe = listKe[i].GetComponent<Posisi>().barisPos;
                int kolomKe = listKe[i].GetComponent<Posisi>().kolomPos;
                if(barisAI == barisKe && (kolomAI - kolomKe > 1 || kolomKe - kolomAI > 1)){
                    bool hasil = AturanGerak.cekLompatKolom(kolomAI, kolomKe, barisAI);
                    if(!hasil && kolomAI > kolomKe){
                        for(int j = kolomKe+1; j<kolomAI; j++){
                            if(Data.getKepemilikan(barisAI, j) == 'l' && (Data.getPangkat(barisAI, j) == -1 || Data.getPangkat(barisAI, j) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'l'){
                                counter+=1;
                            }
                        }
                    }else if(!hasil && kolomAI < kolomKe){
                        for(int j = kolomAI+1; j<kolomKe; j++){
                            if(Data.getKepemilikan(barisAI, j) == 'l' && (Data.getPangkat(barisAI, j) == -1 || Data.getPangkat(barisAI, j) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(barisAI, j) == 'l'){
                                counter+=1;
                            }
                        }
                    }
                }else if(kolomAI == kolomKe && (barisAI - barisKe > 1 || barisKe - barisAI > 1)){
                    bool hasil = AturanGerak.cekLompatBaris(barisAI, barisKe, kolomAI);
                    if(!hasil && barisAI > barisKe){
                        for(int j = barisKe+1; j<barisAI; j++){
                            if(Data.getKepemilikan(j, kolomAI) == 'l' && (Data.getPangkat(j, kolomAI) == -1 || Data.getPangkat(j, kolomAI) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'l'){
                                counter+=1;
                            }
                        }
                    }else if(!hasil && barisAI < barisKe){
                        for(int j = barisAI+1; j<barisKe; j++){
                            if(Data.getKepemilikan(j, kolomAI) == 'l' && (Data.getPangkat(j, kolomAI) == -1 || Data.getPangkat(j, kolomAI) == 1)){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'p'){
                                return langkah;
                            }else if(Data.getKepemilikan(j, kolomAI) == 'l'){
                                counter+=1;
                            }
                        }
                    }
                }else if(Data.getKepemilikan(barisKe, kolomKe) == 'l'){
                    counter+=1;
                }
                cekPath(listKe[i], posisiPemain, langkah+1, posisiNextAI, bidakAI);
            }
        }
        return jumlahLangkah.Count;
    }

    public static void markBidak(int pangkatAI, GameObject bidakPemain, int barisPosAI, int kolomPosAI){
        string predictionNamaPemain = bidakPemain.GetComponentInChildren<Text>().text;
        int predictionPangkatPemain = 0;
        if(predictionNamaPemain == " "){
            if(pangkatAI == -1){
                predictionPangkatPemain = 2;
            }else if(pangkatAI > 0){
                predictionPangkatPemain = pangkatAI + 1;
            }
            string pangkat = Data.namaPangkat(predictionPangkatPemain);
            bidakPemain.GetComponentInChildren<Text>().text = pangkat;
        }else{
            predictionPangkatPemain = Data.nilaiPangkat(predictionNamaPemain);
            if(pangkatAI == -1){
                predictionPangkatPemain = 2;
            }else if(pangkatAI >= predictionPangkatPemain){
                predictionPangkatPemain = pangkatAI + 1;
            }
            string pangkat = Data.namaPangkat(predictionPangkatPemain);
            bidakPemain.GetComponentInChildren<Text>().text = pangkat;
        }
        strategy = 3;
        //Cases kasus = new Cases(pangkatAI, predictionPangkatPemain, barisPosAI, kolomPosAI);
        //listKasus.Add(kasus);
    }

    public static void similarity(){
        int pemainMemasukiWilayahAI=0;
        int jalurKeretaKolomAIKosong=0;
        int AIMemasukiWilayahPemain=0;
        int jalurKeretaKolomPemainKosong=0;
        int tempatIstirahatAIKosong=0;
        int tempatIstirahatPemainKosong=0;
        int bidakTelahDiperkirakanPangkatnya=0;
        int benderaPemainDiketahui=0;
        char[,] dataKepemilikanBidak = Data.dataKepemilikanBidak[Data.dataKepemilikanBidak.Count - 1];
        GameObject[] listBidakPemain = GameObject.FindGameObjectsWithTag("Pemain");
        int jumlahPenghalangAI0 = 0;
        int jumlahPenghalangAI4 = 0;
        int jumlahPenghalangPemain0 = 0;
        int jumlahPenghalangPemain4 = 0;
        if(benderaExposed){
            benderaPemainDiketahui = 1;
        }
        for(int i = 0; i < 14; i++){
            for(int j = 0; j < 5; j++){
                if((i == 2 && j == 1) || (i == 2 && j == 3) || (i == 4 && j == 1) || (i == 4 && j == 3)){
                    if(dataKepemilikanBidak[i, j] == 'n'){
                        tempatIstirahatAIKosong = 1;
                    }
                }
                if(i < 7){
                    if(dataKepemilikanBidak[i,j] == 'p'){
                        pemainMemasukiWilayahAI = 1;
                    }
                    if(j == 0 || j == 4){
                        if(dataKepemilikanBidak[i, j] == 'l' && j == 0){
                            jumlahPenghalangAI0+=1;
                        }
                        if(dataKepemilikanBidak[i, j] == 'l' && j == 4){
                            jumlahPenghalangAI4+=1;
                        }
                        if(jumlahPenghalangAI0 < 2 && jumlahPenghalangAI4 < 2){
                            jalurKeretaKolomAIKosong = 1;
                        }
                    }
                }
                if((i == 2 && j == 1) || (i == 2 && j == 3) || (i == 4 && j == 1) || (i == 4 && j == 3)){
                    if(dataKepemilikanBidak[i, j] == 'n'){
                        tempatIstirahatPemainKosong = 1;
                    }
                }
                if(i >= 7){
                    if(dataKepemilikanBidak[i,j] == 'l'){
                        AIMemasukiWilayahPemain = 1;
                    }
                    if(j == 0 || j == 4){
                        if(dataKepemilikanBidak[i, j] == 'p' && j == 0){
                            jumlahPenghalangPemain0+=1;
                        }
                        if(dataKepemilikanBidak[i, j] == 'p' && j == 4){
                            jumlahPenghalangPemain4+=1;
                        }
                        if(jumlahPenghalangPemain0 < 2 && jumlahPenghalangPemain4 < 2){
                            jalurKeretaKolomPemainKosong = 1;
                        }
                    }
                }
            }
        }
        foreach(var bidakPemain in listBidakPemain){
            if(bidakPemain != null && bidakPemain.GetComponent<Bidak>() && bidakPemain.GetComponentInChildren<Text>()){
                string predictionNamaPemain = bidakPemain.GetComponentInChildren<Text>().text;
                if(predictionNamaPemain != " "){
                    bidakTelahDiperkirakanPangkatnya = 1;
                }
            }
        }
    }
}

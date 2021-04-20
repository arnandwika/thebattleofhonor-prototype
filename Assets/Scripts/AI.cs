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
            }
        }
    }

    public void randomMovement(){
        giliranPemain = Giliran.getGiliran();
        if(strategy == 1 && !giliranPemain){
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
                        if(loop >5){
                            break;
                        }
                        if(bidak.GetComponent<Bidak>().baris < 7){
                            barisPosTujuan = Random.Range(bidak.GetComponent<Bidak>().baris, 8);
                            kolomPosTujuan = Random.Range(0, 5);
                        }else{
                            barisPosTujuan = Random.Range(bidak.GetComponent<Bidak>().baris, 14);
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
                                        strategy = Random.Range(1,3);
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
            listTarget = new List<Target>();
            GameObject[] listBidakPemain = GameObject.FindGameObjectsWithTag("Pemain");
            foreach(var bidakPemain in listBidakPemain){
                if(bidakPemain != null && bidakPemain.GetComponentInChildren<Text>()){
                    min=0;
                    cekLangkah = new List<int>();
                    listPathPosisi = new List<GameObject>();
                    bidakAIGerak = new List<GameObject>();
                    hasilPathPosisi = new GameObject();
                    string predictionNamaPemain = bidakPemain.GetComponentInChildren<Text>().text;
                    int predictionPangkatPemain = 0;
                    if(predictionNamaPemain == ""){
                        continue;
                    }else{
                        predictionPangkatPemain = Data.nilaiPangkat(predictionNamaPemain);
                        foreach(var bidakAI in listBidak){
                            if(bidakAI != null){
                                if(bidakAI.GetComponent<Bidak>().pangkat > predictionPangkatPemain){
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
                                    jumlahLangkah.Sort();
                                    //print(hasil);
                                    //print(predictionNamaPemain);
                                    //print(bidakAI.GetComponent<Bidak>().nama);
                                    //print(jumlahLangkah[0]);
                                    bidakAIGerak.Add(tempBidakAI[jumlahLangkah[0]]);
                                    cekLangkah.Add(jumlahLangkah[0]);
                                    listPathPosisi.Add(tempPathPosisi[jumlahLangkah[0]]);
                                }
                            }
                        }
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
                    
                }else{
                    continue;
                }
            }
            strategy = 2;
            print("Jumlah target : "+listTarget.Count);
            foreach(var target in listTarget){
                print("Bidak AI :"+target.bidakAI);
                print("Perkiraan Bidak Pemain :"+target.bidakPemain.GetComponentInChildren<Text>().text);
                print("Langkah Pertama Bidak AI :"+target.hasilPathPosisi);
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
        if(langkah >= 6 || jumlahLangkah.Count >= 10000){
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
                tempPathPosisi.Add(langkah+1, posisiNextAI);
                tempBidakAI.Add(langkah+1, bidakAI);
                jumlahLangkah.Add(langkah+1);
                return langkah+1;
            }
        }else if(kolomAI == kolomPemain && barisAI > 0 && barisAI < 7 && barisPemain > 0 && barisPemain < 7 && (kolomAI == 0 || kolomAI == 4)){
            if(tempPathPosisi.ContainsKey(langkah+1)){
                return langkah+1;
            }else{
                tempPathPosisi.Add(langkah+1, posisiNextAI);
                tempBidakAI.Add(langkah+1, bidakAI);
                jumlahLangkah.Add(langkah+1);
                return langkah+1;
            }
        }else if(kolomAI == kolomPemain && barisAI > 6 && barisAI < 13 && barisPemain > 6 && barisPemain < 13 && (kolomAI == 0 || kolomAI == 4)){
            if(tempPathPosisi.ContainsKey(langkah+1)){
                return langkah+1;
            }else{
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
                cekPath(listKe[size-i-1], posisiPemain, langkah+1, posisiNextAI, bidakAI);
            }else{
                cekPath(listKe[i], posisiPemain, langkah+1, posisiNextAI, bidakAI);
            }
        }
        return jumlahLangkah.Count;
    }

    public static void markBidak(int pangkatAI, GameObject bidakPemain, int barisPosAI, int kolomPosAI){
        string predictionNamaPemain = bidakPemain.GetComponentInChildren<Text>().text;
        int predictionPangkatPemain = 0;
        if(predictionNamaPemain == ""){
            if(pangkatAI == -2){
                predictionPangkatPemain = 2;
            }else if(pangkatAI > 0){
                predictionPangkatPemain = pangkatAI + 1;
            }
            string pangkat = Data.namaPangkat(predictionPangkatPemain);
            bidakPemain.GetComponentInChildren<Text>().text = pangkat;
        }else{
            predictionPangkatPemain = Data.nilaiPangkat(predictionNamaPemain);
            if(pangkatAI == -2){
                predictionPangkatPemain = 2;
            }else if(pangkatAI > predictionPangkatPemain){
                predictionPangkatPemain = pangkatAI + 1;
            }
            string pangkat = Data.namaPangkat(predictionPangkatPemain);
            bidakPemain.GetComponentInChildren<Text>().text = pangkat;
        }
        strategy = 3;
        //Cases kasus = new Cases(pangkatAI, predictionPangkatPemain, barisPosAI, kolomPosAI);
        //listKasus.Add(kasus);
    }
}

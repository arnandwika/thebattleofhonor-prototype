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
    private char tempatIstirahat1;
    private char tempatIstirahat2;
    private char tempatIstirahat3;
    private char tempatIstirahat4;
    private bool giliranPemain = true;
    private int barisPosAwal;
    private int kolomPosAwal;
    private int barisPosTujuan;
    private int kolomPosTujuan;
    private int strategy; //1 bertahan, 2 menyerang
    public GameObject[] listBidak;
    private float timer = 2;
    private int gerak;
    private static List<Cases> listKasus = new List<Cases>();
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
        Cases kasus = new Cases(pangkatAI, predictionPangkatPemain, barisPosAI, kolomPosAI);
        listKasus.Add(kasus);
    }
}

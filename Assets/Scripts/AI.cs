using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    public struct Cases
    {
        public int pemainMemasukiWilayahAI;
        public int jalurKeretaKolomAIKosong;
        public int AIMemasukiWilayahPemain;
        public int jalurKeretaKolomPemainKosong;
        public int tempatIstirahatAIKosong;
        public int tempatIstirahatPemainKosong;
        public int bidakTelahDiperkirakanPangkatnya;
        public int bidakTelahDiperkirakanPangkatnyaDiIstirahat;
        public int benderaPemainDiketahui;
        public int solusiStrategi;

        public Cases(int pemainMemasukiWilayahAI, int jalurKeretaKolomAIKosong, int AIMemasukiWilayahPemain, int jalurKeretaKolomPemainKosong, int tempatIstirahatAIKosong, int tempatIstirahatPemainKosong, int bidakTelahDiperkirakanPangkatnya, int bidakTelahDiperkirakanPangkatnyaDiIstirahat, int benderaPemainDiketahui, int solusiStrategi){
            this.pemainMemasukiWilayahAI = pemainMemasukiWilayahAI;
            this.jalurKeretaKolomAIKosong = jalurKeretaKolomAIKosong;
            this.AIMemasukiWilayahPemain = AIMemasukiWilayahPemain;
            this.jalurKeretaKolomPemainKosong = jalurKeretaKolomPemainKosong;
            this.tempatIstirahatAIKosong = tempatIstirahatAIKosong;
            this.tempatIstirahatPemainKosong = tempatIstirahatPemainKosong;
            this.bidakTelahDiperkirakanPangkatnya = bidakTelahDiperkirakanPangkatnya;
            this.benderaPemainDiketahui = benderaPemainDiketahui;
            this.solusiStrategi = solusiStrategi;
            this.bidakTelahDiperkirakanPangkatnyaDiIstirahat = bidakTelahDiperkirakanPangkatnyaDiIstirahat;
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
    private float timer = 3;
    private int gerak;
    public static List<Cases> listKasus = new List<Cases>();
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
    public static bool cekInsertKasus = true;
    public static Cases lastCase;
    private static bool cekFalseSolution = false;
    public static int failedSolution = 0;
    private static int cekDiTempatIstirahat = 0;
    private static int cekBisaBergerak = 0;
    // Start is called before the first frame update
    void Start()
    {
        listBidak = GameObject.FindGameObjectsWithTag("Lawan");
        giliranPemain = Giliran.getGiliran();
        Cases kasus1_strategy1 = new Cases(0, 0, 0, 0, 1, 1, 0, 0, 0, 1);
        Cases kasus2_strategy1 = new Cases(1, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        Cases kasus3_strategy1 = new Cases(0, 0, 0, 0, 1, 0, 1, 1, 0, 1);
        Cases kasus1_strategy2 = new Cases(0, 0, 0, 0, 0, 1, 1, 1, 0, 2);
        Cases kasus2_strategy2 = new Cases(1, 0, 1, 0, 0, 0, 0, 0, 0, 2);
        Cases kasus3_strategy2 = new Cases(1, 0, 0, 1, 0, 1, 1, 1, 0, 2);
        Cases kasus1_strategy3 = new Cases(1, 1, 0, 0, 1, 0, 1, 0, 0, 3);
        Cases kasus2_strategy3 = new Cases(0, 0, 1, 0, 0, 1, 2, 1, 0, 3);
        Cases kasus3_strategy3 = new Cases(1, 1, 1, 0, 0, 0, 1, 1, 0, 3);
        Cases kasus1_strategy4 = new Cases(0, 0, 1, 1, 0, 0, 1, 1, 1, 4);
        Cases kasus2_strategy4 = new Cases(1, 0, 0, 1, 0, 1, 0, 0, 1, 4);
        Cases kasus3_strategy4 = new Cases(1, 1, 1, 1, 1, 0, 2, 2, 1, 4);
        listKasus.Add(kasus1_strategy1);
        listKasus.Add(kasus2_strategy1);
        listKasus.Add(kasus3_strategy1);
        listKasus.Add(kasus1_strategy2);
        listKasus.Add(kasus2_strategy2);
        listKasus.Add(kasus3_strategy2);
        listKasus.Add(kasus1_strategy3);
        listKasus.Add(kasus2_strategy3);
        listKasus.Add(kasus3_strategy3);
        listKasus.Add(kasus1_strategy4);
        listKasus.Add(kasus2_strategy4);
        listKasus.Add(kasus3_strategy4);
        foreach(var kasus in listKasus){
            Data.WriteCase(kasus.pemainMemasukiWilayahAI+","+kasus.jalurKeretaKolomAIKosong+","+kasus.AIMemasukiWilayahPemain+","+kasus.jalurKeretaKolomPemainKosong+","+kasus.tempatIstirahatAIKosong+","+kasus.tempatIstirahatPemainKosong+","+kasus.bidakTelahDiperkirakanPangkatnya+","+kasus.benderaPemainDiketahui+","+kasus.solusiStrategi);
        }
    }

    // Update is called once per frame
    void Update()
    {
        giliranPemain = Giliran.getGiliran();
        if(!giliranPemain){
            if(timer > 0){
                timer -= Time.deltaTime;
            }else{
                if(!cekInsertKasus){
                    newCase();
                    cekInsertKasus = true;
                    cekFalseSolution = false;
                }
                randomMovement();
                timer -= Time.deltaTime;
                if(timer < -8){
                    Data.WriteResponseAI("Error AI think too long");
                    strategy = Random.Range(1,4);
                }
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
                                print("AI move from ("+bidak.GetComponent<Bidak>().baris+","+bidak.GetComponent<Bidak>().kolom+") to (4,3)");
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
                                timer = 3;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                lastCase.solusiStrategi = 1;
                                listKasus.Add(lastCase);
                                Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
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
                                print("AI move from ("+bidak.GetComponent<Bidak>().baris+","+bidak.GetComponent<Bidak>().kolom+") to (4,1)");
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
                                timer = 3;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                lastCase.solusiStrategi = 1;
                                listKasus.Add(lastCase);
                                Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
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
                                print("AI move from ("+bidak.GetComponent<Bidak>().baris+","+bidak.GetComponent<Bidak>().kolom+") to (4,3)");
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
                                timer = 3;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                lastCase.solusiStrategi = 1;
                                listKasus.Add(lastCase);
                                Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                            }
                        }
                    }
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
                                print("AI move from ("+bidak.GetComponent<Bidak>().baris+","+bidak.GetComponent<Bidak>().kolom+") to (2,3)");
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
                                timer = 3;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                lastCase.solusiStrategi = 1;
                                listKasus.Add(lastCase);
                                Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
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
                                print("AI move from ("+bidak.GetComponent<Bidak>().baris+","+bidak.GetComponent<Bidak>().kolom+") to (2,1)");
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
                                timer = 3;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                lastCase.solusiStrategi = 1;
                                listKasus.Add(lastCase);
                                Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
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
                                print("AI move from ("+bidak.GetComponent<Bidak>().baris+","+bidak.GetComponent<Bidak>().kolom+") to (2,3)");
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
                                timer = 3;
                                Data.WriteResponseAI("AI filling their Tempat Istirahat");
                                Data.updatePosisiBidak();
                                lastCase.solusiStrategi = 1;
                                listKasus.Add(lastCase);
                                Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                            }
                        }
                    }
                }
            }else{
                Data.WriteResponseAI("Try another strategy than 1");
                if(!cekFalseSolution){
                    failedSolution+=1;
                    cekFalseSolution = true;
                    Data.WriteResponseAI("Total false solution :"+failedSolution);
                }
                strategy = Random.Range(2,4);
            }
        }
        if(strategy == 2 && !giliranPemain){
            int indeks = Random.Range(0, listBidak.Length);
            GameObject bidak = listBidak[indeks];
            if(bidak != null){
                barisPosAwal = bidak.GetComponent<Bidak>().baris;
                kolomPosAwal = bidak.GetComponent<Bidak>().kolom;
                if((barisPosAwal == TempatIstirahat.barisIstirahat[0] && barisPosAwal == TempatIstirahat.kolomIstirahat[0]) || 
                (barisPosAwal == TempatIstirahat.barisIstirahat[1] && barisPosAwal == TempatIstirahat.kolomIstirahat[1]) ||
                (barisPosAwal == TempatIstirahat.barisIstirahat[2] && barisPosAwal == TempatIstirahat.kolomIstirahat[2]) ||
                (barisPosAwal == TempatIstirahat.barisIstirahat[3] && barisPosAwal == TempatIstirahat.kolomIstirahat[3]) ||
                (barisPosAwal == TempatIstirahat.barisIstirahat[4] && barisPosAwal == TempatIstirahat.kolomIstirahat[4]) ||
                (barisPosAwal == TempatIstirahat.barisIstirahat[5] && barisPosAwal == TempatIstirahat.kolomIstirahat[5]) ||
                (barisPosAwal == TempatIstirahat.barisIstirahat[6] && barisPosAwal == TempatIstirahat.kolomIstirahat[6]) ||
                (barisPosAwal == TempatIstirahat.barisIstirahat[7] && barisPosAwal == TempatIstirahat.kolomIstirahat[7])){
                    cekDiTempatIstirahat += 1;
                    if(cekDiTempatIstirahat > 20){
                        Data.WriteResponseAI("Try another strategy than 2");
                        if(!cekFalseSolution){
                            failedSolution+=1;
                            cekFalseSolution = true;
                            Data.WriteResponseAI("Total false solution :"+failedSolution);
                        }
                        cekDiTempatIstirahat = 0;
                        strategy = Random.Range(3,5);
                    }
                }else{
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
                                        print("AI move from ("+barisPosAwal+","+kolomPosAwal+") to ("+barisPosTujuan+","+kolomPosTujuan+")");
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
                                        timer = 3;
                                        hasil = true;
                                        strategy = Random.Range(1,3);
                                        Data.WriteResponseAI("AI "+bidak.GetComponent<Bidak>().nama+" moving forward to ("+posisi.GetComponent<Posisi>().barisPos+","+posisi.GetComponent<Posisi>().kolomPos+")");
                                        Data.updatePosisiBidak();
                                        lastCase.solusiStrategi = 2;
                                        listKasus.Add(lastCase);
                                        Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                                        
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
                                            print("AI move from ("+barisPosAwal+","+kolomPosAwal+") to ("+barisPosTujuan+","+kolomPosTujuan+")");
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
                                            timer = 3;
                                            hasil = true;
                                            strategy = Random.Range(1,3);
                                            Data.WriteResponseAI("AI "+bidak.GetComponent<Bidak>().nama+" moving forward to ("+posisi.GetComponent<Posisi>().barisPos+","+posisi.GetComponent<Posisi>().kolomPos+") and win");
                                            Data.updatePosisiBidak();
                                            lastCase.solusiStrategi = 2;
                                            listKasus.Add(lastCase);
                                            Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                                        }else if(output == "draw"){
                                            print("AI move from ("+barisPosAwal+","+kolomPosAwal+") to ("+barisPosTujuan+","+kolomPosTujuan+")");
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
                                            timer = 3;
                                            hasil = true;
                                            strategy = Random.Range(1,3);
                                            Data.WriteResponseAI("AI "+bidak.GetComponent<Bidak>().nama+" moving forward to ("+posisi.GetComponent<Posisi>().barisPos+","+posisi.GetComponent<Posisi>().kolomPos+") and draw");
                                            Data.updatePosisiBidak();
                                            lastCase.solusiStrategi = 2;
                                            listKasus.Add(lastCase);
                                            Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                                        }else if(output == "kalah"){
                                            print("AI move from ("+barisPosAwal+","+kolomPosAwal+") to ("+barisPosTujuan+","+kolomPosTujuan+")");
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
                                            timer = 3;
                                            hasil = true;
                                            Data.WriteResponseAI("AI "+bidak.GetComponent<Bidak>().nama+" moving forward to ("+posisi.GetComponent<Posisi>().barisPos+","+posisi.GetComponent<Posisi>().kolomPos+") and lose");
                                            Data.updatePosisiBidak();
                                            lastCase.solusiStrategi = 2;
                                            listKasus.Add(lastCase);
                                            Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                                        }else if(output == "selesai"){
                                            print("GAME SELESAI");
                                            double akurasi = AI.listKasus.Count - AI.failedSolution;
                                            akurasi *= 100;
                                            akurasi /= AI.listKasus.Count;
                                            Data.WriteCase("Accuration : "+akurasi+"%");
                                            SceneManager.LoadScene("Main Menu");
                                        }
                                    }
                                }
                            }
                            loop++;
                        }
                    }else{
                        cekBisaBergerak += 1;
                        if(cekBisaBergerak > 20){
                            Data.WriteResponseAI("Try another strategy than 2");
                            if(!cekFalseSolution){
                                failedSolution+=1;
                                cekFalseSolution = true;
                                Data.WriteResponseAI("Total false solution :"+failedSolution);
                            }
                            cekBisaBergerak = 0;
                            strategy = Random.Range(3,5);
                        }
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
                                        Data.WriteResponseAI("Target on Tempat Istirahat");
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
                // print("Jumlah target : "+listTarget.Count);
                // foreach(var target in listTarget){
                //     print("Bidak AI : "+target.bidakAI.GetComponent<Bidak>().nama);
                //     print("Perkiraan Bidak Pemain : "+target.bidakPemain.GetComponentInChildren<Text>().text);
                //     print("Langkah Pertama Bidak AI : "+target.hasilPathPosisi);
                //     print("Jumlah Langkah : "+target.jumlahLangkah);
                // }
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
                        print("AI move from ("+barisAwal+","+kolomAwal+") to ("+barisKe+","+kolomKe+")");
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
                        timer = 3;
                        moved = true;
                        Data.WriteResponseAI("AI try to take down predicted "+target.bidakPemain.GetComponentInChildren<Text>().text+" by "+target.bidakAI.GetComponent<Bidak>().nama+" with "+target.jumlahLangkah+" step");
                        Data.updatePosisiBidak();
                        lastCase.solusiStrategi = 3;
                        listKasus.Add(lastCase);
                        Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                    }else if(Data.getKepemilikan(barisKe, kolomKe) == 'l' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                        
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
                                    print("AI move from ("+barisKe+","+kolomKe+") to ("+tempBaris+","+tempKolom+")");
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
                                    timer = 3;
                                    moved = true;
                                    Data.WriteResponseAI("AI try to take down predicted "+target.bidakPemain.GetComponentInChildren<Text>().text+" by "+target.bidakAI.GetComponent<Bidak>().nama+" with "+target.jumlahLangkah+" step");
                                    Data.updatePosisiBidak();
                                    lastCase.solusiStrategi = 3;
                                    listKasus.Add(lastCase);
                                    Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                                    
                                }
                            }
                        }
                    }else if(Data.getKepemilikan(barisKe, kolomKe) == 'p' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                        
                        if((barisKe == TempatIstirahat.barisIstirahat[0] && kolomKe == TempatIstirahat.kolomIstirahat[0]) || 
                        (barisKe == TempatIstirahat.barisIstirahat[1] && kolomKe == TempatIstirahat.kolomIstirahat[1]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[2] && kolomKe == TempatIstirahat.kolomIstirahat[2]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[3] && kolomKe == TempatIstirahat.kolomIstirahat[3]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[4] && kolomKe == TempatIstirahat.kolomIstirahat[4]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[5] && kolomKe == TempatIstirahat.kolomIstirahat[5]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[6] && kolomKe == TempatIstirahat.kolomIstirahat[6]) ||
                        (barisKe == TempatIstirahat.barisIstirahat[7] && kolomKe == TempatIstirahat.kolomIstirahat[7])){
                            Data.WriteResponseAI("Targeted bidak in Tempat Istirahat");
                        }else{
                            string output = Data.bidakBertabrakan(barisAwal, kolomAwal, barisKe, kolomKe);
                            if(output == "menang"){
                                print("AI move from ("+barisAwal+","+kolomAwal+") to ("+barisKe+","+kolomKe+")");
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
                                timer = 3;
                                moved = true;
                                Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" and win");
                                Data.updatePosisiBidak();
                                lastCase.solusiStrategi = 3;
                                listKasus.Add(lastCase);
                                Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                            }else if(output == "draw"){
                                print("AI move from ("+barisAwal+","+kolomAwal+") to ("+barisKe+","+kolomKe+")");
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
                                timer = 3;
                                moved = true;
                                Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" draw");
                                Data.updatePosisiBidak();
                                lastCase.solusiStrategi = 3;
                                listKasus.Add(lastCase);
                                Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                            }else if(output == "kalah"){
                                print("AI move from ("+barisAwal+","+kolomAwal+") to ("+barisKe+","+kolomKe+")");
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
                                timer = 3;
                                moved = true;
                                Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" lose");
                                Data.updatePosisiBidak();
                                lastCase.solusiStrategi = 3;
                                listKasus.Add(lastCase);
                                Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                            }else if(output == "selesai"){
                                print("GAME SELESAI");
                                double akurasi = AI.listKasus.Count - AI.failedSolution;
                                akurasi *= 100;
                                akurasi /= AI.listKasus.Count;
                                Data.WriteCase("Accuration : "+akurasi+"%");
                                SceneManager.LoadScene("Main Menu");
                            }
                        }
                    }
                }
            }
            if(!moved){
                Data.WriteResponseAI("Try another strategy than 3");
                if(!cekFalseSolution){
                    failedSolution+=1;
                    cekFalseSolution = true;
                    Data.WriteResponseAI("Total false solution :"+failedSolution);
                }
                strategy = Random.Range(1,3);
            }
        }
        if(strategy == 4 && !giliranPemain){
            List <GameObject> listBidakAIDepan = new List<GameObject>();
            GameObject[] listBidakPemain = GameObject.FindGameObjectsWithTag("Pemain");
            GameObject bidakBenderaPemain = null;
            int barisPosBendera = 0;
            int kolomPosBendera = 0;
            if(!benderaExposed){
                Data.WriteResponseAI("Bendera not exposed yet, randoming guess");
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
                    // print("Menarget Bendera");
                    // foreach(var target in listTarget){
                    //     print("Bidak AI : "+target.bidakAI.GetComponent<Bidak>().nama);
                    //     print("Langkah Pertama Bidak AI : "+target.hasilPathPosisi);
                    //     print("Jumlah Langkah : "+target.jumlahLangkah);
                    // }
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
                            print("AI move from ("+barisAwal+","+kolomAwal+") to ("+barisKe+","+kolomKe+")");
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
                            timer = 3;
                            moved = true;
                            Data.WriteResponseAI("AI try to take down predicted bendera by "+target.bidakAI.GetComponent<Bidak>().nama+" with "+target.jumlahLangkah+" step");
                            Data.updatePosisiBidak();
                            lastCase.solusiStrategi = 4;
                            listKasus.Add(lastCase);
                            Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                            print("AI berusaha menjatuhkan bendera");
                        }else if(Data.getKepemilikan(barisKe, kolomKe) == 'l' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                            
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
                                        print("AI move from ("+barisKe+","+kolomKe+") to ("+tempBaris+","+tempKolom+")");
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
                                        timer = 3;
                                        moved = true;
                                        Data.WriteResponseAI("AI try to take down predicted bendera by "+target.bidakAI.GetComponent<Bidak>().nama+" with "+target.jumlahLangkah+" step");
                                        Data.updatePosisiBidak();
                                        lastCase.solusiStrategi = 4;
                                        listKasus.Add(lastCase);
                                        Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                                    }
                                }
                            }
                        }else if(Data.getKepemilikan(barisKe, kolomKe) == 'p' && AturanGerak.opsiGerak(gerak, barisAwal, kolomAwal, barisKe, kolomKe)){
                            
                            if((barisKe == TempatIstirahat.barisIstirahat[0] && kolomKe == TempatIstirahat.kolomIstirahat[0]) || 
                            (barisKe == TempatIstirahat.barisIstirahat[1] && kolomKe == TempatIstirahat.kolomIstirahat[1]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[2] && kolomKe == TempatIstirahat.kolomIstirahat[2]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[3] && kolomKe == TempatIstirahat.kolomIstirahat[3]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[4] && kolomKe == TempatIstirahat.kolomIstirahat[4]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[5] && kolomKe == TempatIstirahat.kolomIstirahat[5]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[6] && kolomKe == TempatIstirahat.kolomIstirahat[6]) ||
                            (barisKe == TempatIstirahat.barisIstirahat[7] && kolomKe == TempatIstirahat.kolomIstirahat[7])){
                                //print("Pemain di tempat istirahat");
                            }else{
                                string output = Data.bidakBertabrakan(barisAwal, kolomAwal, barisKe, kolomKe);
                                if(output == "menang"){
                                    print("AI move from ("+barisAwal+","+kolomAwal+") to ("+barisKe+","+kolomKe+")");
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
                                    timer = 3;
                                    moved = true;
                                    Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" and win");
                                    Data.updatePosisiBidak();
                                    lastCase.solusiStrategi = 4;
                                    listKasus.Add(lastCase);
                                    Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                                    
                                }else if(output == "draw"){
                                    print("AI move from ("+barisAwal+","+kolomAwal+") to ("+barisKe+","+kolomKe+")");
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
                                    timer = 3;
                                    moved = true;
                                    Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" and draw");
                                    Data.updatePosisiBidak();
                                    lastCase.solusiStrategi = 4;
                                    listKasus.Add(lastCase);
                                    Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                                    
                                }else if(output == "kalah"){
                                    print("AI move from ("+barisAwal+","+kolomAwal+") to ("+barisKe+","+kolomKe+")");
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
                                    timer = 3;
                                    moved = true;
                                    Data.WriteResponseAI("AI targeting "+target.bidakPemain.GetComponentInChildren<Text>().text+" and lose");
                                    Data.updatePosisiBidak();
                                    lastCase.solusiStrategi = 4;
                                    listKasus.Add(lastCase);
                                    Data.WriteCase(lastCase.pemainMemasukiWilayahAI+","+lastCase.jalurKeretaKolomAIKosong+","+lastCase.AIMemasukiWilayahPemain+","+lastCase.jalurKeretaKolomPemainKosong+","+lastCase.tempatIstirahatAIKosong+","+lastCase.tempatIstirahatPemainKosong+","+lastCase.bidakTelahDiperkirakanPangkatnya+","+lastCase.benderaPemainDiketahui+","+lastCase.solusiStrategi);
                                    
                                }else if(output == "selesai"){
                                    print("GAME SELESAI");
                                    double akurasi = AI.listKasus.Count - AI.failedSolution;
                                    akurasi *= 100;
                                    akurasi /= AI.listKasus.Count;
                                    Data.WriteCase("Accuration : "+akurasi+"%");
                                    SceneManager.LoadScene("Main Menu");
                                }
                            }
                        }
                    }
                }
                if(!moved){
                    Data.WriteResponseAI("Try another strategy than 4");
                    if(!cekFalseSolution){
                        failedSolution+=1;
                        cekFalseSolution = true;
                        Data.WriteResponseAI("Total false solution :"+failedSolution);
                    }
                    strategy = Random.Range(1,4);
                }
            }else{
                Data.WriteResponseAI("Try another strategy than 4");
                if(!cekFalseSolution){
                    failedSolution+=1;
                    cekFalseSolution = true;
                    Data.WriteResponseAI("Total false solution :"+failedSolution);
                }
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
            if(barisPemain >= barisAI){
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
            }else if(barisPemain < barisAI){
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
    }

    public static void newCase(){
        int pemainMemasukiWilayahAI=0;
        int jalurKeretaKolomAIKosong=0;
        int AIMemasukiWilayahPemain=0;
        int jalurKeretaKolomPemainKosong=0;
        int tempatIstirahatAIKosong=0;
        int tempatIstirahatPemainKosong=0;
        int bidakTelahDiperkirakanPangkatnya=0;
        int benderaPemainDiketahui=0;
        int bidakTelahDiperkirakanPangkatnyaDiIstirahat=0;
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
                    bidakTelahDiperkirakanPangkatnya += 1;
                    int baris = bidakPemain.GetComponent<Bidak>().baris;
                    int kolom = bidakPemain.GetComponent<Bidak>().kolom;
                    if((baris == TempatIstirahat.barisIstirahat[0] && kolom == TempatIstirahat.kolomIstirahat[0]) || 
                    (baris == TempatIstirahat.barisIstirahat[1] && kolom == TempatIstirahat.kolomIstirahat[1]) ||
                    (baris == TempatIstirahat.barisIstirahat[2] && kolom == TempatIstirahat.kolomIstirahat[2]) ||
                    (baris == TempatIstirahat.barisIstirahat[3] && kolom == TempatIstirahat.kolomIstirahat[3]) ||
                    (baris == TempatIstirahat.barisIstirahat[4] && kolom == TempatIstirahat.kolomIstirahat[4]) ||
                    (baris == TempatIstirahat.barisIstirahat[5] && kolom == TempatIstirahat.kolomIstirahat[5]) ||
                    (baris == TempatIstirahat.barisIstirahat[6] && kolom == TempatIstirahat.kolomIstirahat[6]) ||
                    (baris == TempatIstirahat.barisIstirahat[7] && kolom == TempatIstirahat.kolomIstirahat[7])){
                        bidakTelahDiperkirakanPangkatnyaDiIstirahat += 1;
                    }
                }
            }
        }
        Cases kasus = new Cases(pemainMemasukiWilayahAI, jalurKeretaKolomAIKosong, AIMemasukiWilayahPemain, jalurKeretaKolomPemainKosong, tempatIstirahatAIKosong, tempatIstirahatPemainKosong, bidakTelahDiperkirakanPangkatnya, bidakTelahDiperkirakanPangkatnyaDiIstirahat, benderaPemainDiketahui, 0);
        similarity(kasus);
        
    }

    public static void similarity(Cases kasusBaru){
        List<double> listSimilarity= new List<double>();
        double f1=0;
        double f2=0;
        double f3=0;
        double f4=0;
        double f5=0;
        double f6=0;
        double f7=0;
        double f8=0;
        double f9=0;
        double bobot1=2;
        double bobot2=4;
        double bobot3=2;
        double bobot4=4;
        double bobot5=2;
        double bobot6=2;
        double bobot7=4;
        double bobot8=5;
        double bobot9=5;
        double totalBobot = bobot1 + bobot2 + bobot3 + bobot4 + bobot5 + bobot6 + bobot7 + bobot8 + bobot9;
        foreach(var kasus in listKasus){
            if(kasusBaru.pemainMemasukiWilayahAI == kasus.pemainMemasukiWilayahAI){
                f1 = 1;
            }else if(kasusBaru.pemainMemasukiWilayahAI != kasus.pemainMemasukiWilayahAI){
                f1 = 0.3;
            }
            if(kasusBaru.jalurKeretaKolomAIKosong == kasus.jalurKeretaKolomAIKosong){
                f2 = 1;
            }else if(kasusBaru.jalurKeretaKolomAIKosong != kasus.jalurKeretaKolomAIKosong){
                f2 = 0.5;
            }
            if(kasusBaru.AIMemasukiWilayahPemain == kasus.AIMemasukiWilayahPemain){
                f3 = 1;
            }else if(kasusBaru.AIMemasukiWilayahPemain != kasus.AIMemasukiWilayahPemain){
                f3 = 0.3;
            }
            if(kasusBaru.jalurKeretaKolomPemainKosong == kasus.jalurKeretaKolomPemainKosong){
                f4 = 1;
            }else if(kasusBaru.jalurKeretaKolomPemainKosong != kasus.jalurKeretaKolomPemainKosong){
                f4 = 0.5;
            }
            if(kasusBaru.tempatIstirahatAIKosong == kasus.tempatIstirahatAIKosong){
                f5 = 1;
            }else if(kasusBaru.tempatIstirahatAIKosong != kasus.tempatIstirahatAIKosong){
                f5 = 0;
            }
            if(kasusBaru.tempatIstirahatPemainKosong == kasus.tempatIstirahatPemainKosong){
                f6 = 1;
            }else if(kasusBaru.tempatIstirahatPemainKosong != kasus.tempatIstirahatPemainKosong){
                f6 = 0;
            }
            if(kasusBaru.bidakTelahDiperkirakanPangkatnya == kasus.bidakTelahDiperkirakanPangkatnya){
                f7 = 1;
            }else if(kasusBaru.bidakTelahDiperkirakanPangkatnya != kasus.bidakTelahDiperkirakanPangkatnya){
                f7 = 0;
            }
            if(kasusBaru.benderaPemainDiketahui == kasus.benderaPemainDiketahui){
                f8 = 1;
            }else if(kasusBaru.benderaPemainDiketahui != kasus.benderaPemainDiketahui){
                f8 = 0;
            }
            if(kasusBaru.bidakTelahDiperkirakanPangkatnyaDiIstirahat == kasus.bidakTelahDiperkirakanPangkatnyaDiIstirahat){
                f9 = 1;
            }else if(kasusBaru.bidakTelahDiperkirakanPangkatnyaDiIstirahat != kasus.bidakTelahDiperkirakanPangkatnyaDiIstirahat){
                f9 = 0.3;
            }
            double bobotxsim1 = f1*bobot1;
            double bobotxsim2 = f2*bobot2;
            double bobotxsim3 = f3*bobot3;
            double bobotxsim4 = f4*bobot4;
            double bobotxsim5 = f5*bobot5;
            double bobotxsim6 = f6*bobot6;
            double bobotxsim7 = f7*bobot7;
            double bobotxsim8 = f8*bobot8;
            double bobotxsim9 = f9*bobot9;
            double total = bobotxsim1 + bobotxsim2 + bobotxsim3+ bobotxsim4 + bobotxsim5 + bobotxsim6 + bobotxsim7 + bobotxsim8 + bobotxsim9;
            double hasil = total/totalBobot;
            listSimilarity.Add(hasil);
        }
        List<double> sortedListSimilarity = new List<double>(listSimilarity);
        sortedListSimilarity.Sort();
        double maxSimilarity = sortedListSimilarity[sortedListSimilarity.Count - 1];
        int indeks = listSimilarity.IndexOf(maxSimilarity);
        // print(indeks);
        // print(listSimilarity[indeks]);
        strategy = listKasus[indeks].solusiStrategi;
        // print("AI give solution strategy "+strategy+" from case index "+indeks);
        Data.WriteResponseAI("AI give solution strategy "+strategy+" from case index "+indeks);
        lastCase = kasusBaru;
        //listKasus.Add(kasusBaru);
        //Data.WriteCase(kasusBaru.pemainMemasukiWilayahAI+","+kasusBaru.jalurKeretaKolomAIKosong+","+kasusBaru.AIMemasukiWilayahPemain+","+kasusBaru.jalurKeretaKolomPemainKosong+","+kasusBaru.tempatIstirahatAIKosong+","+kasusBaru.tempatIstirahatPemainKosong+","+kasusBaru.bidakTelahDiperkirakanPangkatnya+","+kasusBaru.benderaPemainDiketahui+","+kasusBaru.solusiStrategi);
    }
}

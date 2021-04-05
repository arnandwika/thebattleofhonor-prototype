using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private char tempatIstirahat1;
    private char tempatIstirahat2;
    private char tempatIstirahat3;
    private char tempatIstirahat4;
    private bool giliranPemain = true;
    private int barisPos;
    private int kolomPos;
    private int pangkatBidak;
    private int strategy; //1 bertahan, 2 menyerang
    public GameObject[] listBidak;
    private float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        strategy = 1;
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
                    barisPos = Random.Range(3, 6);
                    kolomPos = Random.Range(2, 5);
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 4 && kolomPos == 3)){
                            continue;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 4, 3, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 4, 3, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                            bidak.GetComponent<Bidak>().baris = 4;
                            bidak.GetComponent<Bidak>().kolom = 3;
                            timer = 2;
                        }
                    }
                }else if(tempatIstirahat3 == 'n'){
                    GameObject posisi = Data.listPosisi[4,1];
                    barisPos = Random.Range(3, 6);
                    kolomPos = Random.Range(0, 3);
                    
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 4 && kolomPos == 1)){
                            continue;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 4, 1, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 4, 1, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                            bidak.GetComponent<Bidak>().baris = 4;
                            bidak.GetComponent<Bidak>().kolom = 1;
                            timer = 2;
                        }
                    }
                }else if(tempatIstirahat4 == 'n'){
                    GameObject posisi = Data.listPosisi[4,3];
                    barisPos = Random.Range(3, 6);
                    kolomPos = Random.Range(2, 5);
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 4 && kolomPos == 3)){
                            continue;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 4, 3, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 4, 3, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                            bidak.GetComponent<Bidak>().baris = 4;
                            bidak.GetComponent<Bidak>().kolom = 3;
                            timer = 2;
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
                    barisPos = Random.Range(1, 4);
                    kolomPos = Random.Range(2, 5);
                    
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 2 && kolomPos == 3)){
                            continue;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 2, 3, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 2, 3, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                            bidak.GetComponent<Bidak>().baris = 2;
                            bidak.GetComponent<Bidak>().kolom = 3;
                            timer = 2;
                        }
                    }
                }else if(tempatIstirahat1 == 'n'){
                    GameObject posisi = Data.listPosisi[2,1];
                    barisPos = Random.Range(1, 4);
                    kolomPos = Random.Range(0, 3);
                    
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 2 && kolomPos == 1)){
                            continue;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 2, 1, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 2, 1, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                            bidak.GetComponent<Bidak>().baris = 2;
                            bidak.GetComponent<Bidak>().kolom = 1;
                            timer = 2;
                        }
                    }
                }else if(tempatIstirahat2 == 'n'){
                    print("error 3");
                    GameObject posisi = Data.listPosisi[2,3];
                    barisPos = Random.Range(1, 4);
                    kolomPos = Random.Range(2, 5);
                    
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 2 && kolomPos == 3)){
                            continue;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 2, 3, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 2, 3, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                            bidak.GetComponent<Bidak>().baris = 2;
                            bidak.GetComponent<Bidak>().kolom = 3;
                            timer = 2;
                        }
                    }
                }else{
                    randomMovement();
                }
            }else{
                strategy = 2;
            }
        }else if(strategy == 2 && !giliranPemain){
            int indeks = Random.Range(0, listBidak.Length);
            GameObject bidak = listBidak[indeks];

            if(bidak != null){
                pangkatBidak = bidak.GetComponent<Bidak>().pangkat;
                if(!(pangkatBidak == 1 || pangkatBidak == -1)){
                    print("bidak bisa bergerak");
                }
            }
        }
    }
}

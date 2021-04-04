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
    private int strategy; //1 bertahan, 2 menyerang
    public GameObject[] listBidak;
    // Start is called before the first frame update
    void Start()
    {
        strategy = 1;
        randomMovement();
        giliranPemain = Giliran.getGiliran();
    }

    // Update is called once per frame
    void Update()
    {
        giliranPemain = Giliran.getGiliran();
        if(!giliranPemain){
            randomMovement();
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
                            break;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 4, 3, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 4, 3, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                        }else{
                            print("error 1");
                        }
                    }
                }else if(tempatIstirahat3 == 'n'){
                    GameObject posisi = Data.listPosisi[4,1];
                    barisPos = Random.Range(3, 6);
                    kolomPos = Random.Range(0, 3);
                    
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 4 && kolomPos == 1)){
                            break;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 4, 1, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 4, 1, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                        }else{
                            print("error 2");
                        }
                    }
                }else if(tempatIstirahat4 == 'n'){
                    GameObject posisi = Data.listPosisi[4,3];
                    barisPos = Random.Range(3, 6);
                    kolomPos = Random.Range(2, 5);
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 4 && kolomPos == 3)){
                            break;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 4, 3, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 4, 3, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                        }else{
                            print("error 3");
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
                            break;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 2, 3, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 2, 3, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                        }else{
                            print("error 4");
                        }
                    }
                }else if(tempatIstirahat1 == 'n'){
                    GameObject posisi = Data.listPosisi[2,1];
                    barisPos = Random.Range(1, 4);
                    kolomPos = Random.Range(0, 3);
                    
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 2 && kolomPos == 1)){
                            break;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 2, 1, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 2, 1, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                        }else{
                            print("error 5");
                        }
                    }
                }else if(tempatIstirahat2 == 'n'){
                    GameObject posisi = Data.listPosisi[2,3];
                    barisPos = Random.Range(1, 4);
                    kolomPos = Random.Range(2, 5);
                    
                    foreach(var bidak in listBidak){
                        if(bidak == null || (barisPos == 2 && kolomPos == 3)){
                            break;
                        }else if(bidak.GetComponent<Bidak>().baris == barisPos && bidak.GetComponent<Bidak>().kolom == kolomPos){
                            bidak.transform.position = new Vector3(posisi.transform.position.x, posisi.transform.position.y, posisi.transform.position.z);
                            Data.dataPangkatPindah(barisPos, kolomPos, 2, 3, Data.getPangkat(barisPos, kolomPos));
                            Data.dataKepemilikanPindah(barisPos, kolomPos, 2, 3, Data.getKepemilikan(barisPos, kolomPos));
                            Giliran.setGiliranPemain();
                        }else{
                            print("error 6");
                        }
                    }
                }else{
                    randomMovement();
                }
            }else{
                print("Sudah masuk istirahat");
            }
        }else if(strategy == 2 && !giliranPemain){
            
        }
    }
}

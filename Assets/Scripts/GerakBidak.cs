﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerakBidak : MonoBehaviour
{
	public Sprite sprites;
    private string scene_name;
    private bool awal_permainan;
    private bool cek_asal;
    private bool cek_taruh;
    private bool giliranPemain;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites;
        scene_name = SceneManager.GetActiveScene().name;
        awal_permainan = true;
        cek_asal = false;
        cek_taruh = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 screenPoint;
    private Vector3 offset;
    private float firstY;
    private float newY;
    private float firstX;
    private float newX;
    private bool status = false;
    private Collider2D obyek_akhir;
    private Collider2D obyek_bidak;
    private Collider2D obyek_asal;
    private Posisi posAwal;
    private Posisi posTujuan;
    private int gerak;
    private int barisPosAwal;
    private int kolomPosAwal;
    private int barisPosTujuan;
    private int kolomPosTujuan;
    private int pangkatBidak;
    private int barisPos;
    private int kolomPos;
    private char kepemilikanBidak;

    void OnMouseDown(){
        giliranPemain = Giliran.getGiliran();
        if((gameObject.tag == "Pemain" && giliranPemain)){
            cek_taruh = false;
            firstY = transform.position.y;
            firstX = transform.position.x;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));  
        }else{
            if(gameObject.tag == "Pemain"){
                print("Giliran lawan");
            }else if(gameObject.tag == "Lawan"){
                print("Giliran pemain");
            }
        }
    }
    void OnMouseDrag(){
        if((gameObject.tag == "Pemain" && giliranPemain)){
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;
            transform.position = curPosition;
        }
    }
    void OnMouseUp(){
        if(scene_name == "Rookie" && (gameObject.tag == "Pemain" && giliranPemain)){
            if(status && AturanGerak.cekPangkatBergerak(barisPosAwal, kolomPosAwal)){
                if(AturanGerak.opsiGerak(gerak, barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan)){
                    if((gameObject.tag == "Pemain" && obyek_akhir.tag == "Lawan") || (gameObject.tag == "Lawan" && obyek_akhir.tag == "Pemain")){
                        string hasil = Data.bidakBertabrakan(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan);
                        if(hasil == "menang"){
                            transform.position = new Vector3(newX, newY, transform.position.z);
                            obyek_akhir.tag = gameObject.tag;
                            obyek_asal.tag = "Untagged";
                            cek_asal = false;
                            cek_taruh = true;
                            pangkatBidak = gameObject.GetComponent<Bidak>().pangkat;
                            kepemilikanBidak = gameObject.GetComponent<Bidak>().kepemilikan;
                            Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan, pangkatBidak);
                            Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan, kepemilikanBidak);
                            Destroy(obyek_bidak.gameObject);
                        }else if(hasil == "draw"){
                            obyek_akhir.tag = "Untagged";
                            obyek_asal.tag = "Untagged";
                            cek_asal = false;
                            cek_taruh = true;
                            Data.zeroPangkat(barisPosAwal, kolomPosAwal);
                            Data.zeroPangkat(barisPosTujuan, kolomPosTujuan);
                            Data.noneKepemilikan(barisPosAwal, kolomPosAwal);
                            Data.noneKepemilikan(barisPosTujuan, kolomPosTujuan);
                            if(gameObject.tag == "Pemain"){
                                Giliran.setGiliranLawan();
                                giliranPemain = Giliran.getGiliran();
                            }else if(gameObject.tag == "Lawan"){
                                Giliran.setGiliranPemain();
                                giliranPemain = Giliran.getGiliran();
                            }
                            Destroy(obyek_bidak.gameObject);
                            Destroy(gameObject);
                        }else if(hasil == "kalah"){
                            obyek_asal.tag = "Untagged";
                            cek_asal = false;
                            cek_taruh = true;
                            Data.zeroPangkat(barisPosAwal, kolomPosAwal);
                            Data.noneKepemilikan(barisPosAwal, kolomPosAwal);
                            if(gameObject.tag == "Pemain"){
                                Giliran.setGiliranLawan();
                                giliranPemain = Giliran.getGiliran();
                            }else if(gameObject.tag == "Lawan"){
                                Giliran.setGiliranPemain();
                                giliranPemain = Giliran.getGiliran();
                            }
                            Destroy(gameObject);
                        }else if(hasil == "selesai"){
                            print("GAME SELESAI");
                            SceneManager.LoadScene("Main Menu");
                        }
                    }else{
                        transform.position = new Vector3(newX, newY, transform.position.z);
                        obyek_akhir.tag = gameObject.tag;
                        obyek_asal.tag = "Untagged";
                        cek_asal = false;
                        cek_taruh = true;
                        pangkatBidak = gameObject.GetComponent<Bidak>().pangkat;
                        kepemilikanBidak = gameObject.GetComponent<Bidak>().kepemilikan;
                        Data.dataPangkatPindah(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan, pangkatBidak);
                        Data.dataKepemilikanPindah(barisPosAwal, kolomPosAwal, barisPosTujuan, kolomPosTujuan, kepemilikanBidak);
                        if(gameObject.tag == "Pemain"){
                            Giliran.setGiliranLawan();
                            giliranPemain = Giliran.getGiliran();
                        }else if(gameObject.tag == "Lawan"){
                            Giliran.setGiliranPemain();
                            giliranPemain = Giliran.getGiliran();
                        }
                    }
                }else{
                    transform.position = new Vector3(firstX, firstY, transform.position.z);
                    cek_asal = false;
                    cek_taruh = true;
                    //obyek.tag = "Untagged";
                }
            }else{
                transform.position = new Vector3(firstX, firstY, transform.position.z);
                cek_asal = false;
                cek_taruh = true;
                //obyek.tag = "Untagged";
            }
        }else if(scene_name == "Expert"){
            if(newY >= firstY && status){
                if((newX == TempatIstirahat.xIstirahat[0] && newY == TempatIstirahat.yIstirahat[0]) || 
                (newX == TempatIstirahat.xIstirahat[1] && newY == TempatIstirahat.yIstirahat[1]) ||
                (newX == TempatIstirahat.xIstirahat[2] && newY == TempatIstirahat.yIstirahat[2]) ||
                (newX == TempatIstirahat.xIstirahat[3] && newY == TempatIstirahat.yIstirahat[3]) ||
                (newX == TempatIstirahat.xIstirahat[4] && newY == TempatIstirahat.yIstirahat[4]) ||
                (newX == TempatIstirahat.xIstirahat[5] && newY == TempatIstirahat.yIstirahat[5]) ||
                (newX == TempatIstirahat.xIstirahat[6] && newY == TempatIstirahat.yIstirahat[6]) ||
                (newX == TempatIstirahat.xIstirahat[7] && newY == TempatIstirahat.yIstirahat[7])){
                    transform.position = new Vector3(firstX, firstY, transform.position.z);
                    cek_asal = false;
                    cek_taruh = true;
                    //obyek.tag = "Untagged";
                }else{
                    transform.position = new Vector3(newX, newY, transform.position.z);
                    obyek_akhir.tag = "Pemain";
                    obyek_asal.tag = "Untagged";
                    cek_asal = false;
                    cek_taruh = true;
                }
            }else{
                transform.position = new Vector3(firstX, firstY, transform.position.z);
                cek_asal = false;
                cek_taruh = true;
                //obyek.tag = "Untagged";
            }
        }
        AturanGerak.allow = true;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(!collision.isTrigger){
            obyek_akhir = collision;
        }
        if(awal_permainan){
            obyek_akhir.tag = gameObject.tag;
            pangkatBidak = gameObject.GetComponent<Bidak>().pangkat;
            kepemilikanBidak = gameObject.GetComponent<Bidak>().kepemilikan;
            if(!collision.isTrigger){
                barisPos = collision.GetComponent<Posisi>().barisPos;
                kolomPos = collision.GetComponent<Posisi>().kolomPos;
            }
            Data.insertPangkat(barisPos, kolomPos, pangkatBidak);
            Data.insertKepemilikan(barisPos, kolomPos, kepemilikanBidak);
            gameObject.GetComponent<Bidak>().baris = barisPos;
            gameObject.GetComponent<Bidak>().kolom = kolomPos;
            awal_permainan = false;
        }else{
            if(!collision.isTrigger){
                posTujuan = collision.GetComponent<Posisi>();
                barisPosTujuan = posTujuan.barisPos;
                kolomPosTujuan = posTujuan.kolomPos;
            }
            if(collision.isTrigger){
                obyek_bidak = collision;
            }
        }
        if(obyek_akhir.tag == gameObject.tag){
            status = false;
        }else if(obyek_akhir.tag != gameObject.tag){
            status = true;
            newX = collision.transform.position.x;
            newY = collision.transform.position.y;
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(cek_asal == false && cek_taruh == false){
            obyek_asal = collision;
            cek_asal = true;
            if(!collision.isTrigger){
                posAwal = collision.GetComponent<Posisi>();
                gerak = posAwal.gerak;
                barisPosAwal = posAwal.barisPos;
                kolomPosAwal = posAwal.kolomPos;
            }
        }
        status = false;
    }
}

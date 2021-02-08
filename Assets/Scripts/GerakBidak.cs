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
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites;
        scene_name = SceneManager.GetActiveScene().name;
        awal_permainan = true;
        cek_asal = false;
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
    private Collider2D obyek;
    private Collider2D obyek_asal;

    void OnMouseDown(){
    	firstY = transform.position.y;
        firstX = transform.position.x;
    	screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    	offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    void OnMouseDrag(){
    	Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    	Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;
    	transform.position = curPosition;
    }
    void OnMouseUp(){
        if(scene_name == "Expert"){
            if(status){
                transform.position = new Vector3(newX, newY, transform.position.z);
                obyek.tag = "Pemain";
                obyek_asal.tag = "Untagged";
                cek_asal = false;
            }else{
                transform.position = new Vector3(firstX, firstY, transform.position.z);
                cek_asal = false;
                //obyek.tag = "Untagged";
            }
        }else if(scene_name == "Rookie"){
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
                    //obyek.tag = "Untagged";
                }else{
                    transform.position = new Vector3(newX, newY, transform.position.z);
                    obyek.tag = "Pemain";
                    obyek_asal.tag = "Untagged";
                    cek_asal = false;
                    Debug.Log(TempatIstirahat.xIstirahat[0]);
                    Debug.Log(TempatIstirahat.yIstirahat[0]);
                    Debug.Log(newX);
                    Debug.Log(newY);
                }
            }else{
                transform.position = new Vector3(firstX, firstY, transform.position.z);
                cek_asal = false;
                //obyek.tag = "Untagged";
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        obyek = collision;
        if(awal_permainan){
            obyek.tag = "Pemain";
            awal_permainan = false;
        }
        if(obyek.tag == "Pemain"){
            status = false;
        }else{
            status = true;
            newX = collision.transform.position.x;
            newY = collision.transform.position.y;
        }
        
    }
    void OnTriggerExit2D(Collider2D collision){
        if(cek_asal == false){
            obyek_asal = collision;
            cek_asal = true;
        }
        status = false;
    }
}

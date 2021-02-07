using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerakBidak : MonoBehaviour
{
	public Sprite sprites;
    private string scene_name;
    private bool awal_permainan;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites;
        scene_name = SceneManager.GetActiveScene().name;
        awal_permainan = true;
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
            }else{
                transform.position = new Vector3(firstX, firstY, transform.position.z);
                //obyek.tag = "Untagged";
            }
        }else if(scene_name == "Rookie"){
            if(newY >= firstY && status){
                transform.position = new Vector3(newX, newY, transform.position.z);
                obyek.tag = "Pemain";
                obyek_asal.tag = "Untagged";
            }else{
                transform.position = new Vector3(firstX, firstY, transform.position.z);
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
        obyek_asal = collision;
        status = false;
    }
}

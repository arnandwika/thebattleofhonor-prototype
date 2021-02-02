using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerakBidak : MonoBehaviour
{
	public Sprite sprites;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites;
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
    private float newZ;
    private bool status = false;

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
        if(status){
            transform.position = new Vector3(newX, newY, newZ);
        }else{
            transform.position = new Vector3(firstX, firstY, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        status = true;
        newX = collision.transform.position.x;
        newY = collision.transform.position.y;
        newZ = collision.transform.position.z;
    }
    void OnTriggerExit2D(Collider2D collision){
        status = false;
    }
}

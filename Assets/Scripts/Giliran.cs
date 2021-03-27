using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Giliran : MonoBehaviour
{
    public static bool giliranPemain = true;
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(giliranPemain){
            txt.text = "YOUR               TURN";
            txt.color = Color.black;
        }else if(!giliranPemain){
            txt.text = "ENEMY             TURN";
            txt.color = Color.red;
        }
        
    }

    public static void setGiliranPemain(){
        giliranPemain = true;
    }

    public static void setGiliranLawan(){
        giliranPemain = false;
    }

    public static bool getGiliran(){
        return giliranPemain;
    }
}

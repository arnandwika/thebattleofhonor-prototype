using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Giliran : MonoBehaviour
{
    public static bool giliranPemain = true;
    public Text txt;
    private string scene_name;
    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        scene_name = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if(scene_name == "PD_Penempatan_Bidak" && AturanGerak.penempatanBidak == false && AturanGerak.jumlahBidakDitempatkan < 25){
            txt.text = "PLACE  YOUR  PAWN";
        }else if(scene_name == "PD_Penempatan_Bidak" && AturanGerak.penempatanBidak == false && AturanGerak.jumlahBidakDitempatkan == 25){
            txt.text = "READY TO PLAY?";
        }else{
            if(giliranPemain){
                txt.text = "YOUR               TURN";
                txt.color = Color.black;
            }else if(!giliranPemain){
                txt.text = "ENEMY             TURN";
                txt.color = Color.red;
            }
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

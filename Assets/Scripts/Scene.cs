using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void PDPenempatanBidak(){
        if(AturanGerak.jumlahBidakDitempatkan == 25){
            AturanGerak.penempatanBidak = true;
            gameObject.GetComponent<Image>().color = Color.black;
        }
    }

    public void PengujianDasar(){
        SceneManager.LoadScene("Pengujian Dasar");
    }
    // public void PengujianSkenarioEasy(){
    //     SceneManager.LoadScene("Pengujian Skenario Easy");
    // }
    // public void PengujianSkenarioMedium(){
    //     SceneManager.LoadScene("Pengujian Skenario Medium");
    // }
    // public void PengujianSkenarioHard(){
    //     SceneManager.LoadScene("Pengujian Skenario Hard");
    // }
}

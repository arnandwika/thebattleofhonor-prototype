using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AturanGerak : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool opsiGerak(int gerak, int barisPosAwal, int kolomPosAwal, int barisPosTujuan, int kolomPosTujuan){
        print(barisPosAwal);
        print(kolomPosAwal);
        print(barisPosTujuan);
        print(kolomPosTujuan);
        if(barisPosAwal == barisPosTujuan && kolomPosAwal == kolomPosTujuan){
            return false;
        }else{
            if(gerak == 1){
                if(barisPosAwal == barisPosTujuan && (kolomPosAwal == kolomPosTujuan+1 || kolomPosAwal == kolomPosTujuan-1)){
                    return true;
                }else if(barisPosAwal == barisPosTujuan+1 && (kolomPosAwal == kolomPosTujuan+1 || kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan)){
                    return true;
                }else if(barisPosAwal == barisPosTujuan-1 && (kolomPosAwal == kolomPosTujuan+1 || kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan)){
                    return true;
                }else{
                    return false;
                }
            }else if(gerak == 2){
                return true;
            }
            else if(gerak == 3){
                return true;
            }
            else if(gerak == 4){
                return true;
            }
            else if(gerak == 5){
                return true;
            }
            else if(gerak == 6){
                return true;
            }
            else if(gerak == 7){
                return true;
            }
            else if(gerak == 8){
                return true;
            }
            else if(gerak == 9){
                return true;
            }
        }
        return false;
    }
}

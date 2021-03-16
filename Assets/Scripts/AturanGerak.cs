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
                if(barisPosAwal == barisPosTujuan){
                    return true;
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 1){
                    if(barisPosAwal >= barisPosTujuan-5){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 12){
                    if(barisPosAwal <= barisPosTujuan+5){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 5){
                    if(barisPosAwal <= barisPosTujuan+4 && barisPosTujuan < barisPosAwal){
                        return true;
                    }else if(barisPosAwal == barisPosTujuan-1 && barisPosTujuan > barisPosAwal){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 8){
                    if(barisPosAwal >= barisPosTujuan-4 && barisPosTujuan > barisPosAwal){
                        return true;
                    }else if(barisPosAwal == barisPosTujuan+1 && barisPosTujuan < barisPosAwal){
                        return true;
                    }else{
                        return false;
                    }
                }else if(barisPosAwal != barisPosTujuan && kolomPosAwal != kolomPosTujuan){
                    if(barisPosAwal == barisPosTujuan-1 && (kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan+1) && (barisPosAwal == 1 || barisPosAwal == 8)){
                        return true;
                    }else if(barisPosAwal == barisPosTujuan+1 && (kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan+1) && (barisPosAwal == 5 || barisPosAwal == 12)){
                        return true;
                    }else{
                        return false;
                    }
                }else{
                    return false;
                }
            }
            else if(gerak == 3){
                if(barisPosAwal == barisPosTujuan){
                    return true;
                }else if(kolomPosAwal == kolomPosTujuan && (barisPosAwal == barisPosTujuan-1 || barisPosAwal == barisPosTujuan+1)){
                    return true;
                }else{
                    return false;
                }
            }
            else if(gerak == 4){
                if(barisPosAwal == barisPosTujuan && (kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan+1)){
                    return true;
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 11){
                    if(barisPosAwal > barisPosTujuan && barisPosAwal <= barisPosTujuan+4){
                        return true;
                    }else if(barisPosAwal < barisPosTujuan && barisPosAwal == barisPosTujuan-1){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 9){
                    if(barisPosAwal > barisPosTujuan && barisPosAwal <= barisPosTujuan+2){
                        return true;
                    }else if(barisPosAwal < barisPosTujuan && barisPosAwal >= barisPosTujuan-3){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 4){
                    if(barisPosAwal > barisPosTujuan && barisPosAwal <= barisPosTujuan+3){
                        return true;
                    }else if(barisPosAwal < barisPosTujuan && barisPosAwal >= barisPosTujuan-2){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 2){
                    if(barisPosAwal > barisPosTujuan && barisPosAwal == barisPosTujuan+1){
                        return true;
                    }else if(barisPosAwal < barisPosTujuan && barisPosAwal >= barisPosTujuan-4){
                        return true;
                    }else{
                        return false;
                    }
                }else{
                    return false;
                }
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

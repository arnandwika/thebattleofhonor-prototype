using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AturanGerak : MonoBehaviour
{
    private static int loop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool opsiGerak(int gerak, int barisPosAwal, int kolomPosAwal, int barisPosTujuan, int kolomPosTujuan){
        // print(barisPosAwal);
        // print(kolomPosAwal);
        // print(barisPosTujuan);
        // print(kolomPosTujuan);
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
                if(barisPosAwal == barisPosTujuan && cekLompatKolom(kolomPosAwal, kolomPosTujuan, barisPosAwal)){
                    return true;
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 1){
                    if(barisPosAwal >= barisPosTujuan-5 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 12){
                    if(barisPosAwal <= barisPosTujuan+5 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 5){
                    if(barisPosAwal <= barisPosTujuan+4 && barisPosAwal > barisPosTujuan && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else if(barisPosAwal == barisPosTujuan-1 && barisPosAwal < barisPosTujuan){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 8){
                    if(barisPosAwal >= barisPosTujuan-4 && barisPosAwal < barisPosTujuan && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else if(barisPosAwal == barisPosTujuan+1 && barisPosAwal > barisPosTujuan){
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
                if(barisPosAwal == barisPosTujuan && cekLompatKolom(kolomPosAwal, kolomPosTujuan, barisPosAwal)){
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
                    if(barisPosAwal > barisPosTujuan && barisPosAwal <= barisPosTujuan+4 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else if(barisPosAwal < barisPosTujuan && barisPosAwal == barisPosTujuan-1){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 9){
                    if(barisPosAwal > barisPosTujuan && barisPosAwal <= barisPosTujuan+2 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else if(barisPosAwal < barisPosTujuan && barisPosAwal >= barisPosTujuan-3 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 4){
                    if(barisPosAwal > barisPosTujuan && barisPosAwal <= barisPosTujuan+3 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else if(barisPosAwal < barisPosTujuan && barisPosAwal >= barisPosTujuan-2 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else{
                        return false;
                    }
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 2){
                    if(barisPosAwal > barisPosTujuan && barisPosAwal == barisPosTujuan+1){
                        return true;
                    }else if(barisPosAwal < barisPosTujuan && barisPosAwal >= barisPosTujuan-4 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else{
                        return false;
                    }
                }else{
                    return false;
                }
            }
            else if(gerak == 5){
                if(barisPosAwal != barisPosTujuan && kolomPosAwal == kolomPosTujuan && (barisPosAwal == barisPosTujuan-1 || barisPosAwal == barisPosTujuan+1)){
                    return true;
                }else if(kolomPosAwal != kolomPosTujuan && barisPosAwal == barisPosTujuan && (kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan+1)){
                    return true;
                }else{
                    return false;
                }
            }
            else if(gerak == 6){
                if(barisPosAwal != barisPosTujuan && kolomPosAwal == kolomPosTujuan){
                    if(barisPosAwal == 10 && barisPosAwal < barisPosTujuan && barisPosAwal >= barisPosTujuan-2 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else if(barisPosAwal == 10 && barisPosAwal > barisPosTujuan && barisPosAwal <= barisPosTujuan+3 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else if(barisPosAwal == 3 && barisPosAwal < barisPosTujuan && barisPosAwal >= barisPosTujuan-3 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else if(barisPosAwal == 3 && barisPosAwal > barisPosTujuan && barisPosAwal <= barisPosTujuan+2 && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                        return true;
                    }else{
                        return false;
                    }
                }else if(barisPosAwal != barisPosTujuan && kolomPosAwal != kolomPosTujuan){
                    if((barisPosAwal == barisPosTujuan-1 || barisPosAwal == barisPosTujuan+1) && (kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan+1)){
                        return true;
                    }else{
                        return false;
                    }
                }else if(barisPosAwal == barisPosTujuan && (kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan+1)){
                    return true;
                }else{
                    return false;
                }
            }
            else if(gerak == 7){
                if(barisPosAwal == barisPosTujuan && cekLompatKolom(kolomPosAwal, kolomPosTujuan, barisPosAwal)){
                    return true;
                }else if(kolomPosAwal == kolomPosTujuan && (barisPosAwal == barisPosTujuan-1 || barisPosAwal == barisPosTujuan+1)){
                    return true;
                }else if((barisPosAwal == 12 || barisPosAwal == 5) && barisPosAwal == barisPosTujuan+1 && (kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan+1)){
                    return true;
                }else if((barisPosAwal == 1 || barisPosAwal == 8) && barisPosAwal == barisPosTujuan-1 && (kolomPosAwal == kolomPosTujuan-1 || kolomPosAwal == kolomPosTujuan+1)){
                    return true;
                }else{
                    return false;
                }
            }
            else if(gerak == 8){
                if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 7 && barisPosAwal >= barisPosTujuan-5 && barisPosAwal < barisPosTujuan && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                    return true;
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 7 && barisPosAwal == barisPosTujuan+1){
                    return true;
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 6 && barisPosAwal <= barisPosTujuan+5 && barisPosAwal > barisPosTujuan && cekLompatBaris(barisPosAwal, barisPosTujuan, kolomPosAwal)){
                    return true;
                }else if(kolomPosAwal == kolomPosTujuan && barisPosAwal == 6 && barisPosAwal == barisPosTujuan-1){
                    return true;
                }
            }else{
                return false;
            }
        }
        return false;
    }

    public static bool cekLompatBaris(int barisPosAwal, int barisPosTujuan, int kolom){
        bool allow = true;
        if(barisPosAwal > barisPosTujuan && barisPosAwal - barisPosTujuan > 1){
            loop = barisPosAwal - barisPosTujuan;
            for(int i = 1; i < loop; i++){
                if(Data.getPangkat(barisPosTujuan+i, kolom) != 0){
                    allow = false;
                }
            }
        }else if(barisPosTujuan > barisPosAwal && barisPosTujuan - barisPosAwal > 1){
            loop = barisPosTujuan - barisPosAwal;
            for(int i = 1; i < loop; i++){
                if(Data.getPangkat(barisPosAwal+i, kolom) != 0){
                    allow = false;
                }
            }
        }
        return allow;
    }

    public static bool cekLompatKolom(int kolomPosAwal, int kolomPosTujuan, int baris){
        bool allow = true;
        if(kolomPosAwal > kolomPosTujuan && kolomPosAwal - kolomPosTujuan > 1){
            loop = kolomPosAwal - kolomPosTujuan;
            for(int i = 1; i < loop; i++){
                if(Data.getPangkat(baris, kolomPosTujuan+i) != 0){
                    allow = false;
                }
            }
        }else if(kolomPosTujuan > kolomPosAwal && kolomPosTujuan - kolomPosAwal > 1){
            loop = kolomPosTujuan - kolomPosAwal;
            for(int i = 1; i < loop; i++){
                if(Data.getPangkat(baris, kolomPosAwal+i) != 0){
                    allow = false;
                }
            }
        }
        return allow;
    }

    public static bool cekPangkatBergerak(int baris, int kolom){
        if(Data.getPangkat(baris, kolom) == 1 || Data.getPangkat(baris, kolom) == -1){
            return false;
        }else{
            return true;
        }
    }
}

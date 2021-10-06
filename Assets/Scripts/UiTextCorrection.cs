using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UiTextCorrection : MonoBehaviour
{
    public static string CorrectUiValue(float val) {
        float test;
        string newVal = "Error";
        // 1k   000
        // 10k  000
        // 100k  000
        // 1M    000 000

        if (val >= 1000000000) {
            newVal = Math.Round(val / 1000000000, 1).ToString() + "B"; 
        }

        else if (val >= 1000000) {
            newVal = Math.Round(val / 1000000, 1).ToString() + "M";   ///.ToString("B") + "M";
        }
        else if (val >= 1000) {
              newVal = Math.Round(val / 1000 , 1).ToString() + "K";  
        }

        else {
            newVal =  Math.Round(val,1).ToString();
        }

        return newVal;
    }
}

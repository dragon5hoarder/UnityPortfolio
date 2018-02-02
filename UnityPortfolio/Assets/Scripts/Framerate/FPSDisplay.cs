﻿using UnityEngine;
using UnityEngine.UI;

//displays the FPS
[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour {

     public Text highestFPSLabel, averageFPSLabel, lowestFPSLabel;

     FPSCounter fpsCounter;

     //allocates memory away from the garbage collector
     static string[] stringsFrom00to99 = {
          "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
          "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
          "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
          "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
          "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
          "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
          "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
          "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
          "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
          "90", "91", "92", "93", "94", "95", "96", "97", "98", "99"
     };

     //assigns a color to each increment of FPS 
     [System.Serializable]
     private struct FPSColor {
          public Color color;
          public int minimumFPS;
     }
     [SerializeField]
     private FPSColor[] coloring;

     //gets the fps counter attached to bject
     void Awake () {
          fpsCounter = GetComponent<FPSCounter>();
     }

     //each display shows the fps assigned to it
     void Update() {
          Display(highestFPSLabel, fpsCounter.HighestFPS);
          Display(averageFPSLabel, fpsCounter.AverageFPS);
          Display(lowestFPSLabel, fpsCounter.LowestFPS);         
     }

     //the current label shows two digits from 00 to 99 and changes color based on the 
     //amount of fps it displays    
     void Display (Text label, int fps) {
          label.text = stringsFrom00to99[Mathf.Clamp(fps, 0, 99)];
          for (int i = 0; i < coloring.Length; i++) {
               if (fps >= coloring[i].minimumFPS) {
                    label.color = coloring[i].color;
                    break;
               }
          }
     }
}

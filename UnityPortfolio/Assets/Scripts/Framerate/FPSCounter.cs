using UnityEngine;

//calculates the current fps
public class FPSCounter : MonoBehaviour {

     //the current fps
	public int AverageFPS { get; private set; }
     public int HighestFPS { get; private set; }
     public int LowestFPS { get; private set; }

     //amount stored in fpsBuffer
     public int frameRange = 60;

     int[] fpsBuffer;
     int fpsBufferIndex;

     void Update() {
          if (fpsBuffer == null || fpsBuffer.Length != frameRange) {
               InitializeBuffer();
          }
          UpdateBuffer();
          CalculateFPS();
     }

     //initializes the buffer
     void InitializeBuffer () {
          if (frameRange <= 0) {
               frameRange = 1;
          }
          fpsBuffer = new int[frameRange];
          fpsBufferIndex = 0;
     }

     //updates the buffer
     void UpdateBuffer() {
          //stores the current fps at the current index
          fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
          //wraps the index back to the start
          if (fpsBufferIndex >= frameRange) {
               fpsBufferIndex = 0;
          }
     }

     //calculates the fps
     void CalculateFPS() {
          int sum = 0;
          int highest = 0;
          int lowest = int.MaxValue;
          //finds the avg fps by summing all the values in the buffer and averages them out
          //and stores the highest and lowest fps
          for (int i = 0; i < frameRange; i++) {
               int fps = fpsBuffer[i];
               sum += fps;
               if (fps > highest) {
                    highest = fps;
               }
               if (fps < lowest) {
                    lowest = fps;
               }
          }
          AverageFPS = sum / frameRange;
          HighestFPS = highest;
          LowestFPS = lowest;
     }


}

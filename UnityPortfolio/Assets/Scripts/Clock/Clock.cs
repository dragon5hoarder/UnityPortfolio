using System;
using UnityEngine;

//Translates time from the System into an analog clock
public class Clock : MonoBehaviour {

     //Variables used to translate time into degrees
     const float 
          degreesPerHour = 30f,
          degreesPerMinute = 6f,
          degreesPerSecond = 6f;

     //slots for the clock arms
     public Transform hoursTransform, minutesTransform, secondsTransform;

     //switches between continuous movement or ticking
     public bool continuous;

     void Update () {
          if (continuous) {
               UpdateContinuous();
          }
          else {
               UpdateDiscrete();
          }
     }

     //causes the clock arms to move in a smooth motion
     void UpdateContinuous () {
          TimeSpan time = DateTime.Now.TimeOfDay; // only needs to call the current time once
          //rotates the arm in the y axis by translating the current hour/minute/second into degrees
          //translates the y axis into a float number to cause the arms to snap into position
          hoursTransform.localRotation =
               Quaternion.Euler(0f, (float)time.TotalHours * degreesPerHour, 0f);
          minutesTransform.localRotation =
               Quaternion.Euler(0f, (float)time.TotalMinutes * degreesPerMinute, 0f);
          secondsTransform.localRotation =
               Quaternion.Euler(0f, (float)time.TotalSeconds * degreesPerSecond, 0f);
     }

     //causesthe clock arms to snap into the current time position
     void UpdateDiscrete() {
          DateTime time = DateTime.Now; // only needs to call the current time once
          //rotates the arm in the y axis by translating the current hour/minute/second into degrees
          hoursTransform.localRotation =
               Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
          minutesTransform.localRotation =
               Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
          secondsTransform.localRotation =
               Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
     }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour {

     public InputField inputField;
     public Slider slider;

     private void Start()
     {
          inputField.text = System.Convert.ToString(slider.value);
     }

     public void SliderUpdate() { inputField.text = System.Convert.ToString(slider.value); }
     public void InputFieldUpdate() { slider.value = System.Convert.ToSingle(inputField.text); }
}

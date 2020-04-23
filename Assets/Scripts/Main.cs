using System.Collections;
using System.Collections.Generic;
using UnityEngine.Ui;
using UnityEngine;

public class Main : MonoBehaviour
{
   public Quations_list[] array_quation;
   public Text Quations_text;
   
   public void OnClickPlay()
   {
        Quations_text.text = array_quation[Random.Range(0,array_quation.Length)].quation;
   }
}
[System.Serializable]
public class Quations_list 
{
    public string quation;
    public string[] answers = new string [3];
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Main : MonoBehaviour
{
   public Quations_list[] array_quation;
   public Text quations_text;
   public List<object> quation_list;
   public Text[] answers_text;
   int rand_quattion;
   
   public void OnClickPlay()
   {
     quation_list = new List<object>(array_quation);
     Quation_Generate();
   }   
   void Quation_Generate(){
        rand_quattion = Random.Range(0,quation_list.Count);
        Quations_list current_quation = quation_list[rand_quattion] as Quations_list;
        quations_text.text = current_quation.quation;
        quations_text.text = array_quation[Random.Range(0,array_quation.Length)].quation;
        for(int i = 0; i < current_quation.answers.Length; i++){
          answers_text[i].text = current_quation.answers[i]; 
        }
   } 
   public void Answers_button(){
       quation_list.RemoveAt(rand_quattion);
       Quation_Generate();
   }
}
[System.Serializable]
public class Quations_list 
{
    public string quation;
    public string[] answers = new string [3];
}

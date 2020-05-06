using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Main : MonoBehaviour
{
   public Quations[] array_quation;
   public Text quations_text;
   List<object> quation_list;
   public Text[] answers_text;
   Quations current_quation;
   int rand_quattion;
   public GameObject HeadPanel;
   public Button[] answerbuttons = new Button[3];
   public Sprite[] icons = new Sprite[2];
   public Image true_false_icons;
   public Text true_false_text;
   bool defoult_color = false, 
   true_color = false, 
   false_color = false;

   private void Update() {
      if ()    
    }
   public void OnClickPlay()
   {
     quation_list = new List<object>(array_quation);
     Quation_Generate();
     if(!HeadPanel.GetComponent<Animator>().enabled){
        HeadPanel.GetComponent<Animator>().enabled = true;  
     }
     else HeadPanel.GetComponent<Animator>().SetTrigger("In");
   
   }   
   void Quation_Generate(){
        if (quation_list.Count > 0){
        rand_quattion = Random.Range(0,quation_list.Count);
        current_quation = quation_list[rand_quattion] as Quations;      
        quations_text.text = current_quation.quation;
        quations_text.gameObject.SetActive(true);
        List<string> answers = new List<string>(current_quation.answers);
          for(int i = 0; i < current_quation.answers.Length; i++){
          int random_answers = Random.Range(0, answers.Count);
          answers_text[i].text = answers[random_answers];
          answers.RemoveAt(random_answers);          
          }
        StartCoroutine(anim_button_answers());
        }else print("Wictory");
        
   }
     IEnumerator anim_button_answers(){
       yield return new WaitForSeconds(1);
       for(int i = 0; i < answerbuttons.Length; i++){
         answerbuttons[i].interactable = false;
       }
       int c = 0;
       while(c < answerbuttons.Length){
         if(!answerbuttons[c].gameObject.activeSelf)answerbuttons[c].gameObject.SetActive(true);
         else answerbuttons[c].gameObject.GetComponent<Animator>().SetTrigger("In");
         c++;
         yield return new WaitForSeconds(0.5f);
       }
        for(int i = 0; i < answerbuttons.Length; i++){
         answerbuttons[i].interactable = true;
       }
       yield break;
     }
     IEnumerator true_false(bool check_answer){
      
      for(int i = 0; i < answerbuttons.Length; i++){
         answerbuttons[i].interactable = false;
      }
      yield return new WaitForSeconds(1);
      
      if(!quations_text.gameObject.activeSelf){
         quations_text.gameObject.SetActive(true);
      }else quations_text.gameObject.SetActive(false);
      for(int i = 0; i < answerbuttons.Length; i++){
         answerbuttons[i].gameObject.GetComponent<Animator>().SetTrigger("Out");
      }     
        quations_text.gameObject.GetComponent<Animator>().SetTrigger("Out");
      yield return new WaitForSeconds(1.2f); 
        if(!true_false_icons.gameObject.activeSelf){
         true_false_icons.gameObject.SetActive(true);
       }else {
         true_false_icons.gameObject.GetComponent<Animator>().SetTrigger("In");
         }

       if(check_answer){
        true_false_text.gameObject.SetActive(true);
         true_false_icons.sprite = icons[0];
         true_false_text.text = "Правильна відповідь";
         yield return new WaitForSeconds(1);
         true_false_icons.gameObject.GetComponent<Animator>().SetTrigger("Out");
         true_false_text.gameObject.SetActive(false);
         quation_list.RemoveAt(rand_quattion);
         yield return new WaitForSeconds(1f);
         Quation_Generate();
         yield break;
       }else {
         true_false_text.gameObject.SetActive(true);
         true_false_icons.sprite = icons[1];
         true_false_text.text = "Відповідь не правильна";
         yield return new WaitForSeconds(1);
         true_false_icons.gameObject.GetComponent<Animator>().SetTrigger("Out");
         true_false_text.gameObject.SetActive(false);
         HeadPanel.GetComponent<Animator>().SetTrigger("Out");
         yield break;
         }
     }  
    
   public void Answers_button(int index){
       if(answers_text[index].text.ToString() == current_quation.answers[0]) StartCoroutine(true_false(true));
       else StartCoroutine(true_false(false));

   }
}
[System.Serializable]
public class Quations 
{
    public string quation;
    public string[] answers = new string [3];
}

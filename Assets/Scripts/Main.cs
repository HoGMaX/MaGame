using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Main : MonoBehaviour
{
   public CategoryList[] category_lyst = new CategoryList[2];
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
   public int categorys_int = 0;
   public float timeleft = 60;
   public Text timertext;
   public float gametime;
   public bool stateRunning = false;

   private void Update() {
      if (defoult_color){
        HeadPanel.GetComponent<Image>().color = Color.Lerp(HeadPanel.GetComponent<Image>().color,
        new Color(4 / 255.0f, 176 / 255.0f, 177 / 255.0f ),8*Time.deltaTime);
      }   
      if (true_color){
        HeadPanel.GetComponent<Image>().color = Color.Lerp(HeadPanel.GetComponent<Image>().color,
        new Color(2 / 255.0f, 167 / 255.0f, 98 / 255.0f ), 8 * Time.deltaTime);
      } 
      if (false_color){
        HeadPanel.GetComponent<Image>().color = Color.Lerp(HeadPanel.GetComponent<Image>().color,
        new Color(239 / 255.0f, 29 / 255.0f, 38 / 255.0f ),8*Time.deltaTime);
      }
      if (stateRunning == true){
      timeleft -= Time.deltaTime;
      timertext.text = Mathf.Round(timeleft).ToString();

     } 
    }
   public void OnClickPlay()
   {
     quation_list = new List<object>(category_lyst[categorys_int].quationslists);
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
        if(!timertext.gameObject.activeSelf){
        timertext.gameObject.SetActive(true);
        }
          timeleft = 60;
          stateRunning = true;
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
       stateRunning = false;
          if(!timertext.gameObject.activeSelf){
            timertext.gameObject.SetActive(false);
            }
      stateRunning = true;
      defoult_color = false;
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
         true_color = true;
         true_false_text.gameObject.SetActive(true);
         true_false_icons.sprite = icons[0];
         true_false_text.text = "Правильна відповідь";
         yield return new WaitForSeconds(1);
         true_false_icons.gameObject.GetComponent<Animator>().SetTrigger("Out");
         true_false_text.gameObject.SetActive(false);
         quation_list.RemoveAt(rand_quattion);
         yield return new WaitForSeconds(1f);
         Quation_Generate();
         true_color = false;
         defoult_color = true;
         yield break;
       }else {
         false_color = true;
         true_false_text.gameObject.SetActive(true);
         true_false_icons.sprite = icons[1];
         true_false_text.text = "Відповідь не правильна";
         yield return new WaitForSeconds(1);
         true_false_icons.gameObject.GetComponent<Animator>().SetTrigger("Out");
         true_false_text.gameObject.SetActive(false);
         HeadPanel.GetComponent<Animator>().SetTrigger("Out");
         false_color = false;
         defoult_color = true;
         yield break;
         }
     }  
    
   public void Answers_button(int index){
       if(answers_text[index].text.ToString() == current_quation.answers[0]){
         StartCoroutine(true_false(true));
       }else StartCoroutine(true_false(false));
   }
   public void Dropdown(int index){
   categorys_int = index;
  }
}


[System.Serializable]
public class Quations 
{
    public string quation;
    public string[] answers = new string [3];
}
[System.Serializable]
public class CategoryList{
  public string categoryName;
  public Quations[] quationslists;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
     private Queue<string> sentences; // will hold all the data of the dialogue

    public Text nameText;
    public Text dialogueText;
   
   public Animator animator; // control on the close-open animation of the dialogue box

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue){
        animator.SetBool ("Is_Open", true); // will open the dialogue window
        nameText.text = dialogue.name;

        sentences.Clear(); // clearing the sentences from previous conversation
        
        // queue up a sentence
        foreach(string sentenece in dialogue.sentences){
            sentences.Enqueue(sentenece);
        }

        DisplayNextSentence();// display sentence
    }

    // will return the first sentence (the top in the current queue) in the sentences queue
    public void DisplayNextSentence(){
        // check if there are any sentences in the queue
        if(sentences.Count==0 ) // we got to the end of the queue
        {        
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        // to be sure we stop animating before starting animating new sentence . if running will stop it than we can start a new one
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // creating that the text will be shown letter by letter
    IEnumerator TypeSentence(string sentence){
        dialogueText.text = "";
        // now we will pass over every letter individual 
        foreach(char letter in sentence.ToCharArray()){
            if(Input.GetKey(KeyCode.Mouse0)){
                dialogueText.text=sentence;
                // break;
                yield return new WaitForSeconds((float)0.039);
                break;
            }
            dialogueText.text+=letter; // this will apped the letter at the end of the string
            yield return new WaitForSeconds((float)0.039);
            //yield return null ; // waiting a small amout of time (a single frame) after every letter
        }
    }

    void EndDialogue(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("main_menu");
        // animator.SetBool ("IsOpen", false); // will close the dialogue window
    }
}

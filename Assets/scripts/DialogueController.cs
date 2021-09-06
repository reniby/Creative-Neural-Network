using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    public Queue<string> sentences; //first in first out
    public Text nameText;
    public Text diaText;

    public bool tutorial = false;

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("IsOpen", true);

        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);

        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        diaText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            diaText.text += letter;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
        }
    }

    void EndDialogue()
    {
        anim.SetBool("IsOpen", false);
        if (tutorial)
        {
            PlayerCores.instance.numCores += 3;
            tutorial = false;
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("RestartScreen");
    }


}

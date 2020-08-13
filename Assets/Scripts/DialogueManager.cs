using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text Name;
    public Text DialougeText;

    public Animator Anim;

    public bool DiaDone;
    //public PlayerBinput Player;

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        DiaDone = false;
        sentences = new Queue<string>();
	}
	
    public void StartDialogue (Dialogue dialogue)
    {
        //Player.LockAllInput(true, true);
        Anim.SetBool("Open", true);
        Name.text = dialogue.charname;
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
        DialougeText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("Reached End");
        DiaDone = true;
        //Player.LockAllInput(false, false);
        Anim.SetBool("Open", false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCharacterActivator : MonoBehaviour {

    Transform PlayerPlacement;
    //public PlayerBhysics Player;
    //Act_Deact_Cases Cases;
    public DialogueInteract Dio;
    public Dialogue dialogue;
    public DialogueManager DIODA;
    int APresses = 0;

    public float Distance;

    // Use this for initialization
    void Start () {
        PlayerPlacement = GameObject.FindWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(PlayerPlacement.position, transform.position) < Distance)
        {
            if (Input.GetButton("A"))// && Player.Grounded)
            {
                //Player.DontJump = true;
                APresses += 1;
                //Dio.TriggerDialogue();
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            if (Input.GetButton("A"))// && Player.Grounded && (APresses > 1))
            {
                DIODA.DisplayNextSentence();
            }
        }
        if (DIODA.DiaDone)
        {
            //Player.DontJump = false;
        }

    }
}

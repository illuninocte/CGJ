﻿/*Created by CaelumLaron*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    [SerializeField] Text textField = null;
    [SerializeField] Canvas canvas = null;
    [SerializeField] int fontSize = 0;

	//Data structure
    bool withoutD = false;
    Queue<int> orderQ;
	Queue<string> sentencesQ;
    Transform[] pos;
    Vector3[] offSetPos;
    GameObject speaker;
    
    void Start(){
        orderQ = new Queue<int>();
    	sentencesQ = new Queue<string>();
        speaker = null;
    }

    void Update(){
        if(Input.GetAxis("Submit") == 1 && orderQ.Count > 0)
            DisplayNextSentence();
    }

    public void StartChat(int[] order, string[] chat, Transform[] chater, Vector3[] offSet, GameObject speak){
        for(int i=0; i<order.Length; ++i){
            orderQ.Enqueue(order[i]);
            sentencesQ.Enqueue(chat[i]);
        }
        pos = chater;
        offSetPos = offSet;
        speaker = speak;
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(withoutD == true)
            return;
    	if(sentencesQ.Count == 0){
    		EndDialogue();
    		return;
    	}
        int index  = orderQ.Dequeue();
    	string sentence = sentencesQ.Dequeue();
    	StartCoroutine(TypeSentence(index, sentence));
    }

    IEnumerator TypeSentence(int index, string sentence){
        withoutD = true;
    	Vector3 wantedPos = pos[index].position + offSetPos[index];
        Text dialogueText = Instantiate(textField) as Text;
        dialogueText.fontSize = fontSize;
        dialogueText.color = Color.yellow;
        dialogueText.transform.SetParent(canvas.transform);
        dialogueText.transform.position = Camera.main.WorldToScreenPoint(wantedPos);
        dialogueText.text = "";
    	foreach(char letter in sentence.ToCharArray()){
    		dialogueText.text += letter;
    		yield return null; 
    	}
        yield return new WaitForSeconds(1);
        Destroy(dialogueText.transform.gameObject);
        withoutD = false;
        if(orderQ.Count == 0)
            EndDialogue();
    }

    private void EndDialogue(){
    	Debug.Log("End of conversation!");
        if(speaker)
            speaker.GetComponent<player>().FreeMove();
        speaker = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Text things")]
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public bool inDialogue = false;
    public bool alreadyTalked = false;

    private PlayerMovement player;
    public int index;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        textComponent.text = string.Empty;
        alreadyTalked = false;
        StartDialogue();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //left click skips line 
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        //player.canMove = false;
        inDialogue = true;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed); //the lower textSpeed, the faster it shows (0.1 seems fine)
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else //ending dialogue
        {
            alreadyTalked = true;
            inDialogue = false;
            index = 0; //TODO: fix dialogue restarting with last line
            gameObject.SetActive(false);
        }

    }
}

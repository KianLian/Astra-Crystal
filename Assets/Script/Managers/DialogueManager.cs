using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance { get; private set; } = null;

    public static bool CanNext = false;
    public static bool IsTalking = false;
    public static bool IsTheDialogueBeforeFight = false;
    public static bool IsTheLastDialogue = false;
    public static bool IsTheDialogueWhenEnteringInCave = false;

   [SerializeField]
    private Text characterName = null;

    [SerializeField]
    private Text dialogues = null;

    [SerializeField]
    private GameObject faceCharacter = null;
    [SerializeField]
    private GameObject lastPicked = null;

    [SerializeField]
    private Queue<string> sentences;
    [SerializeField]
    private Queue<string> names;
    [SerializeField]
    private Queue<GameObject> image;


    [SerializeField]
    private Animator imageAnimator = null;
    [SerializeField]
    private float typeVelocity = 0.5f;
    [SerializeField]
    private float timeToFinishName = 3f;
    [SerializeField]
    private float timeToGoToFinishDialogue = 4f;
    [SerializeField]
    private float endOfAnimation = 0.5f;

    private bool canPutTheName = false;

    [SerializeField]
    private GameObject lifebar = null;

    [SerializeField]
    private GameObject[] dialogueBoxToSetNotActive = null;

    [Header("Platforms")]
    [SerializeField]
    private GameObject[] platforms = null;
    [SerializeField]
    private GameObject boos = null;
    [SerializeField]
    private float timeToShowUp = 6f;
    [SerializeField]
    private GameObject dialogueTrigged = null;

    [SerializeField]
    private Animator qylaxAnimator = null;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        lifebar = GameObject.FindGameObjectWithTag("LifeBar");
        sentences = new Queue<string>();
        names = new Queue<string>();
        image = new Queue<GameObject>();
    }

    public IEnumerator StartTheDialogue(Dialogue dialogue)
    {
        if(dialogue != null)
        {
                IsAstraTalking();
                sentences.Clear();

                lifebar.SetActive(false);
                imageAnimator.SetBool("DialogueAtcivated", true);

            yield return new WaitForSecondsRealtime(endOfAnimation);

            for (int i = 0; i < dialogue.Name.Length; i++){
               
          
                image.Enqueue(dialogue.ImageCharacter[i]);
                names.Enqueue(dialogue.Name[i]);
                sentences.Enqueue(dialogue.Sentences[i]);

                //if (i > 0 )
                //{
                //    lastPicked = dialogue.ImageCharacter[i];
                //}


            }
            ShowNextSentence();
        }

    }

    public void ShowNextSentence()
    {
       if(sentences.Count == 0)
        {
            DialogueEnd();
                return;
        }


        if (lastPicked != null)
        {
            lastPicked.SetActive(false);
        }

        GameObject images = image.Dequeue();
        faceCharacter = images;  
        faceCharacter.SetActive(true);
        lastPicked = faceCharacter;

        string nameChar = names.Dequeue();
        StartCoroutine(TypeName(nameChar));

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSetence(sentence));
    }

    private void DialogueEnd()
    {
        CanNext = false;

        imageAnimator.SetBool("DialogueAtcivated", false);
        lifebar.SetActive(true);

        Invoke(nameof(SetImageNotActive), 2f);

        if(IsTheDialogueWhenEnteringInCave)
        {
            for (int i = 0; i < platforms.Length; i++)  
            {
                platforms[i].GetComponent<Platform>().JustMove = true;
            }
            qylaxAnimator.SetTrigger("CanVanish");
            dialogueTrigged.SetActive(true);
           
        }

        if(IsTheDialogueBeforeFight)
        {
            Invoke(nameof(CallBoss), timeToShowUp);
        }

        if(IsTheLastDialogue)
        {
            UIManager.Instance.PlayerWin();
            Cursor.visible = true;
        }
        IsAstraTalking();
    }


    private void CallBoss()
    {
        boos.GetComponent<BossScrip>().CanShowUp();
    }

    private IEnumerator TypeSetence(string sentence)
    {
        dialogues.text = "";

     //   yield return new WaitForSeconds(timeToFinishName);
        foreach (char letter in sentence.ToCharArray())
        {
            dialogues.text += letter;
            yield return new WaitForSecondsRealtime(typeVelocity);
        }

        CanNext = true;
    }

    private IEnumerator TypeName(string nameChar)
    {
        CanNext = false;

        char[] arr = nameChar.ToCharArray();
        timeToFinishName = arr.Length;

        characterName.text = "";

         foreach (char letter in nameChar.ToCharArray())
        {
            characterName.text += letter;
            yield return new WaitForSecondsRealtime(typeVelocity);
        }
    }

    private void SetImageNotActive()
    {
        for (int i = 0; i < dialogueBoxToSetNotActive.Length; i++)
        {
            dialogueBoxToSetNotActive[i].SetActive(false);
        }
    }
    private void IsAstraTalking()
    {
        IsTalking = !IsTalking;
        Debug.Log(IsTalking);
    }
}

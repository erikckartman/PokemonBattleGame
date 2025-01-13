using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField] private List<CharacterDialogue> characters;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private AnimScript animScript;

    [SerializeField] private string characterName;
    private GameObject player;
    [HideInInspector]public bool isTalking = false;

    private void Awake()
    {
        UpdatePlayer();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdatePlayer();
        }

        if (dialogueSystem.talkAnim)
        {
            animScript.stateInt = 3;
            player.GetComponent<AnimScript>().stateInt = 3;
        }
        else
        {
            animScript.stateInt = 1;
        }

        if (player != null && gameObject.tag == "NPC")
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= 1f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CharacterDialogue character = GetCharacterDialogue(characterName);

                    if (character != null)
                    {
                        DialogueProgress dialogueProgress = GetDialogueProgress(character);

                        if (dialogueProgress != null)
                        {
                            
                            if (dialogueProgress.progress < character.dialogues.Count)
                            {
                                Dialogue dialogue = character.dialogues[dialogueProgress.progress];
                                dialogueSystem.lines = dialogue.text;
                                dialogueSystem.StartDialogue();

                                dialogueProgress.progress++;
                            }
                            else
                            {

                                Debug.Log($"No more dialogues with {characterName}.");
                            }
                        }
                    }
                }
            }
            else
            {
                isTalking = false;
            }
        }
    }

    private void UpdatePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private CharacterDialogue GetCharacterDialogue(string characterName)
    {
        return characters.Find(c => c.characterName == characterName);
    }

    private DialogueProgress GetDialogueProgress(CharacterDialogue character)
    {
        return character.dialogueProgress;
    }

    [System.Serializable]
    public class Dialogue
    {
        public string[] text;
    }

    [System.Serializable]
    public class DialogueProgress
    {
        public int progress = 0;
    }

    [System.Serializable]
    public class CharacterDialogue
    {
        public string characterName;
        public List<Dialogue> dialogues;
        public DialogueProgress dialogueProgress;
    }
}

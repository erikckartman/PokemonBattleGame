using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField] private List<CharacterDialogue> characters;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private AnimScript animScript;

    [SerializeField] private string characterName;
    public GameObject player;
    [HideInInspector]public bool isTalking = false;
    public float distance;

    private void Update()
    {              
        if (player != null && gameObject.tag == "NPC")
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            distance = distanceToPlayer;

            if (distanceToPlayer <= 1f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CharacterDialogue character = characters.Find(c => c.characterName == player.GetComponent<NPCScript>().characterName);
               
                    if (character != null)
                    {
                        Dialogue dialogueProgress = character.dialogues.Find(d => d.progress == LevelSystems.progress);

                        if (dialogueProgress != null)
                        {

                            Dialogue dialogue = character.dialogues[dialogueProgress.progress];
                            dialogueSystem.lines = dialogue.text;
                            dialogueSystem.StartDialogue();
                        }
                        else
                        {
                            Debug.LogError($"{dialogueProgress} is null");
                        }
                    }
                    else
                    {
                        Debug.LogError($"{character} is null");
                    }
                }
            }
            else
            {
                isTalking = false;
            }
        }

        if (dialogueSystem.talkAnim)
        {
            animScript.stateInt = 3;
            player.GetComponent<AnimScript>().stateInt = 3;
        }
        else
        {
            if(gameObject.tag == "NPC")
            {
                animScript.stateInt = 1;
            }
            else
            {
                return;
            }
        }
    }


    [System.Serializable]
    public class Dialogue
    {
        public string[] text;
        public int progress = 0;
    }

    [System.Serializable]
    public class CharacterDialogue
    {
        public string characterName;
        public List<Dialogue> dialogues;
    }
}

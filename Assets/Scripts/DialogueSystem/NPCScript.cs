using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private string characterName;
    [SerializeField] private string[] dialogueWithPeter;
    private GameObject player;

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

        if(player != null && gameObject.tag == "NPC")
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= 1f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (player.GetComponent<NPCScript>().characterName == "Peter")
                    {
                        if(dialogueWithPeter.Length > 0)
                        {
                            dialogueSystem.lines = dialogueWithPeter;
                        }
                        else
                        {
                            Debug.Log("Array is blank");
                        }

                        Debug.Log($"Dialogue lines updated. Total lines: {dialogueSystem.lines.Length}");
                        dialogueSystem.StartDialogue();
                    }
                }
            }
        }
    }

    private void UpdatePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}

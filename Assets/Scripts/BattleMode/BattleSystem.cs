using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOOSE }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject chooseAttack;
    public GameObject changePokemon;

    GameObject player;
    GameObject enemy;

    Fighter playerUnit;
    FighterEnemy enemyUnit;

    public Transform playerPos;
    public Transform enemyPos;

    Vector2 playerPosition;
    Vector2 enemyPosition;
    Vector2 playerAttackPos;
    Vector2 enemyAttackPos;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public TextMeshProUGUI startMessege;

    public GameObject attackButton;
    public GameObject runButton;
    public GameObject changeButton;

    public GameObject fireball;

    [HideInInspector] private bool plr = true;
    [HideInInspector] private bool enm = false;

    public Transform camera;
    private Vector3 cam1p = new Vector3 (0, 1.63f, -11.31f);
    private Vector3 cam1r = new Vector3(6.493f, 0, 0);
    private Vector3 cam2p = new Vector3(27.32f, 18.02f, -5.91f);
    private Quaternion cam2r = Quaternion.Euler(6.493f, -90f, 0);
    public GameObject canvas;

    private void Awake()
    {
        camera.transform.position = cam2p;
        camera.transform.rotation = cam2r;
    }
    void Start()
    {
        playerPosition = playerPos.position;
        enemyPosition = enemyPos.position;
        playerAttackPos = playerPosition + new Vector2(0.5f, 0);
        enemyAttackPos = enemyPosition - new Vector2(0.5f, 0);

        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

        Tweener moveTween = camera.DOMove(cam1p, 3f);
        Tweener rotateTween = camera.DORotate(cam1r, 3f);

        Sequence sequence = DOTween.Sequence()
                                   .Append(camera.DOMove(cam2p, 2f))
                                   .Join(camera.DOMove(cam1r, 2f))
                                   .OnComplete(AnimComplete); 
        sequence.Play();
        
    }

    private void AnimComplete()
    {
        state = BattleState.START;
        StartCoroutine(StartBattle());
    }

    void Update()
    {
        if(state == BattleState.PLAYERTURN)
        {
            attackButton.SetActive(true);
            runButton.SetActive(true);
            changeButton.SetActive(true);
        }
        else
        {
            attackButton.SetActive(false);
            runButton.SetActive(false);
            changeButton.SetActive(false);
        }

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetEnemyHUD(enemyUnit);
    }

    IEnumerator StartBattle()
    {
        player = Instantiate(playerPrefab, playerPos);
        playerUnit = player.GetComponent<Fighter>();
        

        enemy = Instantiate(enemyPrefab, enemyPos);
        enemyUnit = enemy.GetComponent<FighterEnemy>();
        playerUnit.SpawnPokemon();
        enemyUnit.SpawnPokemon();

        startMessege.text = enemyUnit.name + " attacks you...";
        canvas.SetActive(true);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public void PlayerTurn()
    {
        startMessege.text = "Choose an action:";
    }

    public void ChooseAttack()
    {
        if(state == BattleState.PLAYERTURN)
            chooseAttack.SetActive(true);
    }

    public void OnAttackButton()
    {
        if (state == BattleState.PLAYERTURN)
        {
            chooseAttack.SetActive(false);
            StartCoroutine(PlayerAttacks());
        }
        else
        {
            return;
        }
    }

    public void OnPoisonButton()
    {
        if (state == BattleState.PLAYERTURN)
        {
            chooseAttack.SetActive(false);
            StartCoroutine(PlayerPoison());
        }
        else
        {
            return;
        }
    }

    public void CloseCA()
    {
        chooseAttack?.SetActive(false);
    }

    IEnumerator PlayerAttacks()
    {
        startMessege.text = playerUnit.name + " uses attack!";
        yield return new WaitForSeconds(2f);
        player.transform.DOMove(playerAttackPos, 0.2f).OnComplete(() =>
        {
            player.transform.DOMove(playerPosition, 0.5f);
        });
        
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage - enemyUnit.defence);
        startMessege.text = "It's succesful!";
        yield return new WaitForSeconds(3f);

        if (isDead)
        {
            state = BattleState.WIN;
            Ending();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerPoison()
    {
        startMessege.text = playerUnit.name + " uses poison!";
        yield return new WaitForSeconds(2f);

        GameObject hadoken = Instantiate(fireball, playerPos);
        hadoken.transform.localScale = new Vector2(1, 1);
        hadoken.transform.DOMove(enemyPos.position, 1f).OnComplete(() =>
        {
            Destroy(hadoken);
        });

        bool isDead = enemyUnit.TakeDefence(playerUnit.poisoning);
        startMessege.text = "It's succesful!";
        yield return new WaitForSeconds(3f);

        if (isDead)
        {
            state = BattleState.LOOSE;
            Ending();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        startMessege.text = enemyUnit.name + " chooses move!";
        yield return new WaitForSeconds(2f);
        int randomValue = Random.Range(1, 101);

        if (randomValue <= 50)
        {
            StartCoroutine(EnemyAttack());
        }
        else if (randomValue > 50)
        {
            StartCoroutine(EnemyPoison());
        }
        else
        {
            startMessege.text = "ERROR!!!";
        }
    }

    IEnumerator EnemyAttack()
    {
        startMessege.text = enemyUnit.name + " uses attack!";
        
        enemy.transform.DOMove(enemyAttackPos, 0.2f).OnComplete(() =>
        {
            enemy.transform.DOMove(enemyPosition, 0.5f);
        });

        startMessege.text = "It's succesful!";
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage - playerUnit.defence);

        yield return new WaitForSeconds(3f);
        if (isDead)
        {
            state = BattleState.LOOSE;
            Ending();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EnemyPoison()
    {
        startMessege.text = enemyUnit.name + " uses poison!";
        yield return new WaitForSeconds(2f);

        GameObject hadoken = Instantiate(fireball, enemyPos);
        hadoken.transform.localScale = new Vector2(-1, 1);
        hadoken.transform.DOMove(playerPos.position, 1f).OnComplete(() =>
        {
            Destroy(hadoken);
        });

        bool isDead = playerUnit.TakeDefence(enemyUnit.poisoning);
        startMessege.text = "It's succesful!";
        yield return new WaitForSeconds(3f);
        if (isDead)
        {
            state = BattleState.WIN;
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void Ending()
    {
        if(state == BattleState.WIN)
        {
            StartCoroutine(Win());
        }
        else if(state == BattleState.LOOSE)
        {
            StartCoroutine(Loose());
        }
    }

    IEnumerator Win()
    {
        Destroy(enemy);
        startMessege.text = playerUnit.name + " wins in this battle";
        yield return new WaitForSeconds(2f);
        playerUnit.level += 1;
        startMessege.text = "Congratulations! You're winner!";
        LevelSystems.progress = LevelSystems.progress + 1;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level");
    }

    IEnumerator Loose()
    {
        Destroy(player);
        startMessege.text = enemyUnit.name + " wins in this battle";
        yield return new WaitForSeconds(2f);
        enemyUnit.level += 1;
        startMessege.text = "You're looser!";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level");
    }

    public void Run()
    {
        startMessege.text = "You escape the battle";
        StartCoroutine(RunFromBattle());
    }

    IEnumerator RunFromBattle()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level");
    }

    public void OpenCPP()
    {
        changePokemon.SetActive(true);
    }

    public void ChoosePikachu()
    {
        if(playerUnit.number == 1)
        {
            changePokemon.SetActive(false);
        }
        else
        {
            playerUnit.number = 1;
            playerUnit.SetPokemon();
            playerUnit.DestroyPokemon();
            playerUnit.SpawnPokemon();
            changePokemon.SetActive(false);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void ChooseSquirtle()
    {
        if (playerUnit.number == 2)
        {
            changePokemon.SetActive(false);
        }
        else
        {
            playerUnit.number = 2;
            playerUnit.SetPokemon();
            playerUnit.DestroyPokemon();
            playerUnit.SpawnPokemon();
            changePokemon.SetActive(false);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void ChooseChermander()
    {
        if (playerUnit.number == 3)
        {
            changePokemon.SetActive(false);
        }
        else
        {
            playerUnit.number = 3;
            playerUnit.SetPokemon();
            playerUnit.DestroyPokemon();
            playerUnit.SpawnPokemon();
            changePokemon.SetActive(false);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void ChoosePidgeotto()
    {
        if (playerUnit.number == 4)
        {
            changePokemon.SetActive(false);
        }
        else
        {
            playerUnit.number = 4;
            playerUnit.SetPokemon();
            playerUnit.DestroyPokemon();
            playerUnit.SpawnPokemon();
            changePokemon.SetActive(false);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterHelthandSteminaSystem : MonoBehaviour
{
    [Header("체력")]
    [SerializeField] private int playerMaxHelth = 10000;
    [SerializeField] private int playerCurrnetHelth = 0;
    public Transform playerSpawnPoint;

    [Header("스테미나")]
    [SerializeField] private int playerMaxStemina = 10000;
    [SerializeField] private int playerCurrentStemina = 0;

    [Header("플레이어 사망")]
    public Collider playerCollider;
    public CharacterMoveMentSystem _characterMoveMentSystem;
    public UIManager _UIManager;
    public Camera playerDieCamera;
    public bool isPlayerDie;


    private void Start()
    {
        Reload();
        //_UIManager = Object.FindAnyObjectByType<UIManager>();
        _characterMoveMentSystem = Object.FindAnyObjectByType<CharacterMoveMentSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(DiePlayer());
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ReSpawn();
        }

        if (playerCurrnetHelth <= 0)
            StartCoroutine(DiePlayer());
    }

    void Reload()
    {
        playerCurrnetHelth = playerMaxHelth;
        playerCurrentStemina = playerMaxStemina;
    }
    
    public void MinusHelth(int EnemyDamage)
    {
        playerCurrnetHelth -= EnemyDamage;
    }

    public void MinusStenima(int minusStenima)
    {
        playerCurrentStemina -= minusStenima;
    }

    IEnumerator DiePlayer()
    {
        Debug.Log("플레이어 사망");
        isPlayerDie = true;
        playerDieCamera.gameObject.SetActive(true);
        _characterMoveMentSystem.characterAnimator.SetBool("IsDie", true);

        yield return new WaitForSeconds(0.5f);
        playerCollider.enabled = false;

        yield return new WaitForSeconds(0.5f);
        //_UIManager.OnDieUI();
    }

    public void ReSpawn()
    {
        _characterMoveMentSystem.characterAnimator.SetBool("IsDie", false);
        playerDieCamera.gameObject.SetActive(false);
        playerCollider.enabled = true;
        //_UIManager.DIeUI.gameObject.SetActive(false);
        transform.position = new Vector3(playerSpawnPoint.position.x, playerSpawnPoint.position.y, playerSpawnPoint.position.z);
        isPlayerDie = false;
    }
}

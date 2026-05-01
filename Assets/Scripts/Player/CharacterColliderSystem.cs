using UnityEngine;

public class CharacterColliderSystem : MonoBehaviour
{
    public GameManager _gameManager;
    public CharacterMoveMentSystem _characterMoveMentSystem;

    private void Start()
    {
        _gameManager = Object.FindAnyObjectByType<GameManager>();
        _characterMoveMentSystem = GetComponent<CharacterMoveMentSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _characterMoveMentSystem.isGound = true;
            _characterMoveMentSystem.isJump = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bad"))
        {
            if(_gameManager.isAfternoon)
            {
                Debug.Log("밤에만 잠을 잘 수 있음니다.");
            }
            else
            {
                Debug.Log("잠자기 켜기");
            }
        }

        if (other.gameObject.CompareTag("bench"))
        {
            Debug.Log("작업대 안내 UI 오픈");
        }

        if (other.gameObject.CompareTag("Fire"))
        {
            Debug.Log("화상 입음 Helth 감소 진행");
        }

        if (other.gameObject.CompareTag("SafeZone"))
        {
            Debug.Log("안전 구역 진입");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Debug.Log("Helth 감소 종료");
        }

        if (other.gameObject.CompareTag("bench"))
        {
            Debug.Log("작업대 안내 UI 끄기");
        }

        if (other.gameObject.CompareTag("Bad"))
        {
            Debug.Log("침대 안내 UI 끄기");
        }

        if (other.gameObject.CompareTag("SafeZone"))
        {
            Debug.Log("위험 구역 진입");
        }
    }
}

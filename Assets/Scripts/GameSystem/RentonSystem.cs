using UnityEngine;

public class RentonSystem : MonoBehaviour
{
    public GameManager _gameManager;
    public GameObject RentonOnGameObject;
    public GameObject RentonOffGameObject;

    private void Start()
    {
        _gameManager = Object.FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if (_gameManager.isAfternoon) 
        {
            RentonOffGameObject.gameObject.SetActive(true);
            RentonOnGameObject.gameObject.SetActive(false);
            Debug.Log("렌턴 OFF");
        }
        else
        {
            RentonOnGameObject.gameObject.SetActive(true);
            RentonOffGameObject.gameObject.SetActive(false);
            Debug.Log("렌턴 On");
        }
    }

}

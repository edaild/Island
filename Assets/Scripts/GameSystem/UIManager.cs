using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public CharacterHelthandSteminaSystem _characterHelthandSteminaSystem;

    [Header("플레이어 체력 관련")]
    public Slider playerHelthSlider;
    public TextMeshProUGUI playerHelthText;

    [Header("플레이어 스테미나 관련")]
    public Slider playerSteminaSlider;

    [Header("플레이어 사망 관련")]
    public GameObject DIeUI;
    public Button ReSpawnButton;

    public void Start()
    {
         ReSpawnButton.onClick.AddListener(_characterHelthandSteminaSystem.ReSpawn);
        _characterHelthandSteminaSystem = FindAnyObjectByType<CharacterHelthandSteminaSystem>();
    }

    public void OnDieUI()
    {
        DIeUI.gameObject.SetActive(true);
    }
}

using UnityEditor.Build.Content;
using UnityEngine;

public class SunSystem : MonoBehaviour
{
    public GameManager _gameManager;
    public float SunRotageSpeed = 10f;
    public float sunAngleX;

    public void Start()
    {
        _gameManager = Object.FindAnyObjectByType<GameManager>();
    }

    public void Update()
    {
        SunRotate();
        ConvertAngleToTime();
    }

    private void SunRotate()
    {
        sunAngleX += SunRotageSpeed * Time.deltaTime;
        sunAngleX = Mathf.Repeat(sunAngleX, 360f);
        transform.rotation = Quaternion.Euler(sunAngleX, 0f, 0f);
    }

    private void ConvertAngleToTime()
    {
        float time = (sunAngleX / 360f) * 24f;

        time += 6f;
        time = Mathf.Repeat(time, 24f);
        _gameManager.currentGameTime = time;

    }
}

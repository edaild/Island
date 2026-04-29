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
        transform.Rotate(Vector3.right * SunRotageSpeed * Time.deltaTime);
        sunAngleX = transform.eulerAngles.x;
    }

    private void ConvertAngleToTime()
    {
        float time = (sunAngleX / 360f) * 24f;

        time += 6f;

        if (time >= 24f) time -= 24f;
        _gameManager.currentGameTime = time;

    }
}

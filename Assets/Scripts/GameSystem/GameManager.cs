using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float currentGameTime;
    public bool isAfternoon;

    private void Update()
    {
        if (currentGameTime >= 7 && currentGameTime <= 18) isAfternoon = true;
        else isAfternoon = false;
    }
}

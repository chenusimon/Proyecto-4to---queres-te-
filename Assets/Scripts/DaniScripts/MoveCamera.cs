using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform player;

    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        transform.position = player.transform.position;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private List<Vector2> balloonPositions = new List<Vector2>();

    void Start()
    {
        NetworkServerProcessing.SetGameLogic(this);
    }

    void Update()
    {
        if (Time.time % 1 < Time.deltaTime) // Spawn a balloon every second
        {
            SpawnBalloon();
        }
    }

    private void SpawnBalloon()
    {
        float screenPositionXPercent = UnityEngine.Random.Range(0.0f, 1.0f);
        float screenPositionYPercent = UnityEngine.Random.Range(0.0f, 1.0f);
        Vector2 screenPosition = new Vector2(screenPositionXPercent * Screen.width, screenPositionYPercent * Screen.height);
        balloonPositions.Add(screenPosition);

        string message = $"{ServerToClientSignifiers.SpawnBalloon},{screenPosition.x},{screenPosition.y}";
        foreach (var connectionId in NetworkServerProcessing.GetAllClientIDs())
        {
            NetworkServerProcessing.SendMessageToClient(message, connectionId, TransportPipeline.ReliableAndInOrder);
        }
    }

    public void BalloonPopped(int balloonIndex, int clientID)
    {
        if (balloonIndex < 0 || balloonIndex >= balloonPositions.Count) return;

        balloonPositions.RemoveAt(balloonIndex);

        string message = $"{ServerToClientSignifiers.BalloonPopped},{balloonIndex}";
        foreach (var connectionId in NetworkServerProcessing.GetAllClientIDs())
        {
            NetworkServerProcessing.SendMessageToClient(message, connectionId, TransportPipeline.ReliableAndInOrder);
        }
    }
}

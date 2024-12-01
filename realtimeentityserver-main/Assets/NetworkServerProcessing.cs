using System.Collections.Generic;

static public class NetworkServerProcessing
{
    static NetworkServer networkServer;
    static GameLogic gameLogic;

    static public void ReceivedMessageFromClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

        if (signifier == ClientToServerSignifiers.BalloonPopped)
        {
            int balloonIndex = int.Parse(csv[1]);
            gameLogic.BalloonPopped(balloonIndex, clientConnectionID);
        }
    }

    static public List<int> GetAllClientIDs()
    {
        return new List<int>(networkServer.GetAllClientIDs());
    }

    static public void SendMessageToClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        networkServer.SendMessageToClient(msg, clientConnectionID, pipeline);
    }

    static public void ConnectionEvent(int clientConnectionID)
    {
        UnityEngine.Debug.Log("Client connected with ID: " + clientConnectionID);
    }

    static public void DisconnectionEvent(int clientConnectionID)
    {
        UnityEngine.Debug.Log("Client disconnected with ID: " + clientConnectionID);
    }

    static public void SetNetworkServer(NetworkServer NetworkServer)
    {
        networkServer = NetworkServer;
    }

    static public NetworkServer GetNetworkServer()
    {
        return networkServer;
    }

    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }
}

public enum TransportPipeline
{
    NotIdentified,
    ReliableAndInOrder,
    FireAndForget
}

static public class ClientToServerSignifiers
{
    public const int BalloonPopped = 1;
}

static public class ServerToClientSignifiers
{
    public const int SpawnBalloon = 1;
    public const int BalloonPopped = 2;
}

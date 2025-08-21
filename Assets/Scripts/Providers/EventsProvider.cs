using System;
using UnityEngine;

public static class EventsProvider
{
    public static class NetworkEvents
    {
        public static Action OnHostStarted;
        public static Action OnClientStarted;
    }
    
    public static class GameplayEvents
    {
        public static Action<Vector3> OnMovementDirection;
        public static Action OnSendMessage;
        public static Action <string> OnDisplayMessage;
        public static Action OnCubeSpawn;
    }
}

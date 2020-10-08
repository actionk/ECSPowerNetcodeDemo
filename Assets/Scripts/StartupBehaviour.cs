using Plugins.ECSPowerNetcode.Client;
using Plugins.ECSPowerNetcode.Server;
using UnityEngine;

public class StartupBehaviour : MonoBehaviour
{
    public void Start()
    {
        ServerManager.Instance.StartServer(9090);
        ClientManager.Instance.ConnectToServer(9090);
    }
}
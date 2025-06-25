using Unity.Netcode;
using UnityEngine;

public class PlayerCarSpawner : NetworkBehaviour
{
    //public GameObject carPrefab; // Assign in inspector or dynamically
    private GameObject myCar;
    public int numPlayers = 0;
    public int playerCount = 2; //for more players increase this

    public void Awake()
    {
        Debug.Log(GameState.Instance.gamemode);
    }
    public override void OnNetworkSpawn()
    {
        NewPlayerConnected();
        
        if (IsOwner && IsClient)
        {

            myCar = gameObject;
            //Waiting for raceCondition
            base.Invoke("resetCar", 0.1f);

            GameController.Instance.SetCurrentCar(myCar);
        }
    }
    private void Update()
    {
        //myCar.transform.SetPositionAndRotation(GameController.Instance.startPos.position, GameController.Instance.startPos.rotation);
        //Debug.Log(myCar.transform.position);

    }
    public void NewPlayerConnected() 
    {
        if (IsHost) 
        {
            if (NetworkManager.Singleton.ConnectedClients.Count == 2) 
            {
                //GameObject.Find("ScriptName").GetComponent<GameRPC>().StartRaceClientRpc();
                StartRaceClientRpc();

            }

        }
    }
    [ClientRpc]
    public void StartRaceClientRpc(ClientRpcParams clientRpcParams = default)
    {
        GameController.Instance.doLights();
    }

    private void resetCar()
    {
        myCar.transform.SetPositionAndRotation(GameController.Instance.startPos.position, GameController.Instance.startPos.rotation);
    }


}

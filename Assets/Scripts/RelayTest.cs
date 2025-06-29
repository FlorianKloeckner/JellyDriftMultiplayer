using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private TMP_InputField codeInputField;
    // Start is called before the first frame update
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => { Debug.Log("Signed IN "); };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    
    public void ConnectClientWithCode()
    {
        string code = codeInputField.text;
        code = code.Substring(0, 6);

        GameState.Instance.gamemode = Gamemode.Multiplayer;
        JoinRelay(code);
        //SceneManager.LoadScene("0"); //TODO get scene from connection to host and load correct scene
    }

    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(joinCode);
            GameController.Instance.SetJoinCode(joinCode);
            RelayServerData data = new RelayServerData(allocation, "dtls");
            
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);

            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }



    public async void JoinRelay(string code)
    {
        Debug.Log("Trying to connect with code: " + code);
        try
        {
            await RelayService.Instance.JoinAllocationAsync(code);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(code);

            RelayServerData relayServerdata = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerdata);

            NetworkManager.Singleton.StartClient();

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
            //return;
        }
        SceneManager.LoadScene("0");
        
    }

}

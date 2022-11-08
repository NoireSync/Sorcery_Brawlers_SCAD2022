using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Go_To_Server : MonoBehaviourPunCallbacks
{
    // Stores usernames
    [SerializeField] private TMP_InputField userNameInput;
    [SerializeField] private TMP_Text connectTxt;


    // Connects player to the lobby when button is clicked
    public void OnClickConnect()
    {
        // Check if username is at least 1 input long, if true, assign to username var
        if (userNameInput.text.Length >= 1)
        { 
            PhotonNetwork.NickName = userNameInput.text;
            connectTxt.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}

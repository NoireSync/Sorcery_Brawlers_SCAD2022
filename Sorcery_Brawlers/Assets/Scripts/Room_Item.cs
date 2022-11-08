using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Room_Item : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    Lobby_Manger manager;

    private void Start()
    {
        manager = FindObjectOfType<Lobby_Manger>();
    }

    public void SetRoomName(string _roomName)
    { 
        roomName.text = _roomName;
    }

    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }
}

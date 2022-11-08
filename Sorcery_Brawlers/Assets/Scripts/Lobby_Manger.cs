using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Lobby_Manger : MonoBehaviourPunCallbacks
{
    // Stores lobby names
    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private GameObject lobbyCanvas;
    [SerializeField] private GameObject roomCanvas;
    [SerializeField] private TMP_Text roomName;

    // Vars for scroll and created rooms
    [SerializeField] Room_Item roomItemPrefab;
    List<Room_Item> roomItemsList = new List<Room_Item>();
    [SerializeField] Transform contentObj;

    [SerializeField] private float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate()
    {
        // Check if room name is at least 1 input long, if true, create room with max 4 players
        if (roomNameInput.text.Length >= 1)
        {

            PhotonNetwork.CreateRoom(roomNameInput.text, new Photon.Realtime.RoomOptions() {MaxPlayers = 4} );
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyCanvas.SetActive(false);
        roomCanvas.SetActive(true);
        roomName.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }

    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (Room_Item item in roomItemsList)
        { 
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            Room_Item newRoom = Instantiate(roomItemPrefab, contentObj);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    { 
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    { 
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomCanvas.SetActive(false);
        lobbyCanvas.SetActive(true);
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); 
    }
}

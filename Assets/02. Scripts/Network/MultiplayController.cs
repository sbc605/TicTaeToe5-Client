using System;
using Newtonsoft.Json;
using SocketIOClient;
using UnityEngine;

// joinRoom/createRoom 이벤트 전달할 때 전달되는 정보의 타입
public class RoomData
{
    [JsonProperty("roomId")]
    public string roomId { get; set; }
}

// 상대방이 둔 마커 위치
public class BlockData
{
    [JsonProperty("blockIndex")]
    public int blockIndex { get; set; }
}

public class MultiplayController : IDisposable
{
    private SocketIOUnity _socket;

    // Room 상태 변화에 따른 동작을 할당하는 변수
    private Action<Constants.MultiplayControllerState, string> _multiplayStateChanged;

    // 마커의 위치 변화를 업데이트하는 변수
    public Action<int> blockDataChanged;

    public MultiplayController(Action<Constants.MultiplayControllerState, string> onMultiplayStateChanged)
    {
        // 서버에서 이벤트가 발생하면 처리할 함수를 onMultiplayStateChanged에 등록
        _multiplayStateChanged = onMultiplayStateChanged;

        // Socket.io 클라이언트 초기화
        var uri = new Uri(Constants.SocketServerURL);
        _socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        _socket.On("createRoom", CreateRoom);
        _socket.On("joinRoom", JoinRoom);
        _socket.On("startGame", StartGame);
        _socket.On("exitRoom", ExitRoom);
        _socket.On("endGame", EndGame);
        _socket.On("doOpponent", DoOpponent);
    }

    #region Server -> Client
    private void CreateRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _multiplayStateChanged?.Invoke(Constants.MultiplayControllerState.CreateRoom, data.roomId);

    }

    private void JoinRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _multiplayStateChanged?.Invoke(Constants.MultiplayControllerState.JoinRoom, data.roomId);
    }

    private void StartGame(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _multiplayStateChanged?.Invoke(Constants.MultiplayControllerState.StartGame, data.roomId);
    }

    private void ExitRoom(SocketIOResponse response)
    {
        _multiplayStateChanged?.Invoke(Constants.MultiplayControllerState.ExitRoom, null);
    }

    private void EndGame(SocketIOResponse response)
    {
        _multiplayStateChanged?.Invoke(Constants.MultiplayControllerState.EndGame, null);
    }

    private void DoOpponent(SocketIOResponse response)
    {
        var data = response.GetValue<BlockData>();
        blockDataChanged?.Invoke(data.blockIndex);
    }
    #endregion

    #region Client -> Server

    public void LeaveRoom(string roomId)
    {
        _socket.Emit("leaveRoom", new {roomId});
    }

    // 플레이어가 마커를 두면 호출
    public void DoPlayer(string roomId, int position)
    {
        _socket.Emit("doPlayer", new {roomId, position});
    }

    public void Dispose()
    {
        _socket.Disconnect();
        _socket.Dispose();
        _socket = null;
    }

    #endregion
}
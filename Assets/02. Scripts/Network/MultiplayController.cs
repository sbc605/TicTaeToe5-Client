using System;
using Newtonsoft.Json;
using SocketIOClient;
using UnityEngine;

// joinRoom/createRoom �̺�Ʈ ������ �� ���޵Ǵ� ������ Ÿ��
public class RoomData
{
    [JsonProperty("roomId")]
    public string roomId { get; set; }
}

// ������ �� ��Ŀ ��ġ
public class BlockData
{
    [JsonProperty("blockIndex")]
    public int blockIndex { get; set; }
}

public class MultiplayController : IDisposable
{
    private SocketIOUnity _socket;

    // Room ���� ��ȭ�� ���� ������ �Ҵ��ϴ� ����
    private Action<Constants.MultiplayControllerState, string> _multiplayStateChanged;

    // ��Ŀ�� ��ġ ��ȭ�� ������Ʈ�ϴ� ����
    public Action<int> blockDataChanged;

    public MultiplayController(Action<Constants.MultiplayControllerState, string> onMultiplayStateChanged)
    {
        // �������� �̺�Ʈ�� �߻��ϸ� ó���� �Լ��� onMultiplayStateChanged�� ���
        _multiplayStateChanged = onMultiplayStateChanged;

        // Socket.io Ŭ���̾�Ʈ �ʱ�ȭ
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

    // �÷��̾ ��Ŀ�� �θ� ȣ��
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
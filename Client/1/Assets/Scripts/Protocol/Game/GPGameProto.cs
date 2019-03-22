//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: GPGameProto.proto
// Note: requires additional types generated from: GameBase.proto
namespace Proto.GPGameProto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_ReqStartGame")]
  public partial class CMD_ReqStartGame : global::ProtoBuf.IExtensible
  {
    public CMD_ReqStartGame() {}
    
    private uint _roomid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roomid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint roomid
    {
      get { return _roomid; }
      set { _roomid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_RespStartGame")]
  public partial class CMD_RespStartGame : global::ProtoBuf.IExtensible
  {
    public CMD_RespStartGame() {}
    
    private readonly global::System.Collections.Generic.List<Proto.GameBase.GameUserInfo> _gameUser = new global::System.Collections.Generic.List<Proto.GameBase.GameUserInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"gameUser", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Proto.GameBase.GameUserInfo> gameUser
    {
      get { return _gameUser; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_PlayerInfo")]
  public partial class CMD_PlayerInfo : global::ProtoBuf.IExtensible
  {
    public CMD_PlayerInfo() {}
    
    private uint _seat;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"seat", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint seat
    {
      get { return _seat; }
      set { _seat = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_UpdateStep")]
  public partial class CMD_UpdateStep : global::ProtoBuf.IExtensible
  {
    public CMD_UpdateStep() {}
    
    private uint _step;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"step", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint step
    {
      get { return _step; }
      set { _step = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"GameCMD")]
    public enum GameCMD
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"CMD_GAME_REQSTARTGAME", Value=1)]
      CMD_GAME_REQSTARTGAME = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CMD_GAME_RESPSTARTGAME", Value=2)]
      CMD_GAME_RESPSTARTGAME = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CMD_GAME_PLAYERINFO", Value=3)]
      CMD_GAME_PLAYERINFO = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CMD_GAME_UPDATESTEP", Value=4)]
      CMD_GAME_UPDATESTEP = 4
    }
  
}
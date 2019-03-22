using ProtoBuf;
using System.IO;

namespace Proto.Cell
{
    
    //public abstract class Cell
    //{
    //    public abstract void Start(params object[] _params);
    //    public abstract void OnReceivePacket(object _object,byte[] buffer);

    //    protected T unPacket<T>(byte[] buffer)
    //    {
    //        MemoryStream ms = new MemoryStream(buffer);
    //        T p = Serializer.Deserialize<T>(ms);
    //        return p;
    //    }

    //    protected byte[] Packet<T>(T cmdData)
    //    {
    //        MemoryStream ms = new MemoryStream();
    //        Serializer.Serialize<T>(ms, cmdData);
    //        return ms.ToArray();
    //    }
    //}

    public abstract class Cell_Base
    {
        protected byte[] buffer;
        protected static T unPacket<T>(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            T p = Serializer.Deserialize<T>(ms);
            return p;
        }
        protected byte[] Packet<T>(T cmdData)
        {
            MemoryStream ms = new MemoryStream();
            Serializer.Serialize<T>(ms, cmdData);
            return ms.ToArray();
        }

        public byte[] GetBuffer() => buffer;
        public virtual global::ProtoBuf.IExtensible ParseData(byte[] buffer)
        { return new T(); }
        public virtual void SaveData(params object[] args) { }
        public abstract ProtoCommand Proto_Head { get; }
        public abstract uint Proto_Info { get; }
    }

}

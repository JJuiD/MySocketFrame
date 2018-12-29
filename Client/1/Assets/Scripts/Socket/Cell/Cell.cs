using ProtoBuf;
using System.IO;

namespace Proto.Cell
{
    
    public abstract class Cell
    {
        public abstract void OnReceivePacket(object _object,byte[] buffer);

        protected T unPacket<T>(byte[] buffer)
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
    }
}

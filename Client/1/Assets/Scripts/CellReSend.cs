using UnityEngine;
using socket;
using Proto;
using ProtoBuf;
using System.IO;
using UnityEngine.UI;

public class CellReSend : MonoBehaviour
{
    test _test = new test();
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(onClickReSend);
    }

    public void onClickReSend()
    {
        CMD_TEST cmdTest = new CMD_TEST();
        cmdTest.msg = System.Text.Encoding.Default.GetBytes("Hello Server!");
        MemoryStream ms = new MemoryStream();
        Serializer.Serialize<CMD_TEST>(ms, cmdTest);
        _test.SocketSend(ProtoCommand.ProtoCommand_TestModel, ms.ToArray());
    }
	
}

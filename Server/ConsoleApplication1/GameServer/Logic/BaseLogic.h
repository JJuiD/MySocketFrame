#ifndef LOGIC_BASE_H
#define LOGIC_BASE_H

#include <iostream>
#include "../DataCenter.h"
#include "../Base/SocketManager.h"
//#include <winsock2.h>   

using namespace protocol;

namespace logic
{
	void SwitchToLogicThread();

	class BaseLogic
	{
	private:
		UINT LogicUserID;
	public:
		BaseLogic(UINT id)
		{
			LogicUserID = id;
		}
		~BaseLogic()
		{

		}
	public:
		static void StartAnalysisNetPacket()
		{
			thread _thread(SwitchToLogicThread);
			_thread.detach();
		}

		bool DoMainlogic(Proto::ProtoCommand cmd, string buffer)
		{
			switch (cmd)
			{
			case Proto::ProtoCommand::ProtoCommand_TestModel:
				Proto::CMD_TEST cmdTest;
				cmdTest.ParseFromString(buffer);
				std::cout << "客户端" << ":" << cmdTest.msg() << endl;

				char data[SEND_BUFFER_LEN];
				cmdTest.set_msg("hello,Client! 你好,客户端!");
				cmdTest.SerializePartialToArray(data, SEND_BUFFER_LEN);
				SendMsg(Proto::ProtoCommand::ProtoCommand_TestModel, data);
				break;
			}

			return true;
		}
		void SendMsg(Proto::ProtoCommand cmdHead, char* buffer)
		{
			char data[SEND_BUFFER_LEN];
			Proto::ProtoBaseCmd CmdBase;
			CmdBase.set_cmdhead(cmdHead);
			CmdBase.set_buffer(buffer);
			CmdBase.SerializeToArray(data, SEND_BUFFER_LEN);

			socketframe::SocketManager::getInstance().Send(data, LogicUserID);
			//socketframe::_SocketClass->Send(data, LogicUserID);
		}
	private:

	};

	void SwitchToLogicThread()
	{
		while (true)
		{
			Sleep(1000);
			PACKET* packet = DataCenter::getInstance().GetBuffer();
			if (packet != NULL)
			{
				/*stringstream str_id;
				UINT id;
				for (BYTE i = RECV_BUFFER_LEN; *(recvBuffer + i) != '\0'; ++i)
				{
					str_id << *(recvBuffer + i);
				}
				str_id >> id;*/
				//logic::BaseLogic* _BaseLogic = new logic::BaseLogic(atoi(str_id.c_str()));
				logic::BaseLogic* _BaseLogic = new logic::BaseLogic(packet->ID);
				_BaseLogic->DoMainlogic(packet->CMD_HEAD,packet->PACKET_BUFFER);
				delete _BaseLogic;
			}
		}
	}
}






#endif





































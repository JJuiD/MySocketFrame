#ifndef LOGIC_BASE_H
#define LOGIC_BASE_H

#include <iostream>
#include "../DataCenter.h"
#include "../../Protocol/ProtocolBase.h"
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

		bool DoMainlogic(char* data, int len)
		{
			Proto::ProtoBaseCmd CmdBase ;
			CmdBase.ParseFromArray(data, len);
			switch (CmdBase.cmdhead())
			{
			case Proto::ProtoCommand::ProtoCommand_TestModel:
				Proto::CMD_TEST cmdTest;
				cmdTest.ParseFromString(CmdBase.buffer());
				std::cout << "¿Í»§¶Ë" << ":" << cmdTest.msg() << "\n";

				char data[SEND_BUFFER_LEN];
				cmdTest.set_msg("hello,world! ÄãºÃ");
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
		static DataCenter _DataCenter;
		while (true)
		{
			Sleep(1000);
			char* buffer = _DataCenter.GetBuffer();
			if (buffer != NULL)
			{
				string str_id;
				for (BYTE i = RECV_BUFFER_LEN; *(buffer + i) != '\0'; ++i)
				{
					str_id[str_id.length()] = *(buffer + i);
				}
				logic::BaseLogic* _BaseLogic = new logic::BaseLogic(atoi(str_id.c_str()));
				_BaseLogic->DoMainlogic(buffer, SEND_BUFFER_LEN);
				delete _BaseLogic;
			}
		}
	}
}






#endif





































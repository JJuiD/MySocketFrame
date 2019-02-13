#include "RoomLogic.h"

logic::RoomLogic::RoomLogic(UINT id)
{
	LogicUserID = id;
}

logic::RoomLogic::~RoomLogic()
{
}

void logic::RoomLogic::SendData(UINT cmdid, char * buffer)
{
	char data[SEND_BUFFER_LEN];
	Proto::ProtoBaseCmd cmdbase;
	cmdbase.set_cmdhead(Proto::ProtoCommand::ProtoCommand_Room);
	cmdbase.set_cmdinfo(cmdid);
	cmdbase.set_buffer(buffer);
	cmdbase.SerializeToArray(data, SEND_BUFFER_LEN);

	socketframe::SocketManager::getInstance().Send(data, LogicUserID);
}

void logic::RoomLogic::onLogicPacket(UINT proto, string buffer)
{
	//assert(proto)
	switch (Proto::RoomBase::RoomCommand(proto))
	{
	case Proto::RoomBase::RoomCommand::CMDR_REQCREATEUSER:
	{
		
		break;
	}
	}
}

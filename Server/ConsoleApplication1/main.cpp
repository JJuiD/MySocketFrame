#include <iostream>
#include "GameServer/Base/SocketManager.h"
#include "GameServer/Logic/BaseLogic.h"

int main()
{
	std::cout << "MySocketFrame\n";
	logic::BaseLogic::StartAnalysisNetPacket();
	socketframe::SocketManager::StartListenNetPacket(NET_TYPE_UDP);
}


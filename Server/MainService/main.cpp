// Server.cpp : ���ļ����� "main" ����������ִ�н��ڴ˴���ʼ��������
//

#include <iostream>
#include "GameServer/Base/SocketManager.h"
#include "GameServer/Logic/LogicManager.h"
#include "Model/RedisModel.h"

int main()
{
	std::cout << "MySocketFrame\n";
	/*logic::LogicManager::StartAnalysisNetPacket();
	socketframe::SocketManager::StartListenNetPacket(NET_TYPE_UDP);*/
	RedisModel::geInstance().Connect();
}



// ���г���: Ctrl + F5 ����� >����ʼִ��(������)���˵�
// ���Գ���: F5 ����� >����ʼ���ԡ��˵�

// ������ʾ: 
//   1. ʹ�ý��������Դ�������������/�����ļ�
//   2. ʹ���Ŷ���Դ�������������ӵ�Դ�������
//   3. ʹ��������ڲ鿴���������������Ϣ
//   4. ʹ�ô����б��ڲ鿴����
//   5. ת������Ŀ��>���������Դ����µĴ����ļ�����ת������Ŀ��>�����������Խ����д����ļ���ӵ���Ŀ
//   6. ��������Ҫ�ٴδ򿪴���Ŀ����ת�����ļ���>���򿪡�>����Ŀ����ѡ�� .sln �ļ�

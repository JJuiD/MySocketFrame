#ifndef DATA_CENTER_H
#define DATA_CENTER_H

#include "../Protocol/ProtocolBase.h"
#include "../Config/ConfigBase.h"

struct NET_CLIENT
{
	sockaddr_in addr;
	UINT id;

	NET_CLIENT()
	{
		id = USERID_NULL;
	}

	~NET_CLIENT()
	{
		
	}

	bool operator == (sockaddr_in _addr)
	{
		return (_addr.sin_port == addr.sin_port
			&& _addr.sin_addr.S_un.S_addr == addr.sin_addr.S_un.S_addr
			);
	}
	bool operator == (UINT _id)
	{
		return (id == _id);
	}
	
};

struct PACKET
{
	Proto::ProtoCommand CMD_HEAD;
	//WORD PACKET_SIZE;
	UINT ID;
	string PACKET_BUFFER;
};

typedef map<UINT, NET_CLIENT> MAP_CLIENT;

class DataCenter
{
private:
	MAP_CLIENT ClientMap;
	WORD Connect_Client_Num = 0;
	list<PACKET*> RecvBufferList;
public:
	~DataCenter() {}
	static DataCenter & getInstance()
	{
		static DataCenter m_instance;
		return m_instance;
	}
public:
	void AddBuffer(char* buffer,int size,UINT id)
	{
		/*string str_id = to_string(id);
		for (WORD i = 0; i < str_id.length(); ++i)
		{
			buffer[size + i + 1] = str_id[i];
		}*/
		Proto::ProtoBaseCmd CmdData;
		CmdData.ParseFromArray(buffer, size);
		PACKET* packet = new PACKET() ;
		packet->CMD_HEAD = CmdData.cmdhead();
		packet->PACKET_BUFFER = CmdData.buffer();
		packet->ID = id;
		RecvBufferList.push_back(packet);
	}
	PACKET* GetBuffer()
	{
		if (RecvBufferList.empty()) return NULL;
		PACKET* packet = RecvBufferList.front();
		RecvBufferList.pop_front();
		return packet;
	}
public:
	NET_CLIENT GetClient(UINT id)
	{
		if (ClientMap[id] == USERID_NULL) return NET_CLIENT();
		return ClientMap[id];
	}
	NET_CLIENT GetClient(sockaddr_in _addr) {
		MAP_CLIENT::iterator itr = ClientMap.begin();
		while (itr != ClientMap.end())
		{
			if (itr->second == _addr)
			{
				return itr->second;
			}
		}
		return NET_CLIENT();
	}
	bool AddClient(sockaddr_in _addr)
	{
		NET_CLIENT client = GetClient(_addr);
		if (client.id != USERID_NULL) return false;
		client.id = Connect_Client_Num;
		client.addr = _addr;
		ClientMap.emplace(pair<UINT, NET_CLIENT>(Connect_Client_Num, client));
		++Connect_Client_Num;
		return true;
	}
	//bool AddClient(NET_CLIENT client)
	//{
	//	NET_CLIENT _client = GetClient(*client.addr);
	//	if (_client.id != USERID_NULL) return false;
	//	ClientMap.emplace(pair<UINT, NET_CLIENT>(Connect_Client_Num, client));
	//	++Connect_Client_Num;
	//	return true;
	//}

	bool EraseClient(UINT id)
	{
		if (ClientMap[id] == USERID_NULL) return false;

		ClientMap.erase(id);
		return true;
	}
	bool EraseClient(sockaddr_in _addr)
	{
		NET_CLIENT client = GetClient(_addr);
		if (client.id != USERID_NULL) return false;

		return EraseClient(client.id);
	}
};




#endif
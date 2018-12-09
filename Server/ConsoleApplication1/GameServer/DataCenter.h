#ifndef DATA_CENTER_H
#define DATA_CENTER_H


#include "../Config/ConfigBase.h"

struct NET_CLIENT
{
	sockaddr_in* addr;
	UINT id;

	NET_CLIENT()
	{
		addr = new sockaddr_in();
		id = USERID_NULL;
	}

	~NET_CLIENT()
	{
		delete addr;
	}

	bool operator == (sockaddr_in _addr)
	{
		return (_addr.sin_port == addr->sin_port
			&& _addr.sin_addr.S_un.S_addr == addr->sin_addr.S_un.S_addr
			);
	}
	bool operator == (UINT _id)
	{
		return (id == _id);
	}
	
};

typedef map<UINT, NET_CLIENT> MAP_CLIENT;

class DataCenter
{
private:
	MAP_CLIENT ClientMap;
	WORD Connect_Client_Num = 0;
	list<char*> RecvBufferList;
public:
	~DataCenter() {}
	static DataCenter & getInstance()
	{
		static DataCenter m_instance;
		return m_instance;
	}
	void print()
	{
		cout << ++Connect_Client_Num << endl;
	}
public:
	void AddBuffer(char* buffer,UINT id)
	{
		string str_id = to_string(id);
		for (WORD i = 0; i < str_id.length(); ++i)
		{
			*(buffer + RECV_BUFFER_LEN + i) = str_id[i];
		}
		RecvBufferList.push_back(buffer);
	}
	char* GetBuffer()
	{
		if (RecvBufferList.empty()) return NULL;
		char* buffer = RecvBufferList.front();
		RecvBufferList.pop_front();
		return buffer;
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
		client.addr = &_addr;
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
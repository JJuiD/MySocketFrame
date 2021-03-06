// Redis.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "Redis.h"
#include <sstream>

REDISDLL_API bool redisAPI::Connect(const char *ip, int port)
{
	redisContext* _connect = redisConnect(ip, port);
	if (_connect != NULL && _connect->err)
	{
		printf("connect error: %s\n", _connect->errstr);
		return false;
	}
	printf("connect Redis Success");
	return true;
}



/*template<class... T>
std::string redisAPI::Excute(std::string cmd, std::string key, T... args, int numid)
{
	std::string strAgs = cmd + " " + (setCmd(key, numid),key);
	int arr[] = { (setCmd(strAgs,args),0)... };
	printf("Redis Excute: " + strAgs);
	_reply = (redisReply*)redisCommand(_connect, "%s", strAgs);
	std::string str = _reply->str;
	freeReplyObject(_reply);
	return str;
}*/

template <class T>
void redisAPI::setCmd(std::string & cmd, T value)
{
	std::ostringstream oss;
	oss << " " << value;
	cmd += oss.str();
}
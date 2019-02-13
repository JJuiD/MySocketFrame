#ifndef _DLL_H_
#define _DLL_H_

#ifdef DLL_EXPORTS  
#define DLL_API __declspec(dllexport)   
#else  
#define DLL_API __declspec(dllimport)   
#endif

#include <hiredis.h>
#define NO_OFORKIMPL 
#include <Win32_Interop\win32fixes.h>
#pragma comment(lib,"hiredis.lib")
#pragma comment(lib,"Win32_Interop.lib")
#include <iostream>
#include <thread>

class API
{
public:
	DLL_API bool Connect(const char*, int);
private:
	// _connect;
	redisReply* _reply;
	bool isOpenRedisThread = false;
private:
	void RedisThread();
	
};



#endif
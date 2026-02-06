using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class NTPTime : MonoBehaviour
{
    public static NTPTime Instance;
    public bool DateSynchronized => SessionStartTime.HasValue;
    
    public DateTime? SessionStartTime { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        
        SyncTime();
    }

    public async void SyncTime()
    {
        Debug.Log("Starting time Sync...");
        DateTime? netTime = await GetNetworkTimeAsync();

        if (netTime.HasValue)
        {
            SessionStartTime = netTime.Value;
            Debug.Log($"NTP time synchronized: {SessionStartTime}");
        }
        else
        {
            Debug.LogError("Connection to NTP server failed.");
        }
    }

    public static async Task<DateTime?> GetNetworkTimeAsync()
    {
        return await Task.Run<DateTime?>(() =>
        {
            try
            {
                const string ntpServer = "pool.ntp.org";
                var ntpData = new byte[48];
                ntpData[0] = 0x1B; // LI = 0, VN = 3, Mode = 3

                var addresses = Dns.GetHostEntry(ntpServer).AddressList;
                var ipEndPoint = new IPEndPoint(addresses[0], 123);

                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    socket.Connect(ipEndPoint);
                    socket.ReceiveTimeout = 3000; 
                    socket.Send(ntpData);
                    socket.Receive(ntpData);
                }

                const byte serverReplyTime = 40;
                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

                return (DateTime?)networkDateTime;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"NTP Ошибка: {e.Message}");
                return null;
            }
        });
    }

    static uint SwapEndianness(ulong x)
    {
        return (uint)(((x & 0x000000ff) << 24) +
                       ((x & 0x0000ff00) << 8) +
                       ((x & 0x00ff0000) >> 8) +
                       ((x & 0xff000000) >> 24));
    }

    public DateTime GetCurrentTime()
    {
        if (SessionStartTime == null) SyncTime();
        return SessionStartTime.Value.AddSeconds(Time.realtimeSinceStartup);
    }
}
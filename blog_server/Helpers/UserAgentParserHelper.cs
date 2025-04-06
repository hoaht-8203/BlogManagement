using System;
using blog_server.Models;
using UAParser;

namespace blog_server.Helpers;

public class UserAgentParserHelper
{
    public static DeviceInfo ParseUserAgent(string userAgent)
    {
        var parser = Parser.GetDefault();
        ClientInfo client = parser.Parse(userAgent);

        return new DeviceInfo
        {
            DeviceType = string.IsNullOrEmpty(client.Device.Family)
                ? "Desktop"
                : client.Device.Family,
            OS = client.OS.Family ?? "Unknown",
            Browser = client.UA.Family ?? "Unknown",
        };
    }
}

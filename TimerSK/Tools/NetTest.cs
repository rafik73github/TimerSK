using System.Net;
using System.Net.NetworkInformation;

namespace TimerSK.Tools
{
    class NetTest
    {

        public bool IsLocalNetAvailable()
        {
            bool isAvailable = NetworkInterface.GetIsNetworkAvailable();
            return isAvailable;
        }

        public bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
            
        }

    }
}

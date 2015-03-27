using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TheLazyInterfaces;

namespace TheLazyClientMVVM
{
    public class WCFClient
    {
        public ChannelFactory<ITheLazyService> factory;
        EndpointAddress address;
        public ITheLazyService channel;
        public void init()
        {
            factory = new ChannelFactory<ITheLazyService>("TheLazyServiceEndpoint");
            address = getEndpoint();
            channel = factory.CreateChannel(address);
           //(factory.State.ToString);
        }
        void doWork()
        {
            using (ChannelFactory<ITheLazyService> factory = new ChannelFactory<ITheLazyService>("IPartyControllerEndpoint"))
            {
                EndpointAddress address = getEndpoint();
                ITheLazyService channel = factory.CreateChannel(address);
                channel.DoWork();
                ((IClientChannel)channel).Close();
            }
        }

        private static EndpointAddress getEndpoint()
        {
            EndpointAddress address = new EndpointAddress(String.Format("http://{0}/TheLazyService", "localhost"));
            return address;
        }
    }
}

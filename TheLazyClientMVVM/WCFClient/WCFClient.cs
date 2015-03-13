using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TheLazyInterfaces;

namespace TheLazyClientMVVM.WCFClient
{
    class WCFClient
    {
        ChannelFactory<ITheLazyService> factory;
        EndpointAddress address;
        public ITheLazyService channel;
        public void init()
        {
            factory = new ChannelFactory<ITheLazyService>("ITheLazyServiceEndpoint");
            address = getEndpoint();
            channel = factory.CreateChannel(address);
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
            EndpointAddress address = new EndpointAddress(String.Format("net.tcp://{0}/ServiceInterface/PartyController.svc", new SelectorConnexions.ConnectionInfo()));
            return address;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TheLazyInterfaces
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ITheLazyService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ITheLazyService
    {
        [OperationContract]
        void DoWork();
    }
}

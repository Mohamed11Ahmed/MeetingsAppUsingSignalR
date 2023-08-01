using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteBoardDemo.Services
{
    interface IConnectionsService
    {
        void AddUserConnection(Guid guid);

        void removeUserConnection(Guid guid);

        IList<String> GetUserConnectionInRoom(int id);
    }
}

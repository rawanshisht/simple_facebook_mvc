using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookApp.Models
{
    public enum FriendRequestStatus
    {
        SentAndPending = 1,
        Friend = 2,
        NotFriend = 3,
        ReceivedAndPending = 4
    }
}

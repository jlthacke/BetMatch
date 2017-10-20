using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetMatch.Models.Enums
{
    public enum WagerGetQuery
    {
        BetFeed = 0,
        OpenBets = 1,
        PendingSettlement = 2,
        SettledWagers = 3
    }

    public enum WagerEditQuery
    {
        Insert = 0,
        Accept = 1,
        Settle = 2,
        Delete = 3
    }

    public enum UserGetQuery
    {
        ListAll_AdminApproved = 0
    }
}
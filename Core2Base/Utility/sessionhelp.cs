using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Core2Base.Models;
using Microsoft.AspNetCore.Http;

namespace Core2Base.Utility
{
    public class Sessionhelp
    {

        public static Session InitSession()
            {
            Session session = new Session();
            session.SessionID = System.Guid.NewGuid().ToString();
            return session;
            }
    } 
}
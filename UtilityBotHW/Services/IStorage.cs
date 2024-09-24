using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityBotHW.Models;

namespace UtilityBotHW.Services
{
    internal interface IStorage
    {
        Session GetSession(long chatId);
    }
}

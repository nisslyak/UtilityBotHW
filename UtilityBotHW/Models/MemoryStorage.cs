using System;
using System.Collections.Concurrent;
using UtilityBotHW.Services;

namespace UtilityBotHW.Models
{
    internal class MemoryStorage: IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;
        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            var newSession = new Session() { ChosenButton = "count" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}

using System;

namespace PokerSNTS.Domain.Notifications
{
    public class DomainNotification
    {
        public DomainNotification(string key, string value)
        {
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
        }

        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}

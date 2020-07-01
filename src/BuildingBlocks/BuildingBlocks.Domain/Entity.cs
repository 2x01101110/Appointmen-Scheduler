using MediatR;
using System;
using System.Collections.Generic;

namespace BuildingBlocks.Domain
{
    public abstract class Entity<T> : IEquatable<Entity<T>>
    {
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
        public virtual T Id { get; protected set; }

        public void CheckBusinessRule(IBusinessRule businessRule)
        {
            if (!businessRule.IsValid())
            {
                throw new Exception(businessRule.Message);
            }
        }
        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(domainEvent);
        }
        public void RemoveDomainEvent(INotification domainEvent) 
        {
            this._domainEvents?.Remove(domainEvent);
        }
        public void ClearDomainEvents() 
        {
            this._domainEvents?.Clear();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Entity<T> other && Equals(other);
        }
        public bool Equals(Entity<T> other) => Comparer<T>.Default.Compare(this.Id, other.Id) == 0;
        public override int GetHashCode() => this.Id.GetHashCode();
        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }
        public static bool operator !=(Entity<T> left, Entity<T> right) => !(left == right);
    }
}

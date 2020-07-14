using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildingBlocks.Domain
{
    public abstract class ValueObject
    {
        public void CheckBusinessRule(IBusinessRule businessRule)
        {
            if (!businessRule.IsValid())
            {
                throw new Exception(businessRule.Message);
            }
        }

        public abstract IEnumerable<object> GetAtomicValues();

        protected static bool EqualsOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null)) return false;

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;

            ValueObject other = obj as ValueObject;

            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null)) return false;
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current)) return false;
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode() => GetAtomicValues().Select(x => x != null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
    }
}

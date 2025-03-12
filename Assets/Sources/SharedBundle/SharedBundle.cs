using System.Collections.Generic;

namespace Assets.Sources.LoadingTree.SharedBundle
{
    public class SharedBundle
    {
        private readonly Dictionary<string, object> _values;

        public SharedBundle() =>
            _values = new();

        public void Add(in string key, in object value) =>
            _values.Add(key, value);

        public void Remvoe(in string key) =>
            _values.Remove(key);

        public TValue Get<TValue>(in string key)
            where TValue : class =>
            _values[key] as TValue;
    }
}
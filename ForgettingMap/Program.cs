using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgettingMap
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class ForgettingMap
    {

        private readonly IDictionary<string, string> _associations = null;
        private readonly IDictionary<string, int> _searchStats = new Dictionary<string, int>();
        private readonly IDictionary<string, DateTime> _timeOfInsertions = new Dictionary<string, DateTime>();
        private readonly int _maxCapacity;
        public ForgettingMap(int capacity)
        {
            _maxCapacity = capacity;
            _associations = new Dictionary<string, string>(capacity);
        }

        public void Add(string key, string toShortDateString)
        {
            if (_associations.Count >= _maxCapacity)
            {
                if (_searchStats.Count > 0)
                {
                    // get least searcg count
                    var leastSearchCount = _searchStats.Values.Min();
                    var keysWithMinValues =
                        _searchStats.Where(item => item.Value == leastSearchCount).Select(item => item.Key).ToList();

                    var oldestItem =
                        _timeOfInsertions.Where(item => keysWithMinValues.Contains(item.Key))
                            .OrderBy(item => item.Value)
                            .First()
                            .Key;

                    _associations.Remove(oldestItem);
                }
                else
                {
                    var oldestItem = _timeOfInsertions
                            .OrderBy(item => item.Value)
                            .First()
                            .Key;
                    _associations.Remove(oldestItem);
                }
            }

            _associations.Add(key, toShortDateString);
            _timeOfInsertions.Add(key, System.DateTime.Now);
        }

        public int Count
        {
            get { return _associations.Count; }
        }

        public int SearchStats(string key)
        {
            if (_searchStats.ContainsKey(key))
                return _searchStats[key];
            return 0;
        }

        public string Find(string key)
        {

            AddToSearchStats(key);

            if (_associations.ContainsKey(key))
                return _associations[key];

            return null;
        }

        private void AddToSearchStats(string key)
        {
            if (!_associations.ContainsKey(key))
                return;

            if (!_searchStats.ContainsKey(key))
            {
                _searchStats.Add(key, 0);
            }

            _searchStats[key] = _searchStats[key] + 1;
        }
    }
}

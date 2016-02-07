

namespace ForgettingMap.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ForgettingMapTests
    {

        // add association - key & value pair
        [Test]
        public void TestAddAssociation()
        {
            var forgettingMap = new ForgettingMap(2);
            forgettingMap.Add("key", System.DateTime.Now.ToShortDateString());
            Assert.AreEqual(1, forgettingMap.Count, "Expected count should be 1.");
        }

        // should not allow duplicate keys
        [Test]
        public void TestShouldNotAllowDuplicateKeys()
        {
            var forgettingMap = new ForgettingMap(2);
            forgettingMap.Add("key", System.DateTime.Now.ToShortDateString());
            Assert.Throws(typeof(System.ArgumentException), () => forgettingMap.Add("key", System.DateTime.Now.ToShortDateString()));
        }

        // should not exceed capacity
        [Test]
        public void TestShouldNotExceedCapacity()
        {
            int capacity = 2;
            var forgettingMap = new ForgettingMap(capacity);
            forgettingMap.Add("key", System.DateTime.Now.ToShortDateString());
            forgettingMap.Add("key1", System.DateTime.Now.ToShortDateString());
            forgettingMap.Add("key2", System.DateTime.Now.ToShortDateString());
            Assert.AreEqual(capacity, forgettingMap.Count);
        }
        // find an association using key

        // Keep track of find count
        [Test]
        public void TestKeepTrackOfSearchCount()
        {
            int capacity = 2;
            var forgettingMap = new ForgettingMap(capacity);
            forgettingMap.Add("key", System.DateTime.Now.ToShortDateString());
            forgettingMap.Add("key2", System.DateTime.Now.ToShortDateString());
            var searchCount = 5;
            for (int i = 0; i < searchCount; i++)
            {
                var found = forgettingMap.Find("key");
            }
            Assert.AreEqual(searchCount, forgettingMap.SearchStats("key"));
        }

        // remove the least accessed association when capacity exceeds maximum
        [Test]
        public void TestShouldAddItemEvenIfCapacityExceedsByRemovingLeastAccessedItem()
        {
            int capacity = 2;
            var forgettingMap = new ForgettingMap(capacity);
            forgettingMap.Add("key", System.DateTime.Now.ToShortDateString());
            forgettingMap.Add("key1", System.DateTime.Now.ToShortDateString());
            forgettingMap.Add("key2", System.DateTime.Now.ToShortDateString());
            Assert.IsNotNull(forgettingMap.Find("key2"));
        }

        // remove the least accessed association when capacity exceeds maximum
        [Test]
        public void TestShouldRemoveOldestItemIfTwoItemsWithLeastSearch()
        {
            int capacity = 2;
            var forgettingMap = new ForgettingMap(capacity);
            forgettingMap.Add("key", System.DateTime.Now.ToShortDateString());
            forgettingMap.Find("key");
            forgettingMap.Find("key");
            forgettingMap.Find("key");

            forgettingMap.Add("key1", System.DateTime.Now.ToShortDateString());
            forgettingMap.Find("key1");
            forgettingMap.Find("key1");
            forgettingMap.Find("key1");
            forgettingMap.Add("key2", System.DateTime.Now.ToShortDateString());
            Assert.IsNotNull(forgettingMap.Find("key1"));
        }
    }

    
}

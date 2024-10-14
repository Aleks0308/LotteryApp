using NUnit.Framework;
using LotteryApp1.Models;
using LotteryApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotteryApp1.Tests
{
    [TestFixture]
    public class LotteryDrawDataStoreTests
    {
        private LotteryDrawDataStore dataStore;

        [SetUp]
        public void Setup()
        {
            dataStore = new LotteryDrawDataStore();
        }

        [Test]
        public async Task AddItemAsync_AddsItem()
        {
            // Arrange
            var draw = new LotteryDraw { Id = "1" };

            // Act
            bool result = await dataStore.AddItemAsync(draw);

            // Assert
            Assert.IsTrue(result);
            Assert.That((await dataStore.GetItemsAsync()).Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task AddItemsAsync_AddsMultipleItems()
        {
            // Arrange
            var draw1 = new LotteryDraw { Id = "1" };
            var draw2 = new LotteryDraw { Id = "2" };

            // Act
            bool result = await dataStore.AddItemsAsync(new List<LotteryDraw> { draw1, draw2 });

            // Assert
            Assert.IsTrue(result);
            Assert.That((await dataStore.GetItemsAsync()).Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateItemAsync_WithExistingItem_ReturnsTrue()
        {
            // Arrange
            var draw = new LotteryDraw { Id = "1" };

            // Act
            await dataStore.AddItemAsync(draw);
            var updatedDraw = new LotteryDraw { Id = "1", TopPrize = 1000000 };
            bool result = await dataStore.UpdateItemAsync(updatedDraw);
            LotteryDraw resultDraw = await dataStore.GetItemAsync("1");

            // Assert
            Assert.IsTrue(result);
            Assert.That(resultDraw.TopPrize, Is.EqualTo(1000000));
        }

        [Test]
        public async Task UpdateItemAsync_WithNonExistingItem_ReturnsFalse()
        {
            // Arrange
            var draw = new LotteryDraw { Id = "1" };

            // Act
            bool result = await dataStore.UpdateItemAsync(draw);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteItemAsync_WithExistingItem_ReturnsTrue()
        {
            // Arrange
            var draw = new LotteryDraw { Id = "1" };

            // Act
            await dataStore.AddItemAsync(draw);
            bool result = await dataStore.DeleteItemAsync("1");

            // Assert
            Assert.IsTrue(result);
            Assert.That((await dataStore.GetItemsAsync()).Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteItemAsync_WithNonExistingItem_ReturnsFalse()
        {
            // Act
            bool result = await dataStore.DeleteItemAsync("1");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetItemAsync_WithExistingItem_ReturnsItem()
        {
            // Arrange
            var draw = new LotteryDraw { Id = "1" };

            // Act
            await dataStore.AddItemAsync(draw);
            LotteryDraw result = await dataStore.GetItemAsync("1");

            // Assert
            Assert.That(result.Id, Is.EqualTo("1"));
        }

        [Test]
        public async Task GetItemAsync_WithNonExistingItem_ReturnsNull()
        {
            // Act
            LotteryDraw result = await dataStore.GetItemAsync("1");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetItemsAsync_ReturnsAllItems()
        {
            // Arrange
            var draw1 = new LotteryDraw { Id = "1" };
            var draw2 = new LotteryDraw { Id = "2" };

            // Act
            await dataStore.AddItemsAsync(new List<LotteryDraw> { draw1, draw2 });
            IEnumerable<LotteryDraw> results = await dataStore.GetItemsAsync();

            // Assert
            Assert.That(results.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task DeleteItemsAsync_ClearsAllItems()
        {
            // Arrange
            await dataStore.AddItemsAsync(new List<LotteryDraw> { new LotteryDraw { Id = "1" }, new LotteryDraw { Id = "2" } });

            // Act
            bool result = await dataStore.DeleteItemsAsync();

            // Assert
            Assert.IsTrue(result);
            Assert.That((await dataStore.GetItemsAsync()).Count(), Is.EqualTo(0));
        }
    }
}
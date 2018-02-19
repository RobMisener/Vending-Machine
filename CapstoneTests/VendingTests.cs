using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using Capstone.Classes.VendingItems;

namespace CapstoneTests
{
    [TestClass]
    public class VendingTests
    {
        VendingMachine vendingMachine = new VendingMachine();

        [TestMethod]
        public void TestGetItemsCorrectly()
        {
            // Arrange
            VendingMachineStocker stocker = new VendingMachineStocker();

            // Act
            var items = stocker.GetItems();

            // Assert
            Assert.AreEqual(16, items.Count, "Item count not 16");

        }

        [TestMethod]
        public void FeedsMoneyCorrectly()
        {
            // Arrange


            // Act
            int provideCash = 10;
            vendingMachine.FeedMoney(provideCash);

            // Assert
            Assert.AreEqual(provideCash, vendingMachine.CurrentMoneyProvided, "Item count not 16");

        }

        [TestMethod]
        public void ReducesStock()
        {
            // Arrange
            Transaction transaction = new Transaction();
            IVendingItem item = new Candy();
            item.Stock = 5;
            //item = vendingMachine.DecreaseStock(item);

            // Act
            vendingMachine.Purchase(item);

            // Assert
            Assert.AreEqual(4, item.Stock, "Item stock should decrease when purchased.");
        }

        [TestMethod]
        public void ReturnsCorrectChange()
        {
            // Arrange
            Change change = new Change();

            // Act
            change.ReturnChange(2.15M);

            // Assert
            Assert.AreEqual(8, change.Quarters, "The amount of quarters should be 8");
            Assert.AreEqual(1, change.Dimes, "The amount of dimes should be 1");
            Assert.AreEqual(1, change.Nickels, "The amount of nickels should be 1");
        }

        [TestMethod]
        public void UpdatesBalanceCorrectly()
        {
            // Arrange
            IVendingItem item = new Candy();
            item.ItemPrice = 1.50;
            int provideCash = 10;

            // Act
            vendingMachine.FeedMoney(provideCash);
            //vendingMachine.UpdateBalance(item);

            // Assert
            Assert.AreEqual(1.50, vendingMachine.Balance, "The prie of the item should be added to balance.");

        }

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using Capstone.Classes.VendingItems;

namespace CapstoneTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetItemsCorrectly()
        {
            //arrange
            VendingMachine vendingMachine = new VendingMachine();

            //act
            vendingMachine.Items = vendingMachine.GetItems();

            //assert
            Assert.AreEqual(16, vendingMachine.Items.Count, "Item count not 16");

        }

        [TestMethod]
        public void FeedsMoneyCorrectly()
        {
            //arrange
            VendingMachine vendingMachine2 = new VendingMachine();

            //act
            int provideCash = 10;
            vendingMachine2.FeedMoney(provideCash, vendingMachine2);

            //assert
            Assert.AreEqual(provideCash, vendingMachine2.CurrentMoneyProvided, "Item count not 16");

        }

        [TestMethod]
        public void ReducesStock()
        {
            //arrange
            VendingMachine vendingMachine = new VendingMachine();
            Transaction transaction = new Transaction();
            IVendingItem item = new Candy();
            item.Stock = 5;
            item = vendingMachine.DecreaseStock(item);

            //act
            vendingMachine.Purchase(vendingMachine, item, transaction);

            //assert
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
            VendingMachine vendingMachine = new VendingMachine();
            IVendingItem item = new Candy();
            item.ItemPrice = 1.50;
            int provideCash = 10;

            // Act
            vendingMachine.FeedMoney(provideCash, vendingMachine);
            vendingMachine.UpdateBalance(vendingMachine, item);

            // Assert
            Assert.AreEqual(1.50, vendingMachine.Balance, "The prie of the item should be added to balance.");

        }

    }
}

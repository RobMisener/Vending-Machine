using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

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
        public void FeedsMoneyCorrectly()
        {
            //arrange
            VendingMachine vendingMachine = new VendingMachine();

            //act
            vendingMachine.Items = vendingMachine.GetItems();

            //assert


            Assert.AreEqual(provideCash, vendingMachine2.CurrentMoneyProvided, "Item count not 16");

        }

    }
}

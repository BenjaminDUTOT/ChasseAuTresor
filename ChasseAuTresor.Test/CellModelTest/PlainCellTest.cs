using ChasseAuTresor.Model.AdventurerModel;
using ChasseAuTresor.CellModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChasseAuTresor.Test.CellModelTest
{
    internal class PlainCellTest
    {
        [Test]
        public void cellTypeTest()
        {
            PlainCell cell = new PlainCell();

            Assert.That(cell.cellType, Is.EqualTo(TypeCell.Plain));
        }

        [Test]
        public void GetDisplayTextTestNoAdventurer()
        {
            PlainCell cell = new PlainCell();

            Assert.That(cell.GetDisplayType(), Is.EqualTo("."));
        }

        [Test]
        public void GetDisplayTextTestAdventurer()
        {
            PlainCell cell = new PlainCell()
            {
                Adventurer = new Adventurer()
                {
                    Name = "Ben",
                }
            };

            Assert.That(cell.GetDisplayType(), Is.EqualTo("A(Ben)"));
        }

        [Test]
        public void GetDisplayTextTestAdventurerNoName()
        {
            PlainCell cell = new PlainCell()
            {
                Adventurer = new Adventurer()
            };

            Assert.That(cell.GetDisplayType(), Is.EqualTo("A()"));
        }

        [Test]
        public void IsReachableTestNoAdventurer()
        {
            PlainCell cell = new PlainCell();

            Assert.True(cell.IsReachable());
        }

        [Test]
        public void IsReachableTestAdventurer()
        {
            PlainCell cell = new PlainCell()
            {
                Adventurer = new Adventurer()
            };

            Assert.False(cell.IsReachable());
        }

        [Test]
        public void AddAdventurerTestAdventurer()
        {
            PlainCell cell = new PlainCell();

            cell.AddAdventurer(new Adventurer());

            Assert.NotNull(cell.Adventurer);
        }

        [Test]
        public void AddAdventurerTestValue()
        {
            PlainCell cell = new PlainCell();

            cell.AddAdventurer(null);

            Assert.Null(cell.Adventurer);
        }

        [Test]
        public void RemoveAdventurerTestAdventurer()
        {
            PlainCell cell = new PlainCell()
            {
                Adventurer = new Adventurer()
            };

            cell.RemoveAdventurer();

            Assert.Null(cell.Adventurer);
        }

        [Test]
        public void RemoveAdventurerTestNoAdventurer()
        {
            PlainCell cell = new PlainCell();

            cell.RemoveAdventurer();

            Assert.Null(cell.Adventurer);
        }

        [Test]
        public void TryGetDisplayTextTestValue()
        {
            PlainCell cell = new PlainCell()
            {
                AxeHorizontal = 1,
                AxeVertical = 2,
            };

            string resultValue = null;
            bool isSuccess = cell.TryGetWriteText(out resultValue);

            Assert.False(isSuccess);
            Assert.Null(resultValue);
        }

        [Test]
        public void TryGetDisplayTextTestAdventurer()
        {
            PlainCell cell = new PlainCell()
            {
                AxeHorizontal = 1,
                AxeVertical = 2,
                Adventurer = new Adventurer()
                {
                    TreasureValue = 4,
                    AxeHorizontal = 1,
                    AxeVertical = 2,
                    Name = "Ben",
                    Orientation = Orientation.O
                }
            };

            string resultValue;
            bool isSuccess = cell.TryGetWriteText(out resultValue);

            Assert.True(isSuccess);
            Assert.NotNull(resultValue);
            Assert.That(resultValue, Is.EqualTo("A - Ben - 1 - 2 - O - 4"));
        }

        [Test]
        public void TryGetDisplayTextTestNoDefineValue()
        {
            PlainCell cell = new PlainCell();

            string resultValue = null;
            bool isSuccess = cell.TryGetWriteText(out resultValue);

            Assert.False(isSuccess);
            Assert.Null(resultValue);
        }
    }
}

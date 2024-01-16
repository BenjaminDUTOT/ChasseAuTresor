using ChasseAuTresor.Model.AdventurerModel;
using ChasseAuTresor.CellModel;
using ChasseAuTresor.GameDataModel;
using Microsoft.VisualBasic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChasseAuTresor.Test.CellModelTest
{
    internal class TreasureCellTest
    {
        [Test]
        public void cellTypeTest()
        {
            TreasureCell cell = new TreasureCell();

            Assert.That(cell.cellType, Is.EqualTo(TypeCell.Treasure));
        }

        [Test]
        public void GetDisplayTextTestNoAdventurer1()
        {
            TreasureCell cell = new TreasureCell()
            {
                TreasureValue = 1,
            };

            Assert.That(cell.GetDisplayType(), Is.EqualTo("T(1)"));
        }

        [Test]
        public void GetDisplayTextTestNoAdventurer0()
        {
            TreasureCell cell = new TreasureCell()
            {
                TreasureValue = 0,
            };

            Assert.That(cell.GetDisplayType(), Is.EqualTo("T(0)"));
        }

        [Test]
        public void GetDisplayTextTestNoAdventurerNoValueDefine()
        {
            TreasureCell cell = new TreasureCell();

            Assert.That(cell.GetDisplayType(), Is.EqualTo("T(0)"));
        }

        [Test]
        public void GetDisplayTextTestAdventurer()
        {
            TreasureCell cell = new TreasureCell()
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
            TreasureCell cell = new TreasureCell()
            {
                Adventurer = new Adventurer()
            };

            Assert.That(cell.GetDisplayType(), Is.EqualTo("A()"));
        }

        [Test]
        public void AddAdventurerTestNoValue()
        {
            TreasureCell cell = new TreasureCell();

            cell.AddAdventurer(new Adventurer());

            Assert.NotNull(cell.Adventurer);
            Assert.That(cell.TreasureValue, Is.EqualTo(0));
            Assert.That(cell.Adventurer.TreasureValue, Is.EqualTo(0));
        }

        [Test]
        public void AddAdventurerTestValue()
        {
            TreasureCell cell = new TreasureCell()
            {
                TreasureValue = 4
            };

            cell.AddAdventurer(new Adventurer()
            {
                TreasureValue = 5,
            });

            Assert.NotNull(cell.Adventurer);
            Assert.That(cell.TreasureValue, Is.EqualTo(3));
            Assert.That(cell.Adventurer.TreasureValue, Is.EqualTo(6));
        }

        [Test]
        public void AddAdventurerTestNull()
        {
            TreasureCell cell = new TreasureCell();

            cell.AddAdventurer(null);

            Assert.Null(cell.Adventurer);
        }

        [Test]
        public void TryGetDisplayTextTestValue()
        {
            TreasureCell cell = new TreasureCell()
            {
                AxeHorizontal = 1,
                AxeVertical = 2,
                TreasureValue = 3
            };

            string resultValue;
            bool isSuccess = cell.TryGetWriteText(out resultValue);

            Assert.True(isSuccess);
            Assert.NotNull(resultValue);
            Assert.That(resultValue, Is.EqualTo("T - 1 - 2 - 3"));
        }

        [Test]
        public void TryGetDisplayTextTestAdventurer()
        {
            TreasureCell cell = new TreasureCell()
            {
                AxeHorizontal = 1,
                AxeVertical = 2,
                TreasureValue = 3,
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
            Assert.That(resultValue, Is.EqualTo("T - 1 - 2 - 3\r\nA - Ben - 1 - 2 - O - 4"));
        }

        [Test]
        public void TryGetDisplayTextTestNoDefineValue()
        {
            TreasureCell cell = new TreasureCell();

            string resultValue = null;
            bool isSuccess = cell.TryGetWriteText(out resultValue);

            Assert.True(isSuccess);
            Assert.NotNull(resultValue);
            Assert.That(resultValue, Is.EqualTo("T - 0 - 0 - 0"));
        }

        [Test]
        public void IsReachableTestNoAdventurer()
        {
            TreasureCell cell = new TreasureCell();

            Assert.True(cell.IsReachable());
        }

        [Test]
        public void IsReachableTestAdventurer()
        {
            TreasureCell cell = new TreasureCell()
            {
                Adventurer = new Adventurer()
            };

            Assert.False(cell.IsReachable());
        }

        [Test]
        public void RemoveAdventurerTestAdventurer()
        {
            TreasureCell cell = new TreasureCell()
            {
                Adventurer = new Adventurer()
            };

            cell.RemoveAdventurer();

            Assert.Null(cell.Adventurer);
        }

        [Test]
        public void RemoveAdventurerTestNoAdventurer()
        {
            TreasureCell cell = new TreasureCell();

            cell.RemoveAdventurer();

            Assert.Null(cell.Adventurer);
        }

        [Test]
        public void AddTreasureCellInTableTest()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "T - 0 - 0 - 3";
            
            TreasureCell.AddTreasureCellInTable(ref cells, line);

            Assert.NotNull(cells);
            Assert.That(cells[0,0].cellType, Is.EqualTo(TypeCell.Treasure));
            Assert.That(cells[0,0].AxeHorizontal, Is.EqualTo(0));
            Assert.That(cells[0,0].AxeVertical, Is.EqualTo(0));
            Assert.That((cells[0,0] as TreasureCell).TreasureValue, Is.EqualTo(3));
        }

        [Test]
        public void AddTreasureCellInTableTestKoCellsNull()
        {
            AbstractCell[,] cells = null;
            string line = "T - 0 - 0 - 3";

            Assert.Throws<ArgumentNullException>(() => TreasureCell.AddTreasureCellInTable(ref cells, line));
        }

        [Test]
        public void AddTreasureCellInTableTestKoLineNull()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];

            Assert.Throws<ArgumentNullException>(() => TreasureCell.AddTreasureCellInTable(ref cells, null));
        }

        [Test]
        public void AddTreasureCellInTableKoToManyArgument()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "T - 0 - 0 - 3 - 2";

            Assert.Throws<ArgumentException>(() => TreasureCell.AddTreasureCellInTable(ref cells, line));
        }

        [Test]
        public void AddTreasureCellInTableKoToFewArgument()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "T - 0 - 0";

            Assert.Throws<ArgumentException>(() => TreasureCell.AddTreasureCellInTable(ref cells, line));
        }

        [Test]
        public void AddTreasureCellInTableKoCoordonateNotInCells()
        {
            AbstractCell[,] cells = new AbstractCell[1, 2];
            string line = "T - 1 - 2";

            Assert.Throws<ArgumentException>(() => TreasureCell.AddTreasureCellInTable(ref cells, line));
        }
    }
}

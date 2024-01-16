using ChasseAuTresor.Model.AdventurerModel;
using ChasseAuTresor.CellModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChasseAuTresor.Test.CellModelTest
{
    internal class MountainCellTest
    {
        [Test]
        public void cellTypeTest()
        {
            MountainCell cell = new MountainCell();

            Assert.That(cell.cellType, Is.EqualTo(TypeCell.Mountain));
        }

        [Test]
        public void GetDisplayTextTest()
        {
            MountainCell cell = new MountainCell();

            Assert.That(cell.GetDisplayType(), Is.EqualTo("M"));
        }

        [Test]
        public void IsReachableTest()
        {
            MountainCell cell = new MountainCell();

            Assert.False(cell.IsReachable());
        }

        [Test]
        public void TryGetDisplayTextTestValue()
        {
            MountainCell cell = new MountainCell()
            {
                AxeHorizontal = 1,
                AxeVertical = 2,
            };

            string resultValue = null;
            bool isSuccess = cell.TryGetWriteText(out resultValue);

            Assert.True(isSuccess);
            Assert.NotNull(resultValue);
            Assert.That(resultValue, Is.EqualTo("M - 1 - 2"));
        }

        [Test]
        public void TryGetDisplayTextTestNoDefineValue()
        {
            MountainCell cell = new MountainCell();

            string resultValue = null;
            bool isSuccess = cell.TryGetWriteText(out resultValue);

            Assert.True(isSuccess);
            Assert.NotNull(resultValue);
            Assert.That(resultValue, Is.EqualTo("M - 0 - 0"));
        }

        [Test]
        public void AddMountainCellInTableTest()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "M - 0 - 0";

            MountainCell.AddMountainCellInTable(ref cells, line);

            Assert.NotNull(cells);
            Assert.That(cells[0, 0].cellType, Is.EqualTo(TypeCell.Mountain));
            Assert.That(cells[0, 0].AxeHorizontal, Is.EqualTo(0));
            Assert.That(cells[0, 0].AxeVertical, Is.EqualTo(0));
        }

        [Test]
        public void AddMountainCellInTableTestKoCellsNull()
        {
            AbstractCell[,] cells = null;
            string line = "M - 0 - 0";

            Assert.Throws<ArgumentNullException>(() => MountainCell.AddMountainCellInTable(ref cells, line));
        }

        [Test]
        public void AddMountainCellInTableTestKoLineNull()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];

            Assert.Throws<ArgumentNullException>(() => MountainCell.AddMountainCellInTable(ref cells, null));
        }

        [Test]
        public void AddMountainCellInTableKoToManyArgument()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "M - 0 - 0 - 3";

            Assert.Throws<ArgumentException>(() => MountainCell.AddMountainCellInTable(ref cells, line));
        }

        [Test]
        public void AddMountainCellInTableKoToFewArgument()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "M - 0";

            Assert.Throws<ArgumentException>(() => MountainCell.AddMountainCellInTable(ref cells, line));
        }

        [Test]
        public void AddMountainCellInTableKoCoordonateNotInCells()
        {
            AbstractCell[,] cells = new AbstractCell[1, 2];
            string line = "M - 1 - 2";

            Assert.Throws<ArgumentException>(() => MountainCell.AddMountainCellInTable(ref cells, line));
        }
    }
}

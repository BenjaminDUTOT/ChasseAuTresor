using ChasseAuTresor.Model.AdventurerModel;
using ChasseAuTresor.CellModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChasseAuTresor.Test.CellModelTest
{
    internal class AbstractCellTest
    {
        [Test]
        public void CreateTableTest()
        {
            string line = "C - 3 - 4";

            AbstractCell[,] cells = AbstractCell.CreateAbstractCellTableFromLine(line);

            Assert.NotNull(cells);
            Assert.That(cells.GetLength(0), Is.EqualTo(4));
            Assert.That(cells.GetLength(1), Is.EqualTo(3));
        }

        [Test]
        public void CreateTableTestLineNull()
        {
            string line = null;

            Assert.Throws<ArgumentNullException>(() => AbstractCell.CreateAbstractCellTableFromLine(line));
        }

        [Test]
        public void CreateTableTestLineToManyArgument()
        {
            string line = "C - 3 - 4 - 5";

            Assert.Throws<ArgumentException>(() => AbstractCell.CreateAbstractCellTableFromLine(line));
        }

        [Test]
        public void CreateTableTestLineToFewArgument()
        {
            string line = "C - 3";

            Assert.Throws<ArgumentException>(() => AbstractCell.CreateAbstractCellTableFromLine(line));
        }

        [Test]
        public void CreateTableTestLineMissingArgument()
        {
            string line = "C -   - 5";

            Assert.Throws<ArgumentException>(() => AbstractCell.CreateAbstractCellTableFromLine(line));
        }

        [Test]
        public void AddCellInTableTestKoCellsNull()
        {
            AbstractCell[,] cells = null;
            List<Adventurer> adventurers = null;

            Assert.Throws<ArgumentNullException>(() => AbstractCell.AddCellInTable(ref cells, ref adventurers, null));
        }

        [Test]
        public void AddCellInTableTestKoAdventurerNull()
        {
            string line = "C - 3 - 4";
            AbstractCell[,] cells = AbstractCell.CreateAbstractCellTableFromLine(line);

            List<Adventurer> adventurers = null;

            Assert.Throws<ArgumentNullException>(() => AbstractCell.AddCellInTable(ref cells, ref adventurers, null));
        }

        [Test]
        public void AddCellInTableTestKoLinesNull()
        {
            string line = "C - 3 - 4";
            AbstractCell[,] cells = AbstractCell.CreateAbstractCellTableFromLine(line);

            List<Adventurer> adventurers = new List<Adventurer>();

            Assert.Throws<ArgumentNullException>(() => AbstractCell.AddCellInTable(ref cells, ref adventurers, null));
        }

        [Test]
        public void AddCellInTableTestOk()
        {
            string line = "C - 3 - 4";
            AbstractCell[,] cells = AbstractCell.CreateAbstractCellTableFromLine(line);

            List<Adventurer> adventurers = new List<Adventurer>();

            List<string> lines = new List<string>()
            {
                "C - 3 - 4",
                "M - 2 - 2",
                "T - 0 - 0 - 3",
                "A - Lara - 0 - 1 - N - AADADAGGA",
                "A - Ben - 1 - 2 - S - AADA",
                "# - Ben - 1 - 1 - S - AADA",
                "ù"
            };

            
            AbstractCell.AddCellInTable(ref cells, ref adventurers, lines);


            Assert.NotNull(adventurers);
            Assert.That(adventurers.Count, Is.EqualTo(2));

            Assert.That(adventurers.ElementAt(0).Name, Is.EqualTo("Lara"));
            Assert.That(adventurers.ElementAt(0).AxeHorizontal, Is.EqualTo(0));
            Assert.That(adventurers.ElementAt(0).AxeVertical, Is.EqualTo(1));
            Assert.That(adventurers.ElementAt(0).Orientation, Is.EqualTo(Orientation.N));
            Assert.That(adventurers.ElementAt(0).MovementSequency, Is.EqualTo("AADADAGGA"));

            Assert.NotNull(cells);
            Assert.That(cells[2, 2].cellType, Is.EqualTo(TypeCell.Mountain));
            Assert.That(cells[0, 0].cellType, Is.EqualTo(TypeCell.Treasure));
            Assert.That((cells[0, 0] as TreasureCell).TreasureValue, Is.EqualTo(3));
        }

        [Test]
        public void CompleteTableTestKoNullParameter()
        {
            AbstractCell[,] cells = null;

            Assert.Throws<ArgumentNullException>(() => AbstractCell.CompleteTableWithPlainCell(ref cells));
        }

        [Test]
        public void CompleteTableTestEmpty()
        {
            string line = "C - 3 - 4";
            AbstractCell[,] cells = AbstractCell.CreateAbstractCellTableFromLine(line);

            AbstractCell.CompleteTableWithPlainCell(ref cells);

            Assert.NotNull(cells);
            Assert.That(cells.GetLength(0), Is.EqualTo(4));
            Assert.That(cells.GetLength(1), Is.EqualTo(3));
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    Assert.NotNull(cells[i, j]);
                }
            }
        }

        [Test]
        public void CompleteTableTestNotEmpty()
        {
            string line = "C - 3 - 1";
            AbstractCell[,] cells = AbstractCell.CreateAbstractCellTableFromLine(line);

            List<Adventurer> adventurers = new List<Adventurer>();

            List<string> lines = new List<string>()
            {
                "M - 0 - 0",
                "T - 1 - 0 - 3",
            };


            AbstractCell.AddCellInTable(ref cells, ref adventurers, lines);


            Assert.That(cells[0, 0].cellType, Is.EqualTo(TypeCell.Mountain));
            Assert.That(cells[0, 1].cellType, Is.EqualTo(TypeCell.Treasure));
            Assert.That(cells[0, 2].cellType, Is.EqualTo(TypeCell.Plain));
        }

        [Test]
        public void WriteMapInFileTest()
        {
            string line = "C - 3 - 1";
            AbstractCell[,] cells = AbstractCell.CreateAbstractCellTableFromLine(line);

            string result = AbstractCell.WriteMapInFile(ref cells);

            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo("C - 3 - 1"));
        }

        [Test]
        public void WriteMapInFileTestNull()
        {
            AbstractCell[,] cells = null;

            Assert.Throws<ArgumentNullException>(() => AbstractCell.WriteMapInFile(ref cells));
        }

        [Test]
        public void WriteCellsInFileTestNull()
        {
            AbstractCell[,] cells = null;

            Assert.Throws<ArgumentNullException>(() => AbstractCell.WriteCellsInFile(ref cells));
        }

        [Test]
        public void WriteCellsInFileTest()
        {
            string line = "C - 4 - 1";
            AbstractCell[,] cells = AbstractCell.CreateAbstractCellTableFromLine(line);

            List<Adventurer> adventurers = new List<Adventurer>();

            List<string> lines = new List<string>()
            {
                "M - 0 - 0",
                "T - 1 - 0 - 3",
                "A - Ben - 2 - 0 - S - AADA",
            };

            AbstractCell.AddCellInTable(ref cells, ref adventurers, lines);


            string result = AbstractCell.WriteCellsInFile(ref cells);

            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo("M - 0 - 0\r\nT - 1 - 0 - 3\r\nA - Ben - 2 - 0 - S - 0\r\n"));

        }

        
    }
}

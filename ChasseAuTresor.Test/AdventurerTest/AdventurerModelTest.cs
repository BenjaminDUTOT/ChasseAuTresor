using ChasseAuTresor.Model.AdventurerModel;
using ChasseAuTresor.CellModel;
using ChasseAuTresor.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChasseAuTresor.Test.AdventurerTest
{
    internal class AdventurerModelTest
    {
        [Test]
        public void GetDisplayTextTestDefaultValue()
        {
            Adventurer adventurer = new Adventurer();

            string result = adventurer.GetDisplayText();

            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo("A()"));
        }

        [Test]
        public void GetDisplayTextTestName()
        {
            Adventurer adventurer = new Adventurer()
            {
                Name = "Test",
            };

            string result = adventurer.GetDisplayText();

            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo("A(Test)"));
        }

        [Test]
        public void BuildAdventurerTestKoNullData()
        {
            Assert.Throws<ArgumentNullException>(() => Adventurer.BuildAdventurer(null));
        }

        [Test]
        public void BuildAdventurerTestKoTooManyData()
        {
            string[] datas =
            {
                "A",
                "Ben",
                "0",
                "1",
                "S",
                "AA",
                "1"
            };

            Assert.Throws<ArgumentException>(() => Adventurer.BuildAdventurer(datas));
        }

        [Test]
        public void BuildAdventurerTestKoTooFewData()
        {
            string[] datas =
            {
                "A",
                "Ben",
                "0",
                "1",
                "S"
            };

            Assert.Throws<ArgumentException>(() => Adventurer.BuildAdventurer(datas));
        }

        [Test]
        public void BuildAdventurerTestOk()
        {
            string[] datas =
            {
                "A",
                "Ben",
                "0",
                "1",
                "S",
                "AA"
            };

            Adventurer adventurer = Adventurer.BuildAdventurer(datas);

            Assert.NotNull(adventurer);
            Assert.That(adventurer.Name, Is.EqualTo("Ben"));
            Assert.That(adventurer.AxeHorizontal, Is.EqualTo(0));
            Assert.That(adventurer.AxeVertical, Is.EqualTo(1));
            Assert.That(adventurer.Orientation, Is.EqualTo(Orientation.S));
            Assert.That(adventurer.MovementSequency, Is.EqualTo("AA"));
            Assert.That(adventurer.TreasureValue, Is.EqualTo(0));
        }

        [Test]
        public void WriteAdventurerTestKoNullData()
        {
            Assert.Throws<ArgumentNullException>(() => Adventurer.WriteAdventurer(null));
        }

        [Test]
        public void WriteAdventurerTestOk()
        {
            Adventurer adventurer = new Adventurer()
            {
                Name = "Ben",
                AxeHorizontal = 0,
                AxeVertical = 1,
                Orientation = Orientation.S,
                TreasureValue = 10,
            };

            string result = Adventurer.WriteAdventurer(adventurer);

            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo("A - Ben - 0 - 1 - S - 10"));
        }

        [Test]
        public void RealiseActionTestKoAdventurerNull()
        {
            Adventurer adventurer = null;
            AbstractCell[,] cells = new AbstractCell[1, 1];
            Assert.Throws<ArgumentNullException>(() => Adventurer.RealiseAction('A', adventurer, ref cells));
        }

        [Test]
        public void RealiseActionTestKoCellsNull()
        {
            Adventurer adventurer = new Adventurer();
            AbstractCell[,] cells = null;
            Assert.Throws<ArgumentNullException>(() => Adventurer.RealiseAction('A', adventurer, ref cells));
        }

        [Test]
        public void RealiseActionTestKoIncorrectAction()
        {
            Adventurer adventurer = new Adventurer()
            {
                Name = "Ben"
            };
            AbstractCell[,] cells = new AbstractCell[1, 1];
            
            ArgumentException argumentException = Assert.Throws<ArgumentException>(() => Adventurer.RealiseAction('f', adventurer, ref cells));
            Assert.That(argumentException.Message, Is.EqualTo("Une erreur c'est produite lors de la réalisation d'une action. L'action : f de l'aventurier : Ben n'est pas reconnu"));
        }

        [Test]
        public void RealiseActionTestOkA()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();
            Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers);

            Assert.DoesNotThrow(() => Adventurer.RealiseAction('A', adventurers.First(), ref cells));
        }

        [Test]
        public void RealiseActionTestOka()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();
            Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers);

            Assert.DoesNotThrow(() => Adventurer.RealiseAction('a', adventurers.First(), ref cells));
        }

        [Test]
        public void RealiseActionTestOkD()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();
            Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers);

            Assert.DoesNotThrow(() => Adventurer.RealiseAction('D', adventurers.First(), ref cells));
        }

        [Test]
        public void RealiseActionTestOkd()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();
            Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers);

            Assert.DoesNotThrow(() => Adventurer.RealiseAction('d', adventurers.First(), ref cells));
        }

        [Test]
        public void RealiseActionTestOkG()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();
            Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers);

            Assert.DoesNotThrow(() => Adventurer.RealiseAction('G', adventurers.First(), ref cells));
        }

        [Test]
        public void RealiseActionTestOkg()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();
            Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers);

            Assert.DoesNotThrow(() => Adventurer.RealiseAction('g', adventurers.First(), ref cells));
        }

        [Test]
        public void TurnLeftTestOkN()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.N,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("TurnLeft", BindingFlags.NonPublic | BindingFlags.Static);

            method.Invoke(adventurerObject, parameters);

            Assert.NotNull(adventurer);
            Assert.That(adventurer.Orientation, Is.EqualTo(Orientation.O));
        }

        [Test]
        public void TurnLeftTestOkE()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.E,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("TurnLeft", BindingFlags.NonPublic | BindingFlags.Static);

            method.Invoke(adventurerObject, parameters);

            Assert.NotNull(adventurer);
            Assert.That(adventurer.Orientation, Is.EqualTo(Orientation.N));
        }

        [Test]
        public void TurnLeftTestOkS()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.S,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("TurnLeft", BindingFlags.NonPublic | BindingFlags.Static);

            method.Invoke(adventurerObject, parameters);

            Assert.NotNull(adventurer);
            Assert.That(adventurer.Orientation, Is.EqualTo(Orientation.E));
        }

        [Test]
        public void TurnLeftTestOkO()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.O,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("TurnLeft", BindingFlags.NonPublic | BindingFlags.Static);

            method.Invoke(adventurerObject, parameters);

            Assert.NotNull(adventurer);
            Assert.That(adventurer.Orientation, Is.EqualTo(Orientation.S));
        }

        [Test]
        public void TurnRightTestOkN()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.N,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("TurnRight", BindingFlags.NonPublic | BindingFlags.Static);

            method.Invoke(adventurerObject, parameters);

            Assert.NotNull(adventurer);
            Assert.That(adventurer.Orientation, Is.EqualTo(Orientation.E));
        }

        [Test]
        public void TurnRightTestOkE()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.E,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("TurnRight", BindingFlags.NonPublic | BindingFlags.Static);

            method.Invoke(adventurerObject, parameters);

            Assert.NotNull(adventurer);
            Assert.That(adventurer.Orientation, Is.EqualTo(Orientation.S));
        }

        [Test]
        public void TurnRightTestOkS()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.S,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("TurnRight", BindingFlags.NonPublic | BindingFlags.Static);

            method.Invoke(adventurerObject, parameters);

            Assert.NotNull(adventurer);
            Assert.That(adventurer.Orientation, Is.EqualTo(Orientation.O));
        }

        [Test]
        public void TurnRightTestOkO()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.O,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("TurnRight", BindingFlags.NonPublic | BindingFlags.Static);

            method.Invoke(adventurerObject, parameters);

            Assert.NotNull(adventurer);
            Assert.That(adventurer.Orientation, Is.EqualTo(Orientation.N));
        }

        [Test]
        public void GetNextPositionTestOkN()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.N,
                AxeHorizontal = 0,
                AxeVertical = 0,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("GetNextPosition", BindingFlags.NonPublic | BindingFlags.Static);

            Coordinate coordinate = method.Invoke(adventurerObject, parameters) as Coordinate;

            Assert.NotNull(coordinate);
            Assert.That(coordinate.AxeHorizontal, Is.EqualTo(0));
            Assert.That(coordinate.AxeVertical, Is.EqualTo(-1));
        }

        [Test]
        public void GetNextPositionTestOkE()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.E,
                AxeHorizontal = 0,
                AxeVertical = 0,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("GetNextPosition", BindingFlags.NonPublic | BindingFlags.Static);

            Coordinate coordinate = method.Invoke(adventurerObject, parameters) as Coordinate;

            Assert.NotNull(coordinate);
            Assert.That(coordinate.AxeHorizontal, Is.EqualTo(1));
            Assert.That(coordinate.AxeVertical, Is.EqualTo(0));
        }

        [Test]
        public void GetNextPositionTestOkS()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.S,
                AxeHorizontal = 0,
                AxeVertical = 0,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("GetNextPosition", BindingFlags.NonPublic | BindingFlags.Static);

            Coordinate coordinate = method.Invoke(adventurerObject, parameters) as Coordinate;

            Assert.NotNull(coordinate);
            Assert.That(coordinate.AxeHorizontal, Is.EqualTo(0));
            Assert.That(coordinate.AxeVertical, Is.EqualTo(1));
        }

        [Test]
        public void GetNextPositionTestOkO()
        {
            Adventurer adventurer = new Adventurer()
            {
                Orientation = Orientation.O,
                AxeHorizontal = 0,
                AxeVertical = 0,
            };
            object[] parameters = { adventurer };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("GetNextPosition", BindingFlags.NonPublic | BindingFlags.Static);

            Coordinate coordinate = method.Invoke(adventurerObject, parameters) as Coordinate;

            Assert.NotNull(coordinate);
            Assert.That(coordinate.AxeHorizontal, Is.EqualTo(-1));
            Assert.That(coordinate.AxeVertical, Is.EqualTo(0));
        }

        [Test]
        public void IsInTheMapTestFalseHorizontalUnder0()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = -1,
                AxeVertical = 0,
            };
            AbstractCell[,] cells = new AbstractCell[1,1];
            object[] parameters = { coordinate, cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("IsInTheMap", BindingFlags.NonPublic | BindingFlags.Static);

            bool? result = method.Invoke(adventurerObject, parameters) as bool?;

            Assert.NotNull(result);
            Assert.False(result);
        }

        [Test]
        public void IsInTheMapTestFalseVerticalUnder0()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = 0,
                AxeVertical = -1,
            };
            AbstractCell[,] cells = new AbstractCell[1, 1];
            object[] parameters = { coordinate, cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("IsInTheMap", BindingFlags.NonPublic | BindingFlags.Static);

            bool? result = method.Invoke(adventurerObject, parameters) as bool?;

            Assert.NotNull(result);
            Assert.False(result);
        }

        [Test]
        public void IsInTheMapTestFalseVerticalOver0()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = 0,
                AxeVertical = 1,
            };
            AbstractCell[,] cells = new AbstractCell[1, 1];
            object[] parameters = { coordinate, cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("IsInTheMap", BindingFlags.NonPublic | BindingFlags.Static);

            bool? result = method.Invoke(adventurerObject, parameters) as bool?;

            Assert.NotNull(result);
            Assert.False(result);
        }

        [Test]
        public void IsInTheMapTestFalseHorizontalOver0()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = 1,
                AxeVertical = 0,
            };
            AbstractCell[,] cells = new AbstractCell[1, 1];
            object[] parameters = { coordinate, cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("IsInTheMap", BindingFlags.NonPublic | BindingFlags.Static);

            bool? result = method.Invoke(adventurerObject, parameters) as bool?;

            Assert.NotNull(result);
            Assert.False(result);
        }

        [Test]
        public void IsInTheMapTestTrue()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = 0,
                AxeVertical = 0,
            };
            AbstractCell[,] cells = new AbstractCell[1, 1];
            object[] parameters = { coordinate, cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("IsInTheMap", BindingFlags.NonPublic | BindingFlags.Static);

            bool? result = method.Invoke(adventurerObject, parameters) as bool?;

            Assert.NotNull(result);
            Assert.True(result);
        }

        [Test]
        public void CanMoveFalseNoInMap()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = 0,
                AxeVertical = 1,
            };
            AbstractCell[,] cells = new AbstractCell[1, 1];
            AbstractCell.CompleteTableWithPlainCell(ref cells);
            object[] parameters = { coordinate, cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("CanMove", BindingFlags.NonPublic | BindingFlags.Static);

            bool? result = method.Invoke(adventurerObject, parameters) as bool?;

            Assert.NotNull(result);
            Assert.False(result);
        }

        [Test]
        public void CanMoveFalseNoInMapAndNotReachAble()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = 0,
                AxeVertical = 1,
            };
            AbstractCell[,] cells = new AbstractCell[1, 1];
            MountainCell.AddMountainCellInTable(ref cells, "M - 0 - 0");
            object[] parameters = { coordinate, cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("CanMove", BindingFlags.NonPublic | BindingFlags.Static);

            bool? result = method.Invoke(adventurerObject, parameters) as bool?;

            Assert.NotNull(result);
            Assert.False(result);
        }

        [Test]
        public void CanMoveFalseNotReachAble()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = 0,
                AxeVertical = 0,
            };
            AbstractCell[,] cells = new AbstractCell[1, 1];
            MountainCell.AddMountainCellInTable(ref cells, "M - 0 - 0");
            object[] parameters = { coordinate, cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("CanMove", BindingFlags.NonPublic | BindingFlags.Static);

            bool? result = method.Invoke(adventurerObject, parameters) as bool?;

            Assert.NotNull(result);
            Assert.False(result);
        }

        [Test]
        public void CanMoveTestTrue()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = 0,
                AxeVertical = 1,
            };
            AbstractCell[,] cells = new AbstractCell[1, 1];
            AbstractCell.CompleteTableWithPlainCell(ref cells);
            object[] parameters = { coordinate, cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("CanMove", BindingFlags.NonPublic | BindingFlags.Static);

            bool? result = method.Invoke(adventurerObject, parameters) as bool?;

            Assert.NotNull(result);
            Assert.False(result);
        }

        [Test]
        public void MakeMovementOk()
        {
            Coordinate coordinate = new Coordinate()
            {
                AxeHorizontal = 0,
                AxeVertical = 1,
            };
            AbstractCell[,] cells = new AbstractCell[2, 2];
            List<Adventurer> adventurers = new List<Adventurer>();
            Adventurer.AddAdventurerInTable(ref cells, "A - Lara - 0 - 0 - S - AAAA", ref adventurers);
            AbstractCell.CompleteTableWithPlainCell(ref cells);
            object[] parameters = { coordinate, adventurers.First(), cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("MakeMovement", BindingFlags.NonPublic | BindingFlags.Static);

            method.Invoke(adventurerObject, parameters);

            Assert.NotNull(cells);
            Assert.Null((cells[0,0] as PlainCell).Adventurer);
            Assert.NotNull((cells[1, 0] as PlainCell).Adventurer);

            Assert.NotNull(adventurers.First());
            Assert.That(adventurers.First().AxeHorizontal, Is.EqualTo(0));
            Assert.That(adventurers.First().AxeVertical, Is.EqualTo(1));
        }

        [Test]
        public void MovementActionOk()
        {
            AbstractCell[,] cells = new AbstractCell[2, 2];
            List<Adventurer> adventurers = new List<Adventurer>();
            Adventurer.AddAdventurerInTable(ref cells, "A - Lara - 0 - 0 - S - AAAA", ref adventurers);
            AbstractCell.CompleteTableWithPlainCell(ref cells);
            object[] parameters = { adventurers.First(), cells };

            Type type = typeof(Adventurer);
            object adventurerObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("MovementAction", BindingFlags.NonPublic | BindingFlags.Static);

            Assert.DoesNotThrow(() => method.Invoke(adventurerObject, parameters));
        }

        [Test]
        public void AddAdventurerInTableTestKoLineNull()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = null;
            List<Adventurer> adventurers = new List<Adventurer>();

            Assert.Throws<ArgumentNullException>(() => Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers));
        }

        [Test]
        public void AddAdventurerInTableTestKoCellsNull()
        {
            AbstractCell[,] cells = null;
            string line = "A - Lara - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();

            Assert.Throws<ArgumentNullException>(() => Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers));
        }

        [Test]
        public void AddAdventurerInTableTestKoAdventurerNull()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = null;

            Assert.Throws<ArgumentNullException>(() => Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers));
        }

        [Test]
        public void AddAdventurerInTableTestKoToManyParameter()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S - AAAA - 1";
            List<Adventurer> adventurers = new List<Adventurer>();

            Assert.Throws<ArgumentException>(() => Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers));
        }

        [Test]
        public void AddAdventurerInTableTestKoToFewParameter()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S";
            List<Adventurer> adventurers = new List<Adventurer>();

            Assert.Throws<ArgumentException>(() => Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers));
        }

        [Test]
        public void AddAdventurerInTableTestKoNotInCell()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 1 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();

            Assert.Throws<ArgumentException>(() => Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers));
        }

        [Test]
        public void AddAdventurerInTableTestKoMountainCell()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string lineA = "A - Lara - 0 - 0 - S - AAAA";
            string lineM = "M - 0 - 0";
            List<Adventurer> adventurers = new List<Adventurer>();
            MountainCell.AddMountainCellInTable(ref cells, lineM);

            Assert.Throws<ArgumentException>(() => Adventurer.AddAdventurerInTable(ref cells, lineA, ref adventurers));
        }

        [Test]
        public void AddAdventurerInTableTestKoAnotherAdventurer()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string lineLara = "A - Lara - 0 - 0 - S - AAAA";
            string lineBen = "A - Ben - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();
            Adventurer.AddAdventurerInTable(ref cells, lineLara, ref adventurers);

            Assert.Throws<ArgumentException>(() => Adventurer.AddAdventurerInTable(ref cells, lineBen, ref adventurers));
        }

        [Test]
        public void AddAdventurerInTableTestOkEmptyCell()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string line = "A - Lara - 0 - 0 - S - AAAA";
            List<Adventurer> adventurers = new List<Adventurer>();

            Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers);

            Assert.NotNull(cells);
            Assert.That(cells[0, 0].cellType, Is.EqualTo(TypeCell.Plain));
            Assert.That(cells[0, 0].AxeHorizontal, Is.EqualTo(0));
            Assert.That(cells[0, 0].AxeVertical, Is.EqualTo(0));
            Assert.NotNull((cells[0, 0] as PlainCell).Adventurer);
            Assert.NotNull(adventurers);
            Assert.That(adventurers.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddAdventurerInTableTestOkTreasureCell()
        {
            AbstractCell[,] cells = new AbstractCell[1, 1];
            string lineA = "A - Lara - 0 - 0 - S - AAAA";
            string lineT = "T - 0 - 0 - 3";
            List<Adventurer> adventurers = new List<Adventurer>();
            TreasureCell.AddTreasureCellInTable(ref cells, lineT);

            Adventurer.AddAdventurerInTable(ref cells, lineA, ref adventurers);

            Assert.NotNull(cells);
            Assert.That(cells[0, 0].cellType, Is.EqualTo(TypeCell.Treasure));
            Assert.That(cells[0, 0].AxeHorizontal, Is.EqualTo(0));
            Assert.That(cells[0, 0].AxeVertical, Is.EqualTo(0));
            Assert.NotNull((cells[0, 0] as TreasureCell).Adventurer);
            Assert.NotNull(adventurers);
            Assert.That(adventurers.Count, Is.EqualTo(1));
        }
    }
}

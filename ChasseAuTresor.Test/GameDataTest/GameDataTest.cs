using ChasseAuTresor.CellModel;
using ChasseAuTresor.GameDataModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChasseAuTresor.Test.GameDataTest
{
    internal class GameDataTest
    {
        [Test]
        public void ReadFromFileTestOk()
        {
            GameData gameData = new GameData();
            List<string> lines = new List<string>()
            {
                "C - 3 - 4",
                "M - 1 - 0",
                "T - 1 - 3 - 3",
                "A - Lara - 1 - 1 - S - AADADAGGA"
            };

            gameData.ReadGameDataFromLines(lines);

            Assert.NotNull(gameData);
            Assert.NotNull(gameData.cells);
            Assert.NotNull(gameData.adventurers);
        }

        [Test]
        public void ReadFromFileTestKoNoLines()
        {
            GameData gameData = new GameData();
            List<string> lines = new List<string>();

            Assert.Throws<ArgumentException>(() => gameData.ReadGameDataFromLines(lines));
        }

        [Test]
        public void ReadFromFileTestKoNull()
        {
            GameData gameData = new GameData();
            List<string> lines = new List<string>();

            Assert.Throws<ArgumentException>(() => gameData.ReadGameDataFromLines(lines));
        }

        [Test]
        public void ReadFromFileTestKoNoC()
        {
            GameData gameData = new GameData();
            List<string> lines = new List<string>()
            {
                "M - 1 - 0",
                "T - 1 - 3 - 3",
                "A - Lara - 1 - 1 - S - AADADAGGA"
            };

            Assert.Throws<ArgumentException>(() => gameData.ReadGameDataFromLines(lines));
        }

        [Test]
        public void WriteToFileTestOk()
        {
            GameData gameData = new GameData();
            List<string> lines = new List<string>()
            {
                "C - 3 - 4",
                "M - 1 - 0",
                "T - 1 - 3 - 3",
                "A - Lara - 1 - 1 - S - AADADAGGA"
            };

            gameData.ReadGameDataFromLines(lines);
            
            string result = gameData.WriteToFile();

            Assert.NotNull(result);
            Assert.That(result.Contains("# {C comme Carte} - {Nb. de case en largeur} - {Nb. de case en hauteur}"));
            Assert.That(result.Contains("# {M comme Montagne} - {Axe horizontal} - {Axe vertical}"));
            Assert.That(result.Contains("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}"));
            Assert.That(result.Contains("# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Nb.trésors ramassés}"));
        }

        [Test]
        public void PlaySimulationTestOkDifferentSequenceSize()
        {
            GameData gameData = new GameData();
            List<string> lines = new List<string>()
            {
                "C - 3 - 4",
                "M - 1 - 0",
                "T - 1 - 3 - 3",
                "A - Lara - 0 - 1 - S - AADADAGGA",
                "A - Ben - 1 - 1 - S - AADA"
            };

            gameData.ReadGameDataFromLines(lines);
            gameData.PlaySimulation();


            Assert.That((gameData.cells[3, 0] as PlainCell).Adventurer != null);
            Assert.That((gameData.cells[3, 1] as TreasureCell).Adventurer != null);
            Assert.That(gameData.cells[1, 0].cellType != TypeCell.Mountain);
            Assert.That(gameData.cells[1, 0].cellType != TypeCell.Treasure);
        }

        [Test]
        public void PlaySimulationTestOkNoAdventurer()
        {
            GameData gameData = new GameData();
            List<string> lines = new List<string>()
            {
                "C - 3 - 4",
                "M - 1 - 0",
                "T - 1 - 3 - 3",
            };

            gameData.ReadGameDataFromLines(lines);
            gameData.PlaySimulation();

            Assert.That(gameData.cells[1, 0].cellType != TypeCell.Mountain);
            Assert.That(gameData.cells[1, 0].cellType != TypeCell.Treasure);
        }
    }
}

#if UNITY_EDITOR
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSudoku
{


    [TestClass]
    public class TestSudoku
    {
        private IEnumerable<Tile> Build(params int[] values)
            => values.Select(v => { var t = new Tile(); t.SetValue(v); return t; });

        private void setListTileValue(object[] obj, int[] i)
        {
            foreach (Tile t in obj) t.SetValue(i[0]);
        }

        [TestMethod]
        public void TestAucunDoublon()
            => Assert.AreEqual(true, Tile.CheckDoubles(Build(1, 2, 3, 4, 5, 6, 7, 8, 9)));

        [TestMethod]
        public void TestDoublon()
            => Assert.AreEqual(false, Tile.CheckDoubles(Build(1, 1, 3, 4, 5, 6, 7, 8, 9)));

        [TestMethod]
        public void TestCaseVide()
            => Assert.AreEqual(true, Tile.CheckDoubles(Build(0, 2, 3, 4, 5, 6, 7, 8, 9)));

        [TestMethod]
        public void TestCaseVideDoublon()
            => Assert.AreEqual(false, Tile.CheckDoubles(Build(0, 2, 2, 4, 5, 6, 7, 8, 9)));

        [TestMethod]
        public void TestValeursVides()
            => Assert.AreEqual(true, Tile.CheckDoubles(Build(0, 0, 0, 0, 0, 0, 0, 0, 0)));

        [TestMethod]
        public void TestTousDoublons()
            => Assert.AreEqual(false, Tile.CheckDoubles(Build(1, 1, 1, 1, 1, 1, 1, 1, 1)));

        [TestMethod]
        public void TestExecute()
        {
            Tile t = new Tile();
            t.SetValue(0);
            CommandInvoker.Instance.Execute(
                new GenericCommand<int>(new Tile[] { t }, setListTileValue, new int[] { t.Value }, new int[] { 1 })
            );
            Assert.AreEqual(1, t.Value);
        }
        
        [TestMethod]
        public void TestUndo()
        {
            Tile t = new Tile();
            t.SetValue(0);
            CommandInvoker.Instance.Execute(
                new GenericCommand<int>(new Tile[] { t }, setListTileValue, new int[] { t.Value }, new int[] { 1 })
            );
            CommandInvoker.Instance.Undo();
            Assert.AreEqual(0, t.Value);
        }

        [TestMethod]
        public void TestUndoRedo()
        {
            Tile t = new Tile();
            t.SetValue(0);
            CommandInvoker.Instance.Execute(
                new GenericCommand<int>(new Tile[] { t }, setListTileValue, new int[] { t.Value }, new int[] { 1 })
            );
            CommandInvoker.Instance.Undo();
            CommandInvoker.Instance.Redo();
            Assert.AreEqual(1, t.Value);
        }

    }

}
#endif
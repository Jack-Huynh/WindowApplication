using Microsoft.VisualStudio.TestTools.UnitTesting;
using M01_First_WPF_Proj;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace M01_First_WPF_Proj.Tests
{
    [TestClass()]
    public class GroupingTests
    {
        [TestMethod()]
        public void GetGroup_EmptyWorkingList_ReturnsEmptyGroup()
        {
            int distanceThreshold = 100;
            Grouping test = new Grouping(distanceThreshold, 0);

            List<List<Point>> group = test.applyThreseholdsMakeGroups();

            Assert.AreEqual(0, group.Count);
        }

        [TestMethod()]
        public void GetGroup_SinglePointInWorkingList_ReturnsSinglePointGroup()
        {
            Grouping group = new Grouping(100, 1);
            List<Point> workingList = new List<Point> { new Point(10, 10) };

            List<Point> result = group.GetGroup(workingList, 100);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new Point(10, 10), result[0]);
        }

        [TestMethod]
        public void GetGroup_TwoPointsWithinThreshold_ReturnsSingleGroups()
        {
            int distanceThreshold = 100;
            Grouping test = new Grouping(distanceThreshold, 2);
            test.MasterList[0] = new Point(0, 0);
            test.MasterList[1] = new Point(50, 50);

            List<List<Point>> group = test.applyThreseholdsMakeGroups();

            Assert.AreEqual(1, group.Count);
        }

        [TestMethod]
        public void GetGroup_TwoPointsOutsideThreshold_ReturnsTwoGroup()
        {
            int distanceThreshold = 100;
            Grouping test = new Grouping(distanceThreshold, 2);
            test.MasterList[0] = new Point(0, 0);
            test.MasterList[1] = new Point(150, 150);

            List<List<Point>> group = test.applyThreseholdsMakeGroups();

            Assert.AreEqual(2, group.Count);
        }

        [TestMethod]
        public void GetGroup_MixedPointsWithinAndOutsideThreshold_ReturnsMultipleGroups()
        {
            int distanceThreshold = 100;
            Grouping test = new Grouping(distanceThreshold, 3);
            test.MasterList[0] = new Point(0, 0);
            test.MasterList[1] = new Point(100, 100);
            test.MasterList[2] = new Point(150, 150);

            List<List<Point>> group = test.applyThreseholdsMakeGroups();

            Assert.AreEqual(2, group.Count);
        }
    }
}
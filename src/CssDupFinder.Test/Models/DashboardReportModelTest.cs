using CssDupFinder.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssDupFinder.Test.Models
{
    [TestClass]
    public class DashboardReportModelTest
    {
        [TestMethod]
        public void GivenInstantiableModel_PropertyLinksShouldBeEmtpyNotNull()
        {
            var model = new DashboardReportModel();
            model.Links.Should().NotBeNull().And.BeEmpty();
        }

        [TestMethod]
        public void WhenAddOneLinkAnalysis_LinksShouldHaveCountOne()
        {
            var model = new DashboardReportModel();
            model.AddAnalysis("index.html", 2, 2);

            model.Links.Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenAddTwoLinksWithThreeSelectors_TotalSelectorsShouldHaveThree()
        {
            var model = new DashboardReportModel();
            model.AddAnalysis("index.html", 1, 0);
            model.AddAnalysis("default.html", 2, 1);

            model.TotalSelectors.Should().Be(3);
        }

        [TestMethod]
        public void WhenAddTwoLinksWithSevenDuplicates_TotalSDuplicatesShouldHaveSeven()
        {
            var model = new DashboardReportModel();
            model.AddAnalysis("index.html", 1, 5);
            model.AddAnalysis("default.html", 2, 2);

            model.TotalSelectors.Should().Be(3);
        }
    }
}
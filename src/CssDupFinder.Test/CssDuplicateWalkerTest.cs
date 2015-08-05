using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CssDupFinder.Test
{
    [TestClass]
    public class CssDuplicateWalkerTest
    {
        [TestMethod]
        public void GiveOneClassSelector_DuplicatesShouldBeEmpty()
        {
            var walker = @"
    .btn {
        margin-bottom: 10px;
    }".Interpret<CssDuplicateWalker>();

            walker.Duplicates.Should().BeEmpty();
        }

        [TestMethod]
        public void GiveTwoDifferentClassSelector_DuplicatesShouldBeEmpty()
        {
            var walker = @"
    .btn.warning {
        color: yellow;
    }

    .btn.error {
        color: red;
    }
            ".Interpret<CssDuplicateWalker>();

            walker.Duplicates.Should().BeEmpty();
        }

        /*[TestMethod]
        public void GiveTwoDuplicatedClassSelector_DuplicatesShouldHaveCountOne()
        {
            var walker = @"
    .btn {
        margin-bottom: 10px;
    }

    .btn {
        margin-top: 10px
    }
            ".Interpret<CssDuplicateWalker>();

            walker.Duplicates.Should().HaveCount(1);

            var list = walker.Duplicates.First().Value;

            list.Should().HaveCount(2);
            list[0].Text.Trim().Should().Be(@".btn {
  margin-bottom: 10px;
}");
            list[1].Text.Trim().Should().Be(@".btn {
  margin-top: 10px;
}");
        }*/

        [TestMethod]
        public void GiveOneIdSelector_DuplicatesShouldBeEmpty()
        {
            var walker = @"
    #modalError {
        color: red;
    }".Interpret<CssDuplicateWalker>();

            walker.Duplicates.Should().BeEmpty();
        }

        [TestMethod]
        public void GiveTwoDifferentIdSelector_DuplicatesShouldBeEmpty()
        {
            var walker = @"
    #single-modal {
        color: red;
    }

    #summary-errors {
        padding: 10px;
    }".Interpret<CssDuplicateWalker>();

            walker.Duplicates.Should().BeEmpty();
        }

        /*[TestMethod]
        public void Test()
        {
            var walker = @"
@media only screen {
    #single-modal {
        color: red;
    }
}
@media only screen {
    #single-modal {
        color: red;
    }
}".Interpret<CssDuplicateWalker>();

            walker.Duplicates.Should().HaveCount(1);

            var list = walker.Duplicates.First().Value;

            list.Should().HaveCount(2);
        }*/
    }
}
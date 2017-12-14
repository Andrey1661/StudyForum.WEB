using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyForum.DataAccess.Utils;

namespace StudyForum.DataAccess.EF.Tests
{
    [TestClass]
    public class ExpressionExtensionsTest
    {
        [TestMethod]
        public void AndAlsoTest()
        {
            Expression<Func<string, bool>> expression = s => s.Equals("111");
            expression = expression.AndAlso(s => s.Length > 3);
            var str = expression.ToString();

            var func = expression.Compile();
            bool result = func("111");

            Assert.IsFalse(result);
        }

        public void OrEvenTest()
        {

        }
    }
}

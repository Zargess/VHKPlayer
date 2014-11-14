// <copyright file="PathHandlerTest.cs">Copyright ©  2014</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.UtilFunctions
{
    [TestClass]
    [PexClass(typeof(PathHandler))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PathHandlerTest
    {
        [PexMethod]
        public string[] SplitPath(string path) {
            string[] result = PathHandler.SplitPath(path);
            return result;
            // TODO: add assertions to method PathHandlerTest.SplitPath(String)
        }
    }
}

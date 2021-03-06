//------------------------------------------------------------------------------
// <copyright file="MetadataTest.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation
// </copyright>
//------------------------------------------------------------------------------

namespace DMLibTest
{
    using System.Collections.Generic;
    using DMLibTestCodeGen;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MS.Test.Common.MsTestLib;

    [MultiDirectionTestClass]
    public class MetadataTest : DMLibTestBase
    {
        #region Additional test attributes
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            DMLibTestBase.BaseClassInitialize(testContext);
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            DMLibTestBase.BaseClassCleanup();
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            base.BaseTestInitialize();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            base.BaseTestCleanup();
        }
        #endregion

        [TestCategory(Tag.Function)]
        [DMLibTestMethodSet(DMLibTestMethodSet.Cloud2Cloud)]
        public void TestMetadata()
        {
            Dictionary<string, string> metadata = new Dictionary<string, string>();
            metadata.Add(FileOp.NextCIdentifierString(random), FileOp.NextNormalString(random));
            metadata.Add(FileOp.NextCIdentifierString(random), FileOp.NextNormalString(random));

            Test.Info("Metadata is =====================");
            foreach (var keyValue in metadata)
            {
                Test.Info("name:{0}  value:{1}", keyValue.Key, keyValue.Value);
            }

            DMLibDataInfo sourceDataInfo = new DMLibDataInfo(string.Empty);
            FileNode fileNode = new FileNode(DMLibTestBase.FileName)
            {
                SizeInByte = DMLibTestBase.FileSizeInKB * 1024L,
                Metadata = metadata
            };
            sourceDataInfo.RootNode.AddFileNode(fileNode);

            var result = this.ExecuteTestCase(sourceDataInfo, new TestExecutionOptions<DMLibDataInfo>());

            Test.Assert(result.Exceptions.Count == 0, "Verify no exception is thrown.");
            Test.Assert(DMLibDataHelper.Equals(sourceDataInfo, result.DataInfo), "Verify transfer result.");
        }
    }
}

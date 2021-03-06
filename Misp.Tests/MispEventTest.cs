using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Misp;
using System.Collections.Generic;

namespace Misp.Tests
{
    /// <summary>This class contains parameterized unit tests for MispEvent</summary>
    [TestClass]
    [PexClass(typeof(MispEvent))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public class MispEventTest
    {

        public static void AreEqualMinimum(MispEvent expected, MispEvent actual)
        {
            Assert.AreEqual(expected.Info, actual.Info);
        }

        public static void AreEqualFull(MispEvent expected, MispEvent actual)
        {
            AreEqualMinimum(expected, actual);

            Assert.AreEqual(expected.AnalysisLevel, actual.AnalysisLevel);
            Assert.AreEqual(expected.AttributeCount, actual.AttributeCount);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.DisableCorrelation, actual.DisableCorrelation);
            Assert.AreEqual(expected.Distribution, actual.Distribution);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Info, actual.Info);
            Assert.AreEqual(expected.Locked, actual.Locked);
            Assert.AreEqual(expected.OrgCId, actual.OrgCId);
            Assert.AreEqual(expected.OrgId, actual.OrgId);
            Assert.AreEqual(expected.Published, actual.Published);
            Assert.AreEqual(expected.PublishTimestamp, actual.PublishTimestamp);
            Assert.AreEqual(expected.ThreatLevelId, actual.ThreatLevelId);
            Assert.AreEqual(expected.Timestamp, actual.Timestamp);
            Assert.AreEqual(expected.UUID, actual.UUID);

            if (expected.Attribute != null)
            {
                Assert.IsNotNull(actual.Attribute);
                Assert.AreEqual(expected.Attribute.Length, actual.Attribute.Length);
                foreach (Attribute a in expected.Attribute)
                {
                    Assert.IsTrue(TestHelper.Contains(actual.Attribute, a, (x,y) => {
                        return String.Compare(x.Type, y.Type, false) == 0
                        && String.Compare(x.Category, y.Category, false) == 0
                        && String.Compare(x.Value, y.Value, true) == 0; }));
                }
            }

            if (expected.Tag != null)
            {
                Assert.IsNotNull(actual.Tag);
                Assert.AreEqual(expected.Tag.Length, actual.Tag.Length);
                foreach (Tag t in expected.Tag)
                {
                    Assert.IsTrue(TestHelper.Contains(actual.Tag, t, (x, y) => { return x.Name == y.Name && x.Color == y.Color && x.Exportable == y.Exportable; }));
                }

            }
            //Assert.AreEqual(expected.RelatedEvent, actual.RelatedEvent);
            if (expected.RelatedEvent != null)
            {
                Assert.IsNotNull(actual.RelatedEvent);
                Assert.AreEqual(expected.RelatedEvent.Length, actual.RelatedEvent.Length);
                foreach (RelatedEvent re in expected.RelatedEvent)
                {
                    Assert.IsTrue(TestHelper.Contains(actual.RelatedEvent, re, (x, y) => {                        
                        return x.Event.Id == y.Event.Id;
                    }));
               
                }
            }

            if (expected.Galaxy != null)
            {
                Assert.IsNotNull(actual.Galaxy);
                Assert.AreEqual(expected.Galaxy.Length, actual.Galaxy.Length);
                //TODO: Galaxy comparison
            }

            
            //Assert.AreEqual(expected.Org, actual.Org);
            //Assert.AreEqual(expected.OrgC, actual.OrgC);
            
            //Assert.AreEqual(expected.ShadowAttribute, actual.ShadowAttribute);
            //Assert.AreEqual(expected.SharingGroupId, actual.SharingGroupId);

        }


        /// <summary>Test stub for .ctor()</summary>
        [PexMethod]
        public MispEvent ConstructorTest()
        {
            MispEvent target = new MispEvent();
            Assert.IsNotNull(target);
            Assert.IsNotNull(target.Info);
            return target;
        }

        [TestMethod, TestCategory("NoServer")] public void cTor_Basic() { this.ConstructorTest(); }

        [TestMethod, TestCategory("NoServer")] public void Info_SizeLimit()
        {
            MispEvent evnt = this.ConstructorTest();
            evnt.Info = TestHelper.RandomString(500);
            Assert.AreEqual(256, evnt.Info.Length);
        }

        [TestMethod, TestCategory("NoServer")] public void Info_NewLineCleaning()
        {
            MispEvent evnt = this.ConstructorTest();
            String testStr = TestHelper.RandomSentance();
            evnt.Info = testStr.Replace(" ", Environment.NewLine);
            Assert.AreEqual(testStr, evnt.Info);
        }
        [TestMethod, TestCategory("NoServer")] 
        public void Parser_RoundTrip_Simple()
        {
            MispEvent expected = this.ConstructorTest();
            expected.Info = TestHelper.RandomSentance();
            String json = expected.ToString();
            MispEvent actual = MispEvent.FromJson(json);
            AreEqualMinimum(expected, actual);
        }
        [TestMethod, TestCategory("NoServer")]
        public void WrapperParser_RoundTrip_Simple()
        {
            MispEventWrapper expected = new MispEventWrapper(this.ConstructorTest());
            expected.Event.Info = TestHelper.RandomSentance();
            String json = expected.ToString();
            MispEventWrapper actual = MispEventWrapper.FromJson(json);
            Assert.IsNotNull(actual.Event);
            AreEqualMinimum(expected.Event, actual.Event);
        }

    }
}

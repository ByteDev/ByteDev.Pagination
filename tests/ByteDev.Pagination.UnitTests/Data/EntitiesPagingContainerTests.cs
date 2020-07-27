using System;
using System.Collections.Generic;
using ByteDev.Pagination.Data;
using NUnit.Framework;

namespace ByteDev.Pagination.UnitTests.Data
{
    [TestFixture]
    public class EntitiesPagingContainerTests
    {
        [TestFixture]
        public class Constructor
        {
            [Test]
            public void WhenEntitiesIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _ = new EntitiesPagingContainer<object>(null, 1000));
            }

            [Test]
            public void WhenTotalItemsCountIsLessThanZero_ThenThrowException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _ = new EntitiesPagingContainer<object>(new List<object>(), -1));
            }

            [Test]
            public void WhenEntitiesIsValid_ThenSetEntities()
            {
                var entities = new List<object>();

                var classUnderTest = new EntitiesPagingContainer<object>(entities, 10);

                Assert.That(classUnderTest.Entities, Is.SameAs(entities));
            }

            [Test]
            public void WhenTotalItemsCountIsValid_ThenSetTotalItemsCount()
            {
                var classUnderTest = new EntitiesPagingContainer<object>(new List<object>(), 1);

                Assert.That(classUnderTest.TotalItemsCount, Is.EqualTo(1));
            }

            [Test]
            public void WhenDataPagingInfoIsNotNull_ThenSetDataPagingInfo()
            {
                var dataPagingInfo = new DataPagingInfo();

                var classUnderTest = new EntitiesPagingContainer<object>(new List<object>(), 1, dataPagingInfo);

                Assert.That(classUnderTest.DataPagingInfo, Is.SameAs(dataPagingInfo));
            }
        }
    }
}
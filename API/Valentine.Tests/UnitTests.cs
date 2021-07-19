using System;
using System.Collections.Generic;
using System.Linq;
using Valentine.Application.Services;
using Valentine.Shared.Contracts.Responses;
using Xunit;

namespace Valentine.Tests
{
    public class UnitTests
    {
        [Fact]
        public void Layout_Initialize_Success()
        {
            var layout = new LayoutService();

            var areas = Enumerable.Repeat(new AreasCollectionItem
            {
                Id = Guid.NewGuid(),
                GeoPoints = new List<GeoPoint>
                {
                    new GeoPoint(0, 1, 0),
                    new GeoPoint(1, 2, 1),
                    new GeoPoint(2, 3, 2)
                }
            }, 1);

            var result = layout.InitializeMapAreas(areas);
            Assert.NotNull(result);
        }
    }
}

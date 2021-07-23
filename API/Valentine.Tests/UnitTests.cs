using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Valentine.Application.Enums;
using Valentine.Application.Models;
using Valentine.Application.Services;
using Valentine.Shared.Contracts.Models;
using Xunit;

namespace Valentine.Tests
{
    public class UnitTests
    {
        [Fact]
        public void Layout_Initialize_Success()
        {
            var layout = new LayoutService();

            var areas = Enumerable.Repeat(new AreaModel
            {
                Id = Guid.NewGuid(),
                GeoPoints = new List<GeoPointModel>
                {
                    new GeoPointModel(string.Empty, 0, 0, 1),
                    new GeoPointModel(string.Empty, 1, 1, 2),
                    new GeoPointModel(string.Empty, 2, 2, 3),
                }
            }, 1);

            var result = layout.InitializeMapAreas(areas);
            Assert.NotNull(result);
        }

        [Fact]
        public void FeatureCollection_Serialization_Correct()
        {
            var dynamicExpected = new
            {
                Features = new List<dynamic>
                {
                    new
                    {
                        Properties = (Dictionary<string, object>)null,
                        Geometry = new
                        {
                            Coordinates = new List<List<double[]>>
                            {
                                new List<double[]>
                                {
                                    new[] { 2.1, 5.2 }
                                }
                            },
                            Type = FeatureType.Polygon,
                        },
                        Type = FeatureType.Feature,
                    }
                },
                Type = FeatureType.FeatureCollection,
            };

            var featureCollection = new FeatureCollection<Polygon>
            {
                Type = FeatureType.FeatureCollection,
                Features = new List<Polygon>
                {
                    new Polygon
                    {
                        Type = FeatureType.Feature,
                        Geometry = new Geometry(FeatureType.Polygon, Enumerable.Repeat(new GeoPointCoordsCollection(new List<GeoPointCoordinates>
                        {
                            new GeoPointCoordinates(2.1, 5.2)
                        }), 1))
                    }
                }
            };

            var expectedJson = JsonSerializer.Serialize(dynamicExpected);
            var actualJson = JsonSerializer.Serialize(featureCollection);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void Geometry_Deserialization_Correct()
        {
            var json = "{\"type\":\"Polygon\",\"coordinates\":[[[27.441732505045678,53.93265949258276],[27.454252103320275,53.914255468787665],[27.481039450251068,53.92101360612726],[27.495769094762238,53.931732521154395],[27.49693148987876,53.952654317829214],[27.47307399170134,53.95398000106455],[27.441732505045678,53.93265949258276]]]}";
            JsonSerializer.Deserialize<Geometry>(json);

            Assert.True(true);
        }
        
        [Fact]
        public void Geometry_Serialization_Correct()
        {
            var json = "{\"Coordinates\":[[[27.441732505045678,53.93265949258276]]],\"Type\":\"Polygon\"}";
            var obj = new Geometry(FeatureType.Polygon,
                Enumerable.Repeat(
                    new GeoPointCoordsCollection(
                        new List<GeoPointCoordinates> { new GeoPointCoordinates(27.441732505045678, 53.93265949258276) }), 1));

            var result = JsonSerializer.Serialize(obj);

            Assert.Equal(json, result);
        }
    }
}

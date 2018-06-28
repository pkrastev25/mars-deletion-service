using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.Utils;
using Xunit;

namespace UnitTests.Utils
{
    public class EnumUtilTests
    {
        [Fact]
        public void DoesResourceTypeExist_ProjectResourceType_ReturnsTrue()
        {
            // Arrange
            var resourceType = ResourceTypeEnum.Project;

            // Act
            var result = EnumUtil.DoesResourceTypeExist(resourceType);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DoesResourceTypeExist_EmptryResourceType_ReturnsFalse()
        {
            // Arrange
            var resourceType = "";

            // Act
            var result = EnumUtil.DoesResourceTypeExist(resourceType);

            // Assert
            Assert.False(result);
        }
    }
}
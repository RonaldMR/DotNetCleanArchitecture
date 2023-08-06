using CleanApp.Domain.Enums;

namespace CleanApp.Application.Constants
{
    public static class RoomTypeSizeConstants
    {
        public static Dictionary<RoomType, short> Sizes = new()
        {
            { RoomType.Single, 1 },
            { RoomType.Double, 2 },
            { RoomType.Suite, 3 },
            { RoomType.Presidential, 4 }
        };
    }
}

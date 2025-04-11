namespace Mr1Ceng.Util;

/// <summary>
/// 经纬度坐标点
/// </summary>
public class LocationPoint
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LocationPoint()
    {
        Lng = 0;
        Lat = 0;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="lng">经度</param>
    /// <param name="lat">纬度</param>
    public LocationPoint(decimal lng, decimal lat)
    {
        Lng = lng;
        Lat = lat;
    }

    /// <summary>
    /// 经度
    /// </summary>
    public decimal Lng { get; set; }

    /// <summary>
    /// 纬度
    /// </summary>
    public decimal Lat { get; set; }
}

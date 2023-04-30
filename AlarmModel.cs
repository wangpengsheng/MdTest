namespace Demo;

public enum AlarmType
{
    Master1,
    Master2,
    Normal
}

public class AlarmItem
{
    public string AlarmMessage { get; set; }
    public AlarmType AlarmType { get; set; }
    public string AlarmCode { get; set; }
    public string HappenTime { get; set; }
    public string ResetTime { get; set; }
    public string AlarmSolution { get; set; }
    public AlarmItem()
    {
        AlarmMessage = @"默认的报警信息";
        AlarmType = AlarmType.Normal;
        AlarmCode = @"Undefine";
        HappenTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public override string ToString()
    {
        return AlarmMessage + "," + AlarmType + HappenTime + HappenTime + AlarmSolution;
    }
}
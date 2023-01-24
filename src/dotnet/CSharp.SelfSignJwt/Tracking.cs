namespace CSharp.SelfSignJwt;

public class Tracking
{
    public Tracking(string trackingId)
    {
        this.trackingId = trackingId;
    }

    public Tracking()
    {
        this.trackingId = string.Empty;
    }
    public string trackingId {get; set;}
}
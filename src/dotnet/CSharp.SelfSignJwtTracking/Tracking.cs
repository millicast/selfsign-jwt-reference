namespace CSharp.SelfSignJwtTracking;

public class Tracking
{
    public Tracking(string trackingId)
    {
        this.trackingId = trackingId;
    }

    // public Tracking()
    // {
    //     this.trackingId = null;
    // }
    public string trackingId { get; set;}
}
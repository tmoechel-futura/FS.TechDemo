namespace FS.TechDemo.BuyerBFF.Configuration;

public class GrpcOptions
{
    public const string GrpcOut = "GRPCOut";
    public List<GrpcMetadata> Grpc { get; set; } = new List<GrpcMetadata>();
}

public class GrpcMetadata
{
    public string Destination { get; set; } = "";
    public GrpcChannelMetadata Channel { get; set; } = new GrpcChannelMetadata();
}

public class GrpcChannelMetadata
{
    public string Endpoint { get; set; } = "";
    public bool UseTls { get; set; } = true;
}
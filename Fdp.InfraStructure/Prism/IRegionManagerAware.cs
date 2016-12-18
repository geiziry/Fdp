using Prism.Regions;

namespace Fdp.InfraStructure.Prism
{
    public interface IRegionManagerAware
    {
        IRegionManager _RegionManager { get; set; }
    }
}
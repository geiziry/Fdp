using Prism.Regions;

namespace Fdp.InfraStructure.Prism
{
    public interface IRegionManagerAware
    {
        IRegionManager RegionManager { get; set; }
    }
}
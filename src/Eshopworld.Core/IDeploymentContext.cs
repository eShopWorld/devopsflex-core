using System.Collections.Generic;

namespace Eshopworld.Core
{
    /// <summary>
    /// this interface represents deployment context
    /// 
    ///  - region hierarchy
    /// </summary>
    public interface IDeploymentContext
    {
        /// <summary>
        /// list of regions in hierarchical order
        /// </summary>
        IEnumerable<string> PreferredRegions { get; }
    }
}

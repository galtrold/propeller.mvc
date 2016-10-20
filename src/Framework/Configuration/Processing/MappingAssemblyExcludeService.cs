using System.Linq;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.IO;
using Sitecore.Text;
using Sitecore.Xml;

namespace Propeller.Mvc.Core.Processing
{
  internal class MappingAssemblyExcludeService
  {
    public MappingAssemblyExcludeService()
    {
      ExcludePatterns = GetExcludePatternsFromConfiguration();
    }

    private string[] ExcludePatterns { get; set; }

    private string[] GetExcludePatternsFromConfiguration()
    {
      return (from XmlNode configNode in Factory.GetConfigNodes("viewModelResolver/excludeAssemblies/*")
        select XmlUtil.GetAttribute("fileName", configNode)).Select(s => s.ToLowerInvariant()).ToArray();
    }

    public bool Check(string assemblyFileName)
    {
      return
        ExcludePatterns.Any(
          excludePattern =>
            WildCardParser.Matches(FileUtil.GetFileName(assemblyFileName), WildCardParser.GetParts(excludePattern)));
    }
  }
}
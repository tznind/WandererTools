using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using Wanderer.Factories;

namespace src
{
    public class ZipWorldFactory : WorldFactory
    {
        List<WorldFactoryResource> resources = new List<WorldFactoryResource>();
        public ZipWorldFactory(ZipArchive archive)
        {
            foreach(var e in archive.Entries)
            {
                string content;

                using(var s = new StreamReader(e.Open()))
                    content = s.ReadToEnd();

                resources.Add(new WorldFactoryResource(e.FullName
                ,content));
            }
        }
        protected override WorldFactoryResource GetResource(string name)
        {
            return GetResources(name).FirstOrDefault();
        }

        protected override IEnumerable<WorldFactoryResource> GetResources(string pattern)
        {
            var reg = new Regex(pattern.Replace("*",".*"),RegexOptions.IgnoreCase);
            return resources.Where(r=>reg.IsMatch(r.Location));
        }
    }
}

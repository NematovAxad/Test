using System;
using System.Collections.Generic;
using System.Text;

namespace MainInfrastructures.Services
{
    public class RankingStruct
    {
       public List<Sphere> Spheres { get; set; }
    }
    public class Sphere
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public double MaxRate { get; set; }
        public List<Fields> Fields { get; set; }
    }

    public class Fields
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public double MaxRate { get; set; }
        public List<SubFields> SubFields { get; set; }
    }


    public class SubFields
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public double MaxRate { get; set; }
    }
}
